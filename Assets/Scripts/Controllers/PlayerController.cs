using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat = null;
    Vector3 _destPos = new Vector3();

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    PlayerState _state = PlayerState.Idle;

    [SerializeField] float _rotateSpeed;
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
    }

    void Start()
    {
        _stat = GetComponent<PlayerStat>();

        //  Todo : 추 후 조작관련 기능 추가
        //Managers.Input.MoveKeyAction -= OnMoveKey;
        //Managers.Input.MoveKeyAction += OnMoveKey;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }


    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        float dist = dir.magnitude;
        if (dist < /*0.0001f*/ 0.1f)
            _state = PlayerState.Idle;

        else
        {
            NavMeshAgent nma = gameObject.GetComponent<NavMeshAgent>();
            //  nma.CalculatePath();

            //  Move
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0f, dist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            //  transform.Translate(dir.normalized * moveDist, Space.World);
            nma.Move(dir.normalized * moveDist);
            //  Rigidbody rigidBody = GetComponent<Rigidbody>();
            //  rigidBody.MovePosition(transform.position + dir.normalized * moveDist);
            //  Rotate
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), RotateSpeed * Time.deltaTime);
        }

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);
    }

    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0f);
    }

    void UpdateSkill()
    {

    }

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie(); 
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
    }

    void OnMoveKey(bool w, bool a, bool s, bool d)
    {
        //  Todo : 추 후 실제 플레이어 컨트롤러 제작시 추가
        return;

        if (true == w)
        {
            transform.position += (Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * _rotateSpeed);
        }
        else if (true == s)
        {
            transform.position += (Vector3.back * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), Time.deltaTime * _rotateSpeed);
        }

        if (true == a)
        {
            transform.position += (Vector3.left * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), Time.deltaTime * _rotateSpeed);
        }
        else if (true == d)
        {
            transform.position += (Vector3.right * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), Time.deltaTime * _rotateSpeed);
        }

        _state = PlayerState.Moving;
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist = 100f;

        RaycastHit hit;
        int layerMask = (1 << (int)Define.Layer.Floor) | (1 << (int)Define.Layer.Monster);
        if (Physics.Raycast(ray, out hit, dist, layerMask))
        {
            //  Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
            dist = hit.distance;

            _destPos = hit.point;
            _state = PlayerState.Moving;

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                Debug.Log("MonsterClick");
            else
                Debug.Log("FloorClick");
        }

        //  Debug.DrawRay(ray.origin, ray.direction * dist, Color.red, 1f);
    }
}
 