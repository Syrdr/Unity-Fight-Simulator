using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions(); // Create an instance of the input actions class

        // Bind the Move action to the left joystick (Gamepad Left Stick)
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnEnable() => inputActions.Enable();  // Enable the input actions when the script is enabled
    void OnDisable() => inputActions.Disable();  // Disable the input actions when the script is disabled

    void Update()
    {
        // Convert the joystick input into a camera movement direction
        Vector3 moveDirection = new Vector3(-moveInput.y, 0, moveInput.x);

        // Move the camera based on joystick input
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
