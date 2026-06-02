using UnityEngine;
using UnityEngine.InputSystem;
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

    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerInput input;
    [SerializeField] Camera playerCamera;
    [SerializeField] Animator weaponAnimator;

    // Internal
    bool sprinting;
    Vector2 move;
    Vector2 look;
    float yaw;
    float pitch;
    bool attacking;
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
        }
    }
    #endregion

    #region Methods
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
    #endregion
}