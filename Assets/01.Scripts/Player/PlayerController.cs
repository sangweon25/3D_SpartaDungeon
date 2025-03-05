using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed = 2f;
    private Vector2 inputDir;


    [Header("Look")]


    private Rigidbody _rigidbody;

    private void Awake()
    {
        TryGetComponent<Rigidbody>(out _rigidbody);
    }

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 dir = transform.forward * inputDir.y + transform.right * inputDir.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            inputDir = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            inputDir = Vector2.zero;
        }
    }

}
