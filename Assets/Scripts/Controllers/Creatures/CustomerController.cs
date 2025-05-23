using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
        StateMachine.RegisterState<StateEatCustomer>(Define.State.Eat, this);

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
        if (Stacker.IsEmpty)
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

    public IEnumerator Co_EatSequence(TableController targetTable)
    {
        Target.TargetObj = targetTable._holders[0].gameObject;
        Target.Range = 1f;
        State = Define.State.Move;

        while(true)
        {
            if(State != Define.State.Move)
            {
                break;
            }

            yield return null;
        }

        Target.Range = 0f;

        while(!Stacker.IsEmpty)
        {
            targetTable.Stacker.Push(Stacker.Pop());
        }

        float accTime = 0f;

        State = Define.State.Eat;
        Agent.Warp(targetTable.transform.position);
        //  Agent.isStopped = false;
        Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        while (!targetTable.Stacker.IsEmpty)
        {
            accTime += Time.deltaTime;
            if(accTime >= 3f)
            {
                accTime -= 3f;
                GameObject eatObject = targetTable.Stacker.Pop();
                Managers.Item.DestoyItem(eatObject);
            }

            yield return null;
        }
    }
}
