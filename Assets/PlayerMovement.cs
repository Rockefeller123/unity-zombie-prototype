using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private Transform playerCamera;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float groundedForce = -2f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 7f;

    private CharacterController characterController;
    private float pitch;
    private float verticalVelocity;
    public bool canMove = true;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null)
                playerCamera = cam.transform;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!canMove) 
        return;

        HandleLook();
        HandleMove();
    }


    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        pitch = Mathf.Clamp(pitch - mouseY, minPitch, maxPitch);

        float recoil = 0f;
        Gun gun = GetComponentInChildren<Gun>();
        if (gun != null)
            recoil = gun.CurrentRecoil;

        if (playerCamera != null)
            playerCamera.localRotation = Quaternion.Euler(pitch - recoil, 0f, 0f);
    }

    private void HandleMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Ground check
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = groundedForce;

            // Bunny hop jump
            if (Input.GetKeyDown(KeyCode.Space))
                verticalVelocity = jumpForce;
        }

        // Gravity
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * moveSpeed;
        velocity.y = verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);
    }
}


