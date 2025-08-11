using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction moveAction;

    private Vector2 moveAmt;
    private Rigidbody2D rb;

    public float walkSpeed = 5;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveAmt = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Walking();
    }

    private void Walking()
    {
        rb.MovePosition(rb.position + moveAmt * walkSpeed * Time.deltaTime);
    }
}
