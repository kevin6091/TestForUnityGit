using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StandController : ProbController
{
    public Stacker Stacker { get; private set; } = null;
    public ObjectHolder Holder { get; private set; } = null;
    public WaitingLine WaitingLine { get; private set; } = null;

    public override void Init()
    {
        base.Init();

        Holder = GetComponentInChildren<ObjectHolder>();
        Stacker = GetComponentInChildren<Stacker>();
        WaitingLine = GetComponentInChildren<WaitingLine>();

        StateMachine.RegisterState<StateIdleStand>(Define.State.Idle, this);

        State = Define.State.Idle;

        //  Todo : Refactoring
        //  TEST Zone
        WaitingLine.Offset = WaitingLine.transform.forward * -1f * 2f;
    }
}
