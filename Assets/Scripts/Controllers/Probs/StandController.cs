using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StandController : ProbController
{
    private Stacker _stacker = null;
    private ObjectHolder _holder = null;
    private WaitingLine _waitingLine = null;

    public override void Init()
    {
        base.Init();

        _holder = GetComponentInChildren<ObjectHolder>();
        _stacker = GetComponentInChildren<Stacker>();
        _waitingLine = GetComponentInChildren<WaitingLine>();

        StateMachine.RegisterState<StateIdleStand>(Define.State.Idle, this);

        State = Define.State.Idle;

        //TEST Zone
        _waitingLine.Offset = _waitingLine.transform.forward * -1f * 2f;
        Managers.Input.Actions[(Define.InputEvent.KeyEvent, Define.InputType.Down)] -= OnKeyboard;
        Managers.Input.Actions[(Define.InputEvent.KeyEvent, Define.InputType.Down)] += OnKeyboard;
    }

    private void Update()
    {
    }

    private void OnKeyboard(object[] objects)
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameObject go = GameObject.Find("Customer");
            if (go == null)
                return;

            CustomerController controller = go.GetComponent<CustomerController>();
            _waitingLine.Enqueue(controller);
        }
    }
}
