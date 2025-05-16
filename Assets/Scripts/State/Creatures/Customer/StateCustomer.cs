using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCustomer : State
{
    CustomerController _context;
    protected CustomerController Context { get { return _context; } }

    public StateCustomer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine)
    {
        _context = (CustomerController)context;
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
