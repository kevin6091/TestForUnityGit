using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandController : ProbController
{
    private Stacker _stacker = null;
    private ObjectHolder _holder = null;

    public override void Init()
    {
        base.Init();

        _holder = GetComponentInChildren<ObjectHolder>();
        _stacker = GetComponentInChildren<Stacker>();

        StateMachine.RegisterState<StateIdleStand>(Define.State.Idle, this);

        State = Define.State.Idle;
    }
}
