using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : CreatureController
{
    PlayerStat _stat = null;
    IKController _IKController = null;
    Animator _anim = null;
    Stacker _stacker = null;

    bool _stopSkill = false;

    public PlayerStat Stat { get { return _stat; } private set { _stat = value; } }

    public override void Init()
    {
        _stat = GetComponent<PlayerStat>();
        _IKController = GetComponent<IKController>();
        _anim = GetComponent<Animator>();
        _stacker = GetComponentInChildren<Stacker>();
        _stateMachine = new StateMachine();

        StateMachine.RegisterState<StateIdlePlayer>(Define.State.Idle, this);
        StateMachine.RegisterState<StateMovePlayer>(Define.State.Move, this);

        State = Define.State.Idle;

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        //  Sync Hand Socket Stacker
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).name == "Stacker")
            {
                _IKController.LeftHandTarget = transform.GetChild(i).gameObject.GetComponent<Stacker>().LeftHandSocket;
                _IKController.RightHandTarget = transform.GetChild(i).gameObject.GetComponent<Stacker>().RightHandSocket;
                break;
            }
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

        State = Define.State.Move;
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Die:
                break;
            case Define.State.Move:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
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
                    TargetPos = hit.point; State = Define.State.Move;
                    _stopSkill = false;

                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                    {
                        LockTarget = hit.collider.gameObject;
                    }
                    else
                    {
                        LockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                if (LockTarget == null && 
                    isRaycastHit)
                    TargetPos = hit.point;
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    void OnMouseEvent_Skill(Define.MouseEvent evt)
    {
        if(evt == Define.MouseEvent.PointerUp)
        {
            _stopSkill = true;
        }
    }

    public void OnKeyboard()
    {
        State curState = StateMachine.CurState;
        if (curState is IKeyHandler == true)
        {
            IKeyHandler keyHandler = (IKeyHandler)curState;
            keyHandler.OnKeyboard();
        }
    }

    public void PlayAnim(string name)
    {
        _anim.Play(name);
    }

    public void CrossfadeAnim(string name, float time)
    {
        _anim.CrossFade(name, time);
    }

    public void LeanStacker(Quaternion targetRotation, float time, float waitTime = 0f)
    {
        StartCoroutine(_stacker.Co_Lean(targetRotation, time, waitTime));
    }
}
