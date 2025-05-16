using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdlePlayer : StatePlayer, IKeyHandler
{
    public StateIdlePlayer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    public override void Enter() 
    {
        base.Enter();

        Context.CrossfadeAnim("WAIT", 0.2f);
    }

    public override void Execute() 
    {
        base.Execute();

        Context.UpdateArm();
    }

    public override void Exit() 
    { 
        base.Exit();
    }

    public void OnKeyboard()
    {
        return;
    }
}
