using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : CreatureController
{
    IKController _IKController = null;
    public Stacker Stacker { get; private set; } = null;
    public Needs Needs { get; set; } = null;

    public override void Init()
    {
        base.Init();

        _IKController = gameObject.GetOrAddComponent<IKController>();
        Stacker = GetComponentInChildren<Stacker>();

        StateMachine.RegisterState<StateIdleCustomer>(Define.State.Idle, this);
        StateMachine.RegisterState<StateMoveCustomer>(Define.State.Move, this);

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
        StartCoroutine(Stacker.Co_Lean(targetRotation, time, waitTime));
    }

    public void UpdateArm()
    {
        if (0 == Stacker.Count)
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

    public void AquireItem(GameObject itemObject)
    {
        if(itemObject == null)
        {
            return;
        }

        
    }
}
