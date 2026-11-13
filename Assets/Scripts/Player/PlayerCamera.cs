using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("Configuración de Cámara")]
    [SerializeField] private float mouseSensitivity = 15f;
    [SerializeField] private float maxLookAngle = 90f;

    [Header("Referencias")]
    [SerializeField] private Transform playerBody; // Arrastrar el objeto padre (Player) aquí

    [Header("Input References")]
    [SerializeField] private InputActionReference lookAction;

    private float xRotation = 0f;

    private void Start()
    {
        // Bloquear y ocultar el cursor para la vista en primera persona
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (lookAction != null) lookAction.action.Enable();
    }

    private void OnDisable()
    {
        if (lookAction != null) lookAction.action.Disable();
    }

    private void Update()
    {
        // Leer el movimiento del mouse/stick
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotación vertical (Cámara)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (Cuerpo del jugador)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}