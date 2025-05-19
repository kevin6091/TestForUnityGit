using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingLine : MonoBehaviour
{
    private LinkedList<CustomerController> _lineObjects = new LinkedList<CustomerController>();
    private Vector3 _offset = Vector3.zero;

    public Vector3 Offset { get { return _offset; } set { _offset = value; } }
    
    public void Enqueue(CustomerController customer)
    {
        customer.Target.TargetObj = gameObject;
        customer.Target.Offset = Offset * _lineObjects.Count;
        customer.State = Define.State.Move;

        _lineObjects.AddLast(customer);
    }

    public CustomerController Dequeue()
    {
        CustomerController customer = null;
        if (_lineObjects.Count <= 0)
            return customer;

        customer = _lineObjects.First.Value;
        _lineObjects.RemoveFirst();
        return customer;
    }

    public int Count()
    {
        return _lineObjects.Count;
    }
}
