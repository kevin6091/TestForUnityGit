using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureController : MonoBehaviour
{

    [SerializeField]
    public Vector3 TargetPos { get; set; } = new Vector3();

    [SerializeField]
    public GameObject LockTarget { get; set; } = null;

    [SerializeField]

    protected StateMachine _stateMachine = null;

    public Define.State State
    {
        get { return _stateMachine.CurStateType(); }
        set { _stateMachine.ChangeState(value); }
    }

    public StateMachine StateMachine { get { return _stateMachine; } }

    private void Start()
    {
        Init(); 
    }

    private void Update()
    {
        _stateMachine.Execute();
    }

    public abstract void Init();
}
