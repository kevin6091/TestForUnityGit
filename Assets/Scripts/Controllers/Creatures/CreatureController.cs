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

        float dist = Mathf.Min(Stat.MoveSpeed * Time.deltaTime, dir.magnitude);
        dir = dir.normalized;
        transform.position += dir * dist;
    }

    public IEnumerator Co_MoveToTarget()
    {
        float dist = Mathf.Infinity;

        while (dist > Mathf.Epsilon)
        {
            if(Target.Direction(out Vector3 newDir))
                yield break;

            dist = Mathf.Min(Stat.MoveSpeed * Time.deltaTime, newDir.magnitude);
            newDir = newDir.normalized;

            transform.position += newDir * dist;

            yield return null;
        }
    }

    public void RotateToTarget()
    {
        if (false == Target.Direction(out Vector3 dir) ||
            dir.magnitude <= /*Mathf.Epsilon */0.0001f)
            return;

        Quaternion rotateSrc = transform.rotation;
        Quaternion rotateDst = Quaternion.LookRotation(dir);
        float rotateAngle = Mathf.Min(Stat.RotateSpeed * Time.deltaTime, Quaternion.Angle(rotateSrc, rotateDst));

        Quaternion rotation = Quaternion.RotateTowards(rotateSrc, rotateDst, rotateAngle);

        transform.rotation = rotation;
    }

    public bool IsReachedTarget()
    {
        if(Target.Position(out Vector3 targetPos))
            return (targetPos - transform.position).magnitude < Mathf.Epsilon;

        return true;
    }
}
