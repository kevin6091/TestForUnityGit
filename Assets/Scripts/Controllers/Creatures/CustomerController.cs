using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : CreatureController
{
    IKController _IKController = null;
    public Stacker Stacker { get; private set; } = null;
    public Needs<Define.ItemType> Needs { get; private set; } = null;

    public override void Init()
    {
        base.Init();

        _IKController = gameObject.GetOrAddComponent<IKController>();
        Stacker = GetComponentInChildren<Stacker>();

        Needs = new Needs<Define.ItemType>(Define.ItemType.Pizza, UnityEngine.Random.Range(3, 5));

        StateMachine.RegisterState<StateIdleCustomer>(Define.State.Idle, this);
        StateMachine.RegisterState<StateMoveCustomer>(Define.State.Move, this);
        StateMachine.RegisterState<StateEatCustomer>(Define.State.Eat, this);

        Stacker.pushEvent -= OnStackerPush;
        Stacker.pushEvent += OnStackerPush;

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

    private void OnStackerPush()
    {
        Needs.AquireNeed();

        if (Needs.IsEnough)
        {
            StartCoroutine(Co_FindEmptyTable());            
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

    public IEnumerator Co_FindEmptyTable()
    {
        while(true)
        {
            ProbController nearestProb = Managers.Prob.GetNearestProb(Define.ProbType.Table, transform.position, TableController.HasEmptySeat);
            if(nearestProb != null)
            {
                TableController table = nearestProb as TableController;
                ObjectHolder emptySeat = table.GetEmptySeat();
                if (emptySeat != null)
                {
                    StartCoroutine(Co_EatSequence(table, emptySeat));
                    yield break;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator Co_EatSequence(TableController table, ObjectHolder emptySeat)
    {
        Target.TargetObj = emptySeat.gameObject;
        Target.Range = 1f;
        State = Define.State.Move;

        emptySeat.HoldObject = gameObject;

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
            table.Stacker.Push(Stacker.Pop());
        }


        State = Define.State.Eat;
        float accTime = 0f;

        while (true)
        {
            accTime += Time.deltaTime;
            if(accTime >= 3f)
            {
                accTime -= 3f;
                GameObject eatObject = table.Stacker.Pop();
                if(eatObject != null)
                {
                    Managers.Item.DestoyItem(eatObject);
                }

                if(table.Stacker.IsEmpty)
                {
                    ProbController nearestStand = Managers.Prob.GetNearestProb(Define.ProbType.Stand, transform.position);
                    (nearestStand as StandController).WaitingLine.Enqueue(this);
                    Needs = new Needs<Define.ItemType>(Define.ItemType.Pizza, UnityEngine.Random.Range(3, 5));
                    emptySeat.HoldObject = null;
                    yield break;
                }
            }

            yield return null;
        }
    }
}
