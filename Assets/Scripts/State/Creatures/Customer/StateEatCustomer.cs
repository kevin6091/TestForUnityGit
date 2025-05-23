using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateEatCustomer : StateCustomer
{
    public StateEatCustomer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    public override void Enter()
    {
        base.Enter();

        Context.CrossFadeSeparateAnim("EAT", "SIT", 0.2f);

        Context.Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        Context.StartCoroutine(Context.Co_LerpToObject_Wrap(Context.Target.TargetObj, 0.5f));
        Context.StartCoroutine(Context.Co_SlerpRotationToObject(Context.Target.TargetObj, 0.5f));
    }

    public override void Execute()
    {
        base.Execute();

        Context.UpdateArm();
    }

    public override void Exit()
    {
        base.Exit();

        Context.Agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;

        Context.EndSeparateAnim(0.2f);
    }
}
