using System.Text.RegularExpressions;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraArm : MonoBehaviour
{
    private Player _player;
    private Camera _camera;

    [Range(1f, 50f)]
    [SerializeField] float _armLength = 3f;
    public float SpeedRot = 30f;

    private float _xRot;
    private float _yRot;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _camera = GetComponentInChildren<Camera>();

        _xRot = 0f;
        _yRot = 0f;
        SetTarget(_player);
        //_camera
    }

    void Update()
    {
        Rotate();
        CameraUpdate();
    }

    private void CameraUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _armLength))
        {
            _camera.transform.position = hit.point;
        }
        else
        {
            _camera.transform.position = transform.position + (-transform.forward * _armLength);
        }
    }

    private void Rotate()
    {
        //회전
        var xMove = CharacterManager.Instance.Player.controller.MouseDelta.x;
        var yMove = -CharacterManager.Instance.Player.controller.MouseDelta.y;

        _xRot += yMove * SpeedRot * Time.deltaTime;
        _yRot += xMove * SpeedRot * Time.deltaTime;

        _xRot = Mathf.Clamp(_xRot, -85f, 85f);

        transform.localRotation = Quaternion.Euler(_xRot, _yRot, 0f);
    }

    public void SetTarget(Player player)
    {
        //추적할 타겟 오브젝트를 세팅 (null로 세팅 가능)
        _player = player;
        if (player != null)
        {
            transform.localPosition =new Vector3(0,1.5f,0);
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
    }
}
