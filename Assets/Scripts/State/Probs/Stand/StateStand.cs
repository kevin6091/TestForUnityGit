using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateStand : State
{
    StandController _context;
    protected StandController Context { get { return _context; } }

    public StateStand(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine)
    {
        _context = (StandController)context;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
