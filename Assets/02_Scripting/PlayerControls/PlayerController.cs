using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("PlayerControls")]
    [SerializeField] float groundedRange;
    [SerializeField] float movementSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float sprintSpeedMultiplier;
    [SerializeField] float jumpForce;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float maxCameraAngle;
    [Tooltip("The amount of attacks per second the player is able to do")]
    [SerializeField] float attackSpeed;
    [SerializeField] float weaponDamage;
    [SerializeField] float grabDistance;
    [SerializeField] bool isGnomeGrabbed;
    [SerializeField] float throwForce;
    

    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerInput input;
    [SerializeField] Camera playerCamera;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] Weapon weapon;
    [SerializeField] private LayerMask grabMask;
    [SerializeField] Transform weaponParent;

    [SerializeField]private GrabGnome currentGnome;

    // Internal
    bool sprinting;
    Vector2 move;
    Vector2 look;
    float yaw;
    float pitch;
    bool attacking;

    public static PlayerController instance;
    #endregion

    #region Input
    public void OnMove(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        move = movement;
    }
    public void OnLook(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        look = movement;
    }
    public void OnJump(InputValue input)
    {
        if (Grounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    public void OnSprint(InputValue input)
    {
        sprinting = !sprinting;
    }
    public void OnAttack(InputValue input)
    {
        if (!attacking)
        {
            attacking = true;
            weaponAnimator.Play("MeleeWeaponAttack");
            weaponAnimator.speed = 1f / attackSpeed;
            weapon.Attack(attackSpeed, weaponDamage);
        }
    }
    public void OnGrab(InputValue input)
    {
        Debug.Log("Grab/Throw");
        Debug.Log("Gnome grabbed: " + isGnomeGrabbed);
        if (isGnomeGrabbed)
        {
            Throw();
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * grabDistance, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabMask))
        {
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Gnome"))
            {
                Debug.Log("Gnome hit!");
                GrabGnome grab = hit.collider.GetComponent<GrabGnome>();
                if (grab == null) return;

                isGnomeGrabbed = true;
                grab.Grab(playerCamera.transform);
                currentGnome = grab;
            }
        }
    }

    private void Throw()
    {
        isGnomeGrabbed = false;
        currentGnome.Throw(playerCamera.transform.forward, throwForce);
        currentGnome = null;
    }
    #endregion

    #region Methods
    private void OnDestroy()
    {
        EventBusManager.instance.EquipWeaponEvent.Unregister(ChangeWeapon);
    }
    private IEnumerator Start()
    {
        if (instance == null) instance = this;
        yield return new WaitForEndOfFrame();
        EventBusManager.instance.EquipWeaponEvent.Register(ChangeWeapon);
    }
    /// <summary>
    /// Returns whether the player is close to the ground or not
    /// </summary>
    /// <returns></returns>
    bool Grounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundedRange))
        {
            if (hit.collider.tag == "ground") return true;
        }
        return false;
    }
    private void Update()
    {
        DoCamera();

        FixAnimator();
    }
    private void FixedUpdate()
    {
        DoMovement();
    }
    void FixAnimator()
    {
        AnimatorStateInfo info = weaponAnimator.GetCurrentAnimatorStateInfo(0);
        if (attacking && !info.IsName("MeleeWeaponAttack")) attacking = false;
    }
    /// <summary>
    /// Move the player around
    /// </summary>
    void DoMovement()
    {
        float moveSpeed = sprinting ? movementSpeed * sprintSpeedMultiplier : movementSpeed;

        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * move.y * moveSpeed);
            rb.AddForce(transform.right * move.x * moveSpeed);
        }
    }
    /// <summary>
    /// Rotate player/camera to allow player to look around
    /// </summary>
    void DoCamera()
    {
        yaw += look.x * cameraSensitivity;
        pitch -= look.y * cameraSensitivity;

        pitch = Mathf.Clamp(pitch, -maxCameraAngle, maxCameraAngle);

        transform.localRotation = Quaternion.Euler(0f, yaw, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
    void ChangeWeapon(EquipWeaponEventData data)
    {
        Destroy(weapon.gameObject);
        if (data.weapon != null)
        {
            weapon = Instantiate(data.weapon.WeaponPrefab, weaponParent);
            weapon.transform.localPosition = new Vector3(0, 0.5f, 0);
            attackSpeed = data.weapon.AttackSpeed;
            weaponDamage = data.weapon.Damage;
        }
        else
        {
            Debug.LogWarning("FISTS NOT IMPLEMENTED YET");
        }
    }
    #endregion
}