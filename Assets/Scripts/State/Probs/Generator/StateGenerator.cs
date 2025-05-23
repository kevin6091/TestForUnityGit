using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class StateGenerator : State
{
    GeneratorController _context;

    protected GeneratorController Context { get => _context; }

    public StateGenerator(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine)
    {
        _context = (GeneratorController)context;
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

