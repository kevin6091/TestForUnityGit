using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCorruptTable : StateTable
{
    public StateCorruptTable(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
