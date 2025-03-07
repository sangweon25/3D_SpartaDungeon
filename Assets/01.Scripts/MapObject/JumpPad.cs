using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("점프 velocity")]
    [Range(1f,500f)]
    public float yJumpVal = 100f; //y 점프값

    [Range(1f, 500f)]
    public float xzJumpVal = 10f; //xz 점프값

    //xz로 힘을 속력을 계산하는 값
    private Vector3 xzVector; 
    private float xzTime; // 점프시점부터 올라가는 시간 
    public float xzMaxTime = 10f; // 점프대 최대시간

    private void Awake()
    {
        //xzVector = transform.up * xzRebound;
        //xzVector.y = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //플레이어 검사
        if(collision.gameObject.CompareTag("Player"))
        {
            xzVector = transform.up * xzJumpVal;
            collision.rigidbody.AddForce(Vector3.up * yJumpVal, ForceMode.Impulse);

            //충돌시 OnJumping 바인딩
            CharacterManager.Instance.Player.controller.OnFixedUpdate += OnJumping;
            //collision.transform.GetComponent<PlayerController>().OnFixedUpdate += OnJumping;

            //충돌할때마다 0저장
            xzTime = 0f;
        }
    }

    private void OnJumping(PlayerController controller)
    {
        //PlayerController.FixedUpdate에서 호출되는 메서드로 time/maxTime으로 점점 forcedVelocity를 줄여서 저장
        controller.forcedVelocity = Vector3.Lerp(xzVector,Vector3.zero,xzTime / xzMaxTime);

        //점프대 충돌부터 xzMaxTime만큼 경과했거나 땅에 도달시
        if (xzTime >= xzMaxTime || controller.IsGrounded())
        {
            //점프 완료 시점, 속도와 해당 메서드 언바인딩
            controller.forcedVelocity = Vector3.zero;
            controller.OnFixedUpdate -= OnJumping;
        }
        //점프되는 시점부터 xzTime을 증가시켜준다.
        xzTime += Time.fixedDeltaTime;
    }
}
