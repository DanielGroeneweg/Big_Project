using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent (typeof(Stamina))]
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

    [Header("Audio")]
    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip[] jumpSounds;

    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerInput input;
    [SerializeField] Camera playerCamera;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] Weapon weaponCollider;
    [SerializeField] GameObject weaponModel;
    [SerializeField] private LayerMask grabMask;
    [SerializeField] Transform weaponParent;
    [SerializeField] Transform weaponColliderParent;
    [SerializeField] WeaponItem currentWeapon;
    private GrabGnome currentGnome;

    Stamina stamina;
    // Internal
    bool sprinting;
    Vector2 move;
    Vector2 look;
    float yaw;
    float pitch;
    bool attacking;
    bool canPlayStepSound = true;

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
        if (stamina.ActionStaminaDictionary[playerActions.Jump] > stamina._Stamina)
        {
            return;
        }

        if (Grounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            stamina.UseStamina(stamina.ActionStaminaDictionary[playerActions.Jump]);
            SoundManager.instance.PlaySound(jumpSounds[Random.Range(0, jumpSounds.Length)]);
        }
    }
    public void OnSprint(InputValue input)
    {
        sprinting = !sprinting;
    }
    public void OnAttack(InputValue input)
    {
        if (!attacking && weaponCollider != null)
        {
                    if (stamina.ActionStaminaDictionary[playerActions.Attack] > stamina._Stamina)
                    {
                        return;
                    }

            attacking = true;
            weaponAnimator.Play("MeleeWeaponAttack");
            //weaponAnimator.speed = attackSpeed;
            weaponCollider.Attack(1f / attackSpeed, weaponDamage);
            stamina.UseStamina(stamina.ActionStaminaDictionary[playerActions.Attack]);
            StartCoroutine(FixAnimator());
        }
    }
    public void OnGrab(InputValue input)
    {
        Debug.Log("Grab/Throw");
        Debug.Log("Gnome grabbed: " + isGnomeGrabbed);
        if (isGnomeGrabbed)
        {
            if (stamina.ActionStaminaDictionary[playerActions.Throw] > stamina._Stamina)
            {
                return;
            }

            Throw();
            stamina.UseStamina(stamina.ActionStaminaDictionary[playerActions.Throw]);
            return;
        }

        else
        {
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

                    if (stamina.ActionStaminaDictionary[playerActions.Grab] > stamina._Stamina)
                    {
                        return;
                    }

                    isGnomeGrabbed = true;
                    grab.Grab(playerCamera.transform);
                    currentGnome = grab;
                    stamina.UseStamina(stamina.ActionStaminaDictionary[playerActions.Grab]);
                }
            }
        }
    }
    private void Throw()
    {
        isGnomeGrabbed = false;
        currentGnome.Throw(new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z), throwForce);
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
        stamina = GetComponent<Stamina>();
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
    }
    private void FixedUpdate()
    {
        DoMovement();
    }
    IEnumerator FixAnimator()
    {
        yield return new WaitForSeconds(1f / attackSpeed);
        yield return null;
        attacking = false;
    }
    /// <summary>
    /// Move the player around
    /// </summary>
    void DoMovement()
    {
        float multiplier = 1;

        if (sprinting)
        {
            if (stamina.ActionStaminaDictionary[playerActions.Sprint] <= stamina._Stamina)
            {
                multiplier = sprintSpeedMultiplier;
                stamina.UseStamina(stamina.ActionStaminaDictionary[playerActions.Sprint]);
            }
        }

        float moveSpeed = movementSpeed * multiplier;

        if (move.magnitude != 0 && canPlayStepSound)
        {
            canPlayStepSound = false;
            AudioClip clip = footSteps[Random.Range(0, footSteps.Length)];
            SoundManager.instance.PlaySound(clip, multiplier);
            StartCoroutine(EnableFootStep(clip, multiplier));
        }

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
        if (currentWeapon != null && data.oldWeaponDestroyed == false)
        {
            DropWeaponEventData dropData = new DropWeaponEventData() { weapon = currentWeapon, position = transform.position, droppedByEnemy = false, durability = weaponCollider.durability };
            EventBusManager.instance.DropWeaponEvent.Raise(dropData);
        }

        if (weaponModel != null)
        {
            Destroy(weaponModel.gameObject);
            weaponModel = null;
        }

        if (weaponCollider != null)
        {
            Destroy(weaponCollider.gameObject);
            weaponCollider = null;
        }

        if (data.weapon != null)
        {
            weaponModel = Instantiate(data.weapon.WeaponPrefab, weaponParent);
            weaponModel.transform.localPosition = new Vector3(0, 0.5f, 0);
            attackSpeed = data.weapon.AttackSpeed;
            weaponDamage = data.weapon.Damage;
            currentWeapon = data.weapon;

            weaponCollider = Instantiate(data.weapon.WeaponColliderPrefab, weaponColliderParent);
            weaponCollider.durability = data.durability;
            weaponCollider.maxDurability = data.weapon.StartDurability;
        }
    }
    IEnumerator EnableFootStep(AudioClip clip, float time)
    {
        float pause = time;
        pause = clip.length * (1 / time);
        yield return new WaitForSeconds(pause);
        canPlayStepSound = true;
    }
    #endregion
}