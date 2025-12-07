using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxMoveSpeed = 5f;
    [SerializeField]
    private float acceleration = 5f;

    private Rigidbody2D rigidbody;

    private Vector2 velocity;
    private Vector2 currentInput;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        currentInput = Vector2.zero;
    }

    public void MovePlayer(CallbackContext ctx)
    {
        //Debug.Log("MovePlayer called with phase: " + ctx.phase + " and value: " + ctx.ReadValue<Vector2>());
        if (ctx.phase == InputActionPhase.Performed)
        {
            currentInput = ctx.ReadValue<Vector2>();
        }
        currentInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        velocity = Vector2.MoveTowards(velocity, currentInput, acceleration * Time.deltaTime);
        rigidbody.linearVelocity = velocity * maxMoveSpeed;
    }
}
