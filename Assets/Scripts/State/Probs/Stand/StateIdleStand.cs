using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleStand : StateStand
{
    public StateIdleStand(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

        Context.TransferStackingObjectToWaiting();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
