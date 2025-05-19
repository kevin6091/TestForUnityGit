using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMoveCustomer : StateCustomer
{
    public StateMoveCustomer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    public override void Enter()
    {
        base.Enter();

        Context.CrossfadeAnim("RUN", 0.2f);
    }

    public override void Execute()
    {
        base.Execute();

        Context.MoveToTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
