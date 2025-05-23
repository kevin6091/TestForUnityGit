using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateMoveCustomer : StateCustomer
{
    GameObject _curTarget = null;
    public StateMoveCustomer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    public override void Enter()
    {
        base.Enter();

        Context.CrossfadeAnim("RUN", 0.2f);
        Context.LeanStacker(Quaternion.Euler(new Vector3(-5f, 0f, 0f)), 0.2f);

        UpdatePathToTarget();
    }

    public override void Execute()
    {
        base.Execute();

        Context.RotateToTarget();
        Context.UpdateArm();

        if(NavMesh.Raycast(Context.transform.position, Context.transform.position + Context.transform.forward, out NavMeshHit hit, NavMesh.AllAreas))
        {
            int obstacleArea = NavMesh.GetAreaFromName("Obstacle"); // 장애물 영역 ID 가져오기
            if ((hit.mask & (1 << obstacleArea)) != 0)
            {
                Debug.Log("이 위치는 장애물입니다!");
                Context.Agent.ResetPath();
            }
        }

        if(Context.IsSameTargetObject(_curTarget))
        {
            UpdatePathToTarget();
        }

        if (Context.IsReachedTarget())
            Context.State = Define.State.Idle;
    }

    public override void Exit()
    {
        base.Exit();

        Context.LeanStacker(Quaternion.Euler(new Vector3(2f, 0f, 0f)), 0.15f);
        Context.LeanStacker(Quaternion.identity, 0.1f, 0.15f);

        Context.Agent.ResetPath();
        _curTarget = null;
    }

    private void UpdatePathToTarget()
    {
        Context.SetDestinationToTarget();
        _curTarget = Context.Target.TargetObj;
    }
}
