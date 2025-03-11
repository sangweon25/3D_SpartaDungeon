using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("다음 목표 위치")]
    [SerializeField] Transform targetPos;
    [Tooltip("wayPoints 하나 이상 생성")]
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float unitPerSecond;
    [SerializeField] bool isLoop = true;
    [SerializeField] bool isPlayOnAwkae = true;
    private Rigidbody _rigidbody;
    private int wayPointCnt;
    private int curPointIdx;
    [Tooltip("목표 도착 후 대기 시간")]
    public float waitTime;

    public GameObject newObj;

    void Awake()
    {
        wayPointCnt = wayPoints.Count;

        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        if (targetPos == null || targetPos.Equals(transform))
        {
            targetPos = transform;
            if (wayPoints.Count == 1)
            {
                Instantiate(newObj, targetPos);
                
                newObj.transform.position = targetPos.transform.position;
                SwapWayPoint(newObj.transform);
            }

        }
        if (isPlayOnAwkae == true)
            Play();
    }

    public void Play()
    {
        StartCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        var wait = new WaitForSeconds(waitTime);

        while (true)
        {
            // wayPoints[curPointIdx].position 까지 이동
            yield return StartCoroutine(MoveAToB(targetPos.position, wayPoints[curPointIdx].position));

            if(curPointIdx < wayPointCnt - 1)
            {
                curPointIdx++;
            }
            else
            {
                if (isLoop == true)
                    curPointIdx = 0;
                else
                    break;
            }
            yield return wait;
        }
    }

    private IEnumerator MoveAToB(Vector3 start, Vector3 end)
    {
        float percent = 0;
        //이동 시간 = 총 이동거리 / 초당 이동 거리
        float moveTime = Vector3.Distance(start, end)/ unitPerSecond;

        while(percent < 1)
        {
            percent += Time.deltaTime / moveTime;

            targetPos.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

    private void SwapWayPoint(Transform transform)
    {
        var temp = wayPoints[0];
        wayPoints[0] = transform;
        wayPoints.Add(temp);
        wayPointCnt++;
    }
}
