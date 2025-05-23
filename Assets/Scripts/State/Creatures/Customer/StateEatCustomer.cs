using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEatCustomer : StateCustomer
{
    public StateEatCustomer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    public override void Enter()
    {
        base.Enter();

        Context.CrossFadeSeparateAnim("EAT", "SIT", 0.2f);
    }

    public override void Execute()
    {
        base.Execute();

        Context.UpdateArm();
    }

    public override void Exit()
    {
        base.Exit();

        Context.EndSeparateAnim(0.2f);
    }
}
