using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : ProbController
{
    private Stacker _stacker = null;
    private List<ObjectHolder> _holders = new List<ObjectHolder>();

    public override void Init()
    {
        base.Init();

        ObjectHolder[] holders = transform.GetComponentsInChildren<ObjectHolder>();
        foreach(ObjectHolder holder in holders) 
            _holders.Add(holder);

        _stacker = GetComponentInChildren<Stacker>();

        StateMachine.RegisterState<StateIdleStand>(Define.State.Idle, this);

        State = Define.State.Idle;

       
    }

    private void Update()
    {
    }
}
