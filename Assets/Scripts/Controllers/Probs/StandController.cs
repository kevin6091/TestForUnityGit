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

        WaitingLine.topReachedEvents += OnWaitingLineTopReached;

        //  Todo : Refactoring
        WaitingLine.Offset = WaitingLine.transform.forward * -1f * 2f;

        //  Todo : Test
        for (int i = 0; i < 100; ++i)
        {
            Stacker.Push(Managers.Item.CreateItem(Define.ItemType.Pizza).gameObject);
        }
    }

    private void OnWaitingLineTopReached()
    {

    }
}
