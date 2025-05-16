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

        Managers.Input.Actions[(Define.InputEvent.MouseEvent, Define.InputType.Drag)] -= JoyStickMove;
        Managers.Input.Actions[(Define.InputEvent.MouseEvent, Define.InputType.Drag)] += JoyStickMove;

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

    void JoyStickMove(object[] objects)
    {
        Vector3 dir = ((Vector3)objects[0]).normalized;

        dir = new Vector3(dir.x, 0.0f, dir.y);

        NavMeshAgent nma = GetComponent<NavMeshAgent>();
        nma.Move(dir * Time.deltaTime * _stat.MoveSpeed);
        transform.rotation = Quaternion.LookRotation(dir);
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
