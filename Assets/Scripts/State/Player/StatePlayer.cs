using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StatePlayer : State 
{
    PlayerController _context;
    protected PlayerController Context { get {  return _context; } }

    public StatePlayer(StateMachine stateMachine, MonoBehaviour context) : base (stateMachine)
    {
        _context = (PlayerController)context;
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