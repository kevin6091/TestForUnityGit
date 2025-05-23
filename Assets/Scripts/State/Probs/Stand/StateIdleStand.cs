using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleStand : StateStand
{
    public StateIdleStand(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    Coroutine coroutineHandle = null;

    public override void Enter()
    {
        base.Enter();

        coroutineHandle = Context.StartCoroutine(Context.Co_TransferStackingObjectToWaiting(0.5f));
    }

    public override void Execute()
    {
        base.Execute();

    }

    public override void Exit()
    {
        base.Exit();

        if(coroutineHandle != null)
        {
            Context.StopCoroutine(coroutineHandle);
            coroutineHandle = null;
        }
    }
}
