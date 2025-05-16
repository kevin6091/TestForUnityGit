using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEmployee : State
{
    EmployeeController _context;
    protected EmployeeController Context { get { return _context; } }

    public StateEmployee(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine)
    {
        _context = (EmployeeController)context;
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
