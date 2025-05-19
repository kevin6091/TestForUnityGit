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
        Context.LeanStacker(Quaternion.Euler(new Vector3(-5f, 0f, 0f)), 0.2f);
    }

    public override void Execute()
    {
        base.Execute();

        Context.MoveToTarget();
        Context.RotateToTarget();
        Context.UpdateArm();

        if (Context.IsReachedTarget())
            Context.State = Define.State.Idle;
    }

    public override void Exit()
    {
        base.Exit();

        Context.LeanStacker(Quaternion.Euler(new Vector3(2f, 0f, 0f)), 0.15f);
        Context.LeanStacker(Quaternion.identity, 0.1f, 0.15f);
    }
}
