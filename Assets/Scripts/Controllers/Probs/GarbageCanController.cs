using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GarbageCanController : ProbController
{
    public Stacker Stacker { get; private set; } = null;
    public override void Init()
    {
        base.Init();

        Stacker = GetComponentInChildren<Stacker>();

        StateMachine.RegisterState<StateIdleGenerator>(Define.State.Idle, this);
        State = Define.State.Idle;
    }
    protected override void Update()
    {
        StateMachine.Execute();
    }

}

