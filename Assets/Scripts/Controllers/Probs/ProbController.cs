using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public abstract class ProbController : MonoBehaviour
{
    protected StateMachine _stateMachine = null;
    public StateMachine StateMachine { get { return _stateMachine; } private set { _stateMachine = value; } }
    public NavMeshObstacle Obstacle { get; private set; } = null;

    public Define.ProbType ProbType { get; protected set; } = Define.ProbType.END;

    protected Work Work { get; set; } = null;

    public Define.State State
    {
        get { return _stateMachine.CurStateType(); }
        set { _stateMachine.ChangeState(value); }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        StateMachine.Execute();
    }

    public virtual void Init()
    {
        StateMachine = new StateMachine();
        Obstacle = gameObject.GetOrAddComponent<NavMeshObstacle>();

        Obstacle.carving = true;
        Work = transform.GetComponentInChildren<Work>();
        
    }

    protected void OnStackerEmpty()
    {
        Work.OnStackerEmpty();
    }

    protected void OnStackerPush()
    {
        Work.OnStackerPush();
    }
}
