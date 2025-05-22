using DG.Tweening;
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
    private IKController _IKController = null;
    private Stacker _stacker = null;

    public override void Init()
    {
        base.Init(); 

        _IKController = gameObject.GetOrAddComponent<IKController>();
        _stacker = GetComponentInChildren<Stacker>();

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

        Managers.UI.ShowSceneUI<UI_Joystick>("UI_Joystick");

        //Test

        Stat.MoveSpeed = 10.0f;

        for (int i = 0; i < 7; i++)
        {
            EmployeeController employee = (Managers.Resource.Instantiate("Employee/Employee")).GetComponent<EmployeeController>();
            Managers.Employee.AddEmployee(employee);
        }
    }

    void JoyStickMove(object[] objects)
    {
        Vector3 dir = ((Vector3)objects[0]).normalized;

        dir = new Vector3(dir.x, 0.0f, dir.y);

        NavMeshAgent nma = GetComponent<NavMeshAgent>();
        nma.Move(dir * Time.deltaTime * Stat.MoveSpeed);
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("StartCoroutine2");
            StartCoroutine("Co_Test"); 
            Debug.Log("StartCoroutine After");
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

    public void LeanStacker(Quaternion targetRotation, float time, float waitTime = 0f)
    {
        StartCoroutine(_stacker.Co_Lean(targetRotation, time, waitTime));
    }

    public void UpdateArm()
    {
        if (0 == _stacker.Count)
        {
            float curWeight = _IKController.Weight;
            curWeight = Mathf.Max(curWeight - Time.deltaTime * 5f, 0f);
            _IKController.Weight = curWeight;
        }
        else
        {
            float curWeight = _IKController.Weight;
            curWeight = Mathf.Min(curWeight + Time.deltaTime * 5f, 1f);
            _IKController.Weight = curWeight;
        }
    }

    public IEnumerator Co_Test()
    {
        Debug.Log("Execute Coroutine");
        yield return new WaitForSeconds(3f);
        yield break;
    }
}
