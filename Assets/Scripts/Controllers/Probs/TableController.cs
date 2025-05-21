using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : ProbController
{
    public Stacker Stacker { get; private set; } = null;
    public List<ObjectHolder> _holders = new List<ObjectHolder>();

    public override void Init()
    {
        base.Init();

        ObjectHolder[] holders = transform.GetComponentsInChildren<ObjectHolder>();
        foreach(ObjectHolder holder in holders) 
            _holders.Add(holder);

        Stacker = GetComponentInChildren<Stacker>();
        StateMachine.RegisterState<StateIdleTable>(Define.State.Idle, this);
        StateMachine.RegisterState<StateCorruptTable>(Define.State.Corrupt, this);

        ProbType = Define.ProbType.Table;
        State = Define.State.Idle;
    }

    public static bool HasEmptySeat(TableController controller)
    {
        if (controller == null)
        {
            return false;
        }

        foreach(ObjectHolder holder in controller._holders)
        {
            if (!holder.IsEmpty)
                return false;
        }

        return true;
    }
}
