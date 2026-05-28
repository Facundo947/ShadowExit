using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationOffset;
    [SerializeField] private InputActionReference moveActionRference;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 moveInput;
    private Vector2 facingDirection = Vector2.right;

    public Vector2 FacingDirection => facingDirection;
    public Vector2 MoveInput => moveInput;
    public bool HasMoveInput => moveInput.sqrMagnitude > 0.001f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void OnEnable()
    {
        moveActionRference.action.Enable();
        moveActionRference.action.performed += OnMovePerfomed;
        moveActionRference.action.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        moveActionRference.action.performed -= OnMovePerfomed;
        moveActionRference.action.canceled -= OnMoveCanceled;
        moveActionRference.action.Disable();
    }

    private void OnMovePerfomed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    public void TickMovement(float deltaTime)
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * deltaTime);
    }

    private void RotateTowardsMouse()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera == null || Mouse.current == null)
        {
            return;
        }

        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 lookDirection = mouseWorldPosition - transform.position;

        if (lookDirection.sqrMagnitude <= 0.001f)
        {
            return;
        }

        facingDirection = lookDirection.normalized;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
