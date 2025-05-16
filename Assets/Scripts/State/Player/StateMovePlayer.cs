using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMovePlayer : StatePlayer, IKeyHandler
{
    public StateMovePlayer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    {

    }

    public override void Enter() 
    {
        Context.CrossfadeAnim("RUN", 0.2f);
        Context.LeanStacker(Quaternion.Euler(new Vector3(-5f, 0f, 0f)), 0.2f);
    }

    public override void Execute() 
    {

    }

    public override void Exit() 
    {
        Context.LeanStacker(Quaternion.Euler(new Vector3(2f, 0f, 0f)), 0.15f);
        Context.LeanStacker(Quaternion.identity, 0.1f, 0.15f);
    }

    public void OnKeyboard()
    {
        return;
    }
}
