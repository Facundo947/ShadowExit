using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private InputActionReference moveActionRference;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed *  Time.deltaTime);
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



    private void Action_canceled(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnMovePerfomed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }
}
