using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Input References")]
    [SerializeField] private InputActionReference moveAction;

    private CharacterController controller;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    private void Update()
    {
        // Leer el valor Vector2 del Input System
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Calcular la dirección basada en la rotación actual del jugador
        Vector3 move = transform.right * input.x + transform.forward * input.y;

        // Aplicar movimiento
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Aplicar gravedad constante para mantenerlo en el piso del camión
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}