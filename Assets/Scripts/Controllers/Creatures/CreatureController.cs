using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  3D 오브젝트
//  리깅할 오브젝트 => 캐릭터

public abstract class CreatureController : MonoBehaviour
{

    [SerializeField]
    public GameObject LockTarget { get; set; } = null;

    Animator _anim = null;
    public Animator Anim { get { return _anim; } protected set { _anim = value; } }
    public TargetHolder Target { get; private set; }
    public Stat Stat { get; protected set; }

    protected StateMachine _stateMachine = null;
    public StateMachine StateMachine { get { return _stateMachine; } private set { _stateMachine = value; } }

    public Define.State State
    {
        get { return _stateMachine.CurStateType(); }
        set { _stateMachine.ChangeState(value); }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        StateMachine.Execute();
    }

    public virtual void Init()
    {
        StateMachine = new StateMachine();
        Anim = GetComponent<Animator>();
        Target = gameObject.GetOrAddComponent<TargetHolder>();
        Stat = gameObject.GetOrAddComponent<Stat>();
    }

    public void PlayAnim(string name)
    {
        _anim.Play(name);
    }

    public void CrossfadeAnim(string name, float time)
    {
        _anim.CrossFade(name, time);
    }

    public void MoveToTarget()
    {
        if (false == Target.Direction(out Vector3 dir))
            return;

        transform.position += dir.normalized * Stat.MoveSpeed;
    }
}
