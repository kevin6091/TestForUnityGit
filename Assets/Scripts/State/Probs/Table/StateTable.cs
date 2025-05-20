using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateTable : State
{
    TableController _context;
    protected TableController Context { get { return _context; } }

    public StateTable(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine)
    {
        _context = (TableController)context;
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