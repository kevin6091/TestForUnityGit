using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateWaitGenerator : StateGenerator
{
    public StateWaitGenerator(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

        if(Context.Stacker.Count < Context.MaxCount)
        {
            Context.State = Define.State.Idle;
        }
    }

    public override void Exit()
    {
        base.Exit();

    }

}

