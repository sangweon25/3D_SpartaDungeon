using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action<PlayerController> OnFixedUpdate;

    [Header("Movement")]
    private float moveSpeed = 5f;
    public float jumpPower = 100f;
    private Vector2 inputDir;
    public LayerMask groundLayerMask;
    public float jumpStamina = 20f;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; 
    public float maxXLook; 

    private float camCurXRot;
    public float lookSensitivity; 
    private Vector2 mouseDelta; 
    public Vector2 MouseDelta {  get { return mouseDelta; } }
    public bool canLook = true;

    [HideInInspector] public Vector3 forcedVelocity;
    [HideInInspector] public Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        Move();
        //함수가 등록되어있다면 Invoke
        OnFixedUpdate?.Invoke(this);
    }
    private void LateUpdate()
    {
        if(canLook)
        {
            //CameraLook();
        }
    }
    void Move()
    {
        Vector3 dir = transform.forward * inputDir.y + transform.right * inputDir.x;
        dir *= moveSpeed;
        dir += forcedVelocity;
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

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started&& IsGrounded())
        {
            if(CharacterManager.Instance.Player.resource.Stamina.CurVal > jumpStamina)
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                CharacterManager.Instance.Player.resource.St_Decrease(jumpStamina);
            }
        }
    }
    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right* 0.2f) + (transform.up * 0.01f),Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void BoostSpeed(float value,float time)
    {
        StartCoroutine(Boost(value,time));
    }
    public IEnumerator Boost(float value,float time)
    {
        moveSpeed = value;
        yield return new WaitForSeconds(time);
        moveSpeed = 5f;
    }

}
