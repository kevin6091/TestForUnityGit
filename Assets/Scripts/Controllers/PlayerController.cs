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

    GameObject _lockTarget = null;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    [SerializeField]
    PlayerState _state = PlayerState.Idle;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("RUN", 0.25f);
                    break;
                case PlayerState.Idle:
                    anim.CrossFade("WAIT", 0.25f);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("ATTACK", 0.25f, -1, 0f);
                    break;
            }
        }
    }

    void Start()
    {
        _stat = GetComponent<PlayerStat>();

        //  Todo : 추 후 조작관련 기능 추가
        //Managers.Input.MoveKeyAction -= OnMoveKey;
        //Managers.Input.MoveKeyAction += OnMoveKey;
        Managers.Input.MouseActions -= OnMouseEvent;
        Managers.Input.MouseActions += OnMouseEvent;

        Managers.Input.TouchActions[(int)Define.TouchEvent.Drag] += UpdateIdle;
    }


    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir;
        float dist;

        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;

            dir = _destPos - transform.position;
            dist = dir.magnitude;

            if ((_destPos - transform.position).magnitude <= 1f)
            {
                State = PlayerState.Skill;
                return;
            }
        }
        else
        {
            dir = _destPos - transform.position;
            dist = dir.magnitude;
        }


        if (dist < /*0.0001f*/ 0.1f)
            State = PlayerState.Idle;

        else
        {
            NavMeshAgent nma = gameObject.GetComponent<NavMeshAgent>();
            //  nma.CalculatePath();

            //  Move
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0f, dist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }

            nma.Move(dir.normalized * moveDist);

            //  Rotate
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), _stat.RotateSpeed * Time.deltaTime);
        }
    }

    void UpdateIdle()
    {
    }

    void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), _stat.RotateSpeed * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if(_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }
    }

    void Update()
    {
        switch (State)
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * _stat.RotateSpeed);
        }
        else if (true == s)
        {
            transform.position += (Vector3.back * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), Time.deltaTime * _stat.RotateSpeed);
        }

        if (true == a)
        {
            transform.position += (Vector3.left * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), Time.deltaTime * _stat.RotateSpeed);
        }
        else if (true == d)
        {
            transform.position += (Vector3.right * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), Time.deltaTime * _stat.RotateSpeed);
        }

        State = PlayerState.Moving;
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Die:
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                OnMouseEvent_Skill(evt);
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist = 100f;
        RaycastHit hit;
        int layerMask = (1 << (int)Define.Layer.Floor) | (1 << (int)Define.Layer.Monster);

        bool isRaycastHit = Physics.Raycast(ray, out hit, dist, layerMask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                if (isRaycastHit)
                {
                    _destPos = hit.point; State = PlayerState.Moving;
                    _stopSkill = false;

                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                    {
                        _lockTarget = hit.collider.gameObject;
                    }
                    else
                    {
                        _lockTarget= null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                if (_lockTarget == null && 
                    isRaycastHit)
                    _destPos = hit.point;
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    bool _stopSkill = false;
    void OnMouseEvent_Skill(Define.MouseEvent evt)
    {
        if(evt == Define.MouseEvent.PointerUp)
        {
            _stopSkill = true;
        }
    }
}
