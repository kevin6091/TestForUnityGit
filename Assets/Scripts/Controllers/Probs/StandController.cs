using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StandController : ProbController
{
    public Stacker Stacker { get; private set; } = null;
    public ObjectHolder Holder { get; private set; } = null;
    public WaitingLine WaitingLine { get; private set; } = null;

    public bool IsWaitingLineEmpty
    { get { return WaitingLine == null || WaitingLine.IsEmpty; } }

    public bool IsStackerEmpty
    { get { return Stacker == null || Stacker.IsEmpty; } }

    public override void Init()
    {
        base.Init();

        Holder = GetComponentInChildren<ObjectHolder>();
        Stacker = GetComponentInChildren<Stacker>();
        WaitingLine = GetComponentInChildren<WaitingLine>();

        StateMachine.RegisterState<StateIdleStand>(Define.State.Idle, this);

        ProbType = Define.ProbType.Stand;
        State = Define.State.Idle;

        //  Todo : Refactoring
        WaitingLine.Offset = WaitingLine.transform.forward * -1f * 2f;

        //  Todo : Test
        for (int i = 0; i < 100; ++i)
        {
            Stacker.Push(Managers.Item.CreateItem(Define.ItemType.Pizza).gameObject);
        }
    }

    public IEnumerator Co_TransferStackingObjectToWaiting(float time)
    {
        float accTime = 0f;

        while (true)
        {
            accTime += Time.deltaTime;

            if (accTime >= time)
            {
                accTime -= time;

                if (IsStackerEmpty ||
                    IsWaitingLineEmpty ||
                    WaitingLine.IsTopReached() == false)
                {
                    yield return null;
                    continue;
                }

                GameObject gameObject = Stacker.Pop();
                CreatureController creature = WaitingLine.Dequeue();
                CustomerController customer = creature as CustomerController;
                if (customer == null)
                {
                    yield return null;
                    continue;
                }

                if(customer.Stacker.Push(gameObject) == false)
                {
                    yield return null;
                    continue;
                }    

                ProbController nearestProb = Managers.Prob.GetNearestProb(Define.ProbType.Table, transform.position);

                StartCoroutine(customer.Co_EatSequence((TableController)nearestProb));

                //  customer.Target.TargetObj = nearestProb.gameObject;
                //  customer.State = Define.State.Move;
            }

            yield return null;
        }
    }
}
