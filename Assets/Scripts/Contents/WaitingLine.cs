using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaitingLine : MonoBehaviour
{
    private LinkedList<CreatureController> _lineObjects = new LinkedList<CreatureController>();
    private Vector3 _offset = Vector3.zero;

    public Vector3 Offset { get { return _offset; } set { _offset = value; } }


    public int Count { get { return _lineObjects.Count; } }
    public bool IsEmpty { get { return Count == 0; } }
    public bool IsTopReached()
    {
        if (IsEmpty)
            return false;

        CreatureController creature = _lineObjects.First.Value;
        return creature.IsReachedTarget();
    }

    public void Enqueue(CreatureController creature)
    {
        creature.Target.TargetObj = gameObject;
        creature.Target.Offset = Offset * _lineObjects.Count;
        creature.State = Define.State.Move;

        _lineObjects.AddLast(creature);
    }

    public CreatureController Dequeue()
    {
        CreatureController creature = null;
        if (_lineObjects.Count <= 0)
            return creature;

        creature = _lineObjects.First.Value;
        _lineObjects.RemoveFirst();

        CreatureController[] lineObjects = _lineObjects.ToArray();
        foreach(CustomerController customerController in lineObjects)
        {
            customerController.Target.Offset -= Offset;
            customerController.State = Define.State.Move;
        }             

        return creature;
    }
}
