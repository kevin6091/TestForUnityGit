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

    public Define.State State
    {
        get { return _stateMachine.CurStateType(); }
        set { _stateMachine.ChangeState(value); }
    }

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        StateMachine.Execute();
    }

    public virtual void Init()
    {
        StateMachine = new StateMachine();
        Obstacle = gameObject.GetOrAddComponent<NavMeshObstacle>();

        Obstacle.carving = true;
    }
}
