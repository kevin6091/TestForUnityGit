using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeController : CreatureController
{
    Stat _stat = null;
    IKController _IKController = null;
    Stacker _stacker = null;

    public override void Init()
    {
        base.Init();
        
        _stat = gameObject.GetOrAddComponent<Stat>();
        _IKController = gameObject.GetOrAddComponent<IKController>();
        _stacker = GetComponentInChildren<Stacker>();

        StateMachine.RegisterState<StateIdleEmployee>(Define.State.Idle, this);
        StateMachine.RegisterState<StateMoveEmployee>(Define.State.Move, this);

        State = Define.State.Idle;

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

    public IEnumerator MoveToWorkRoutine(Vector3 target)
    {
        yield return null;
    }
}
