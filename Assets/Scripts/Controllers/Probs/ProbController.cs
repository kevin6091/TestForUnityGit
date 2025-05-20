using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class ProbController : MonoBehaviour
{
    protected StateMachine _stateMachine = null;
    public StateMachine StateMachine { get { return _stateMachine; } private set { _stateMachine = value; } }

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
    }
}
