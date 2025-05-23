using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleStand : StateStand
{
    public StateIdleStand(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    { }

    float _accTime = 0f;
    float _coolTime = 0.5f;
    CustomerController _lastCustomer = null;

    public override void Enter()
    {
        base.Enter();
        _accTime = 0f;
    }

    public override void Execute()
    {
        base.Execute();

        _accTime += Time.deltaTime;

        if (_accTime >= _coolTime)
        {
            _accTime -= _coolTime;

            if (Context.IsStackerEmpty ||
                Context.IsWaitingLineEmpty ||
                Context.WaitingLine.IsTopReached() == false)
            {
                return;
            }

            CreatureController creature = Context.WaitingLine.Peek();
            CustomerController customer = creature as CustomerController;

            if (!customer.Needs.IsEnough)
            {
                GameObject gameObject = Context.Stacker.Pop();
                _lastCustomer = customer;
                customer.Stacker.Push(gameObject);
            }
        }

        if(_lastCustomer != null &&
            _lastCustomer.State == Define.State.Move)
        {
            Context.WaitingLine.Dequeue();
            _lastCustomer = null;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
