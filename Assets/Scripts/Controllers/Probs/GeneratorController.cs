using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : ProbController
{
    public Stacker Stacker { get; private set; } = null;

    public float GenerateSpeed { get; set; } =  1f;
    public int MaxCount { get; set; } = 6;

    public override void Init()
    {
        base.Init();

        Stacker = GetComponentInChildren<Stacker>();
        StateMachine.RegisterState<StateIdleGenerator>(Define.State.Idle, this);
        StateMachine.RegisterState<StateWaitGenerator>(Define.State.Wait, this);
        State = Define.State.Idle;
    }

    #region For Future. Upgrade
    private void Upgrade()
    {

    }
    private void ChangeModel()
    {
    }
    #endregion
}
