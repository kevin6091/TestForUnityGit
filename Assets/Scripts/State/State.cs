using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;

    public State(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public virtual void Enter() 
    { 
        
    }

    public virtual void Execute() { }

    public virtual void Exit() 
    { 

    }
}