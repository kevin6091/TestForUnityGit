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

    Animator _anim = null;
    public Animator Anim { get { return _anim; } protected set { _anim = value; } }

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

    public virtual void Init()
    {
        _stateMachine = new StateMachine();
        Anim = GetComponent<Animator>();
    }

    public void PlayAnim(string name)
    {
        _anim.Play(name);
    }

    public void CrossfadeAnim(string name, float time)
    {
        _anim.CrossFade(name, time);
    }

}
