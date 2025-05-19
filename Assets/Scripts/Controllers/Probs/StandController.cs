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

        List<GameObject> matchingObjects = new List<GameObject>();
        GameObject[] allObjects = FindObjectsOfType<GameObject>(); // 씬 내 모든 활성 오브젝트 검색

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Customer") // 이름 비교
            {
                customers.Add(obj.GetComponent<CustomerController>());
            }
        }

    }

    private void Update()
    {
    }

    List<CustomerController> customers = new List<CustomerController>();
    int index = 0;
    private void OnKeyboard(object[] objects)
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            _waitingLine.Enqueue(customers[index++]);
            while (index >= customers.Count)
                --index;
        }
    }
}
