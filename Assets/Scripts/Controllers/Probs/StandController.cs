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
        for(int i = 0; i < 100; ++i)
        {
            GameObject pizza = Managers.Resource.Instantiate("Foods/Pizza");
            Stacker.Push(pizza);
        }
    }

    public void TransferStackingObjectToWaiting()
    {
        if (IsStackerEmpty ||
            IsWaitingLineEmpty ||
            WaitingLine.IsTopReached() == false)
        {
            return;
        }

        GameObject gameObject = Stacker.Pop();
        CreatureController creature = WaitingLine.Dequeue();
        CustomerController customer = creature as CustomerController;
        if (customer == null)
        {
            return;
        }

        customer.Stacker.Push(gameObject);

        ProbController nearestProb = Managers.Prob.GetNearestProb(Define.ProbType.Table, transform.position);

        customer.State = Define.State.Move;
        customer.Target.TargetObj = nearestProb.gameObject;
    }
}
