using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureController : MonoBehaviour
{

    [SerializeField]
    protected Vector3 _destPos = new Vector3();

    [SerializeField]
    protected GameObject _lockTarget = null;

    [SerializeField]

    protected StateMachine _stateMachine = null;

    public Define.State State
    {
        get { return _stateMachine.CurStateType(); }
        set
        {
            _stateMachine.ChangeState(value);
        }
    }

    public StateMachine StateMachine { get { return _stateMachine; } }

    private void Start()
    {
        Init(); 
    }

    public abstract void Init();

    void Update()
    {
        switch (State)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Move:
                UpdateMoving();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
    }

    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateSkill() { }
}
