using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : ProbController
{
    public Stacker Stacker { get; private set; } = null;
    private List<ObjectHolder> _holders = new List<ObjectHolder>();

    public override void Init()
    {
        base.Init();

        ObjectHolder[] holders = transform.GetComponentsInChildren<ObjectHolder>();
        foreach(ObjectHolder holder in holders)
        {
            _holders.Add(holder);
        }

        Stacker = GetComponentInChildren<Stacker>();
        StateMachine.RegisterState<StateIdleTable>(Define.State.Idle, this);
        StateMachine.RegisterState<StateCorruptTable>(Define.State.Corrupt, this);

        ProbType = Define.ProbType.Table;
        State = Define.State.Idle;
    }

    public void ClearSeat()
    {
        foreach(ObjectHolder holder in _holders)
        {
            holder.HoldObject = null;
        }
    }

    public static bool HasEmptySeat(ProbController prob)
    {
        if (prob == null)
        {
            return false;
        }

        TableController table = prob as TableController;
        if(table == null)
        {
            return false;
        }    

        foreach(ObjectHolder holder in table._holders)
        {
            if (holder.IsEmpty)
                return true;
        }

        return false;
    }

    public ObjectHolder GetEmptySeat()
    {
        foreach(ObjectHolder holder in _holders)
        {
            if(holder.IsEmpty)
            {
                return holder;
            }
        }

        return null;
    }

    //public static GameObject GetSeatObject(TableController controller)
    //{

    //}
}
