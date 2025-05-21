using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//  3D 오브젝트
//  리깅할 오브젝트 => 캐릭터

public abstract class CreatureController : MonoBehaviour
{

    [SerializeField]
    public GameObject LockTarget { get; set; } = null;

    NavMeshAgent _nma = null;
    Animator _anim = null;
    public NavMeshAgent Agent { get { return _nma; } private set { _nma = value; } }
    public Animator Anim { get { return _anim; } private set { _anim = value; } }
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
        LateInit();
    }

    private void Update()
    {
        StateMachine.Execute();
    }

    public virtual void Init()
    {
        StateMachine = new StateMachine();
        Anim = GetComponent<Animator>();
        Agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        Target = gameObject.GetOrAddComponent<TargetHolder>();
        Stat = gameObject.GetOrAddComponent<Stat>();
    }

    public virtual void LateInit()
    {
        Stat.SyncNavMeshAgent(Agent);
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
        if (false == Target.DirectionXZ(out Vector3 dir))
            return;

        float dist = Mathf.Min(Stat.MoveSpeed * Time.deltaTime, dir.magnitude);
        dir = dir.normalized;
        transform.position += dir * dist;
    }

    public IEnumerator Co_MoveToTarget()
    {
        float dist = Mathf.Infinity;

        while (true)
        {
            if (Target.DirectionXZ(out Vector3 dir) == false ||
                IsReachedTarget())
            {
                CoroutineHelper.RemoveCoroutineDict(this, Co_MoveToTarget());
                yield break;
            }

            dist = Mathf.Min(Stat.MoveSpeed * Time.deltaTime, dir.magnitude);
            dir = dir.normalized;

            transform.position += dir * dist;

            yield return null;
        }
    }

    //  TODO: UnreadyMthod Make it ( IEnumerator Co_MovePath(NavMeshPath path) )
    //public IEnumerator Co_MovePath(NavMeshPath path)
    //{
    //    int cornerIdx = 0;
    //    while (true)
    //    {
    //        if(cornerIdx >= path.corners.Length)
    //        {
    //            CoroutineHelper.RemoveCoroutineDict(this, Co_MoveToTarget());
    //            yield break;
    //        }

    //        float curMoveDist = Mathf.

    //        dist = Mathf.Min(Stat.MoveSpeed * Time.deltaTime, dir.magnitude);
    //        dir = dir.normalized;

    //        transform.position += dir * dist;

    //        yield return null;
    //    }
    //}

    public void SetDestinationToTarget()
    {
        if (Agent == null)
            return;

        if (Target.Position(out Vector3 targetPos) == false)
            return;

        if(NavMesh.SamplePosition(targetPos, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            Debug.Log(hit.position);
            Agent.SetDestination(hit.position);
        }
    }

    public void RotateToTarget()
    {
        if (false == Target.DirectionXZ(out Vector3 dir) ||
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
        if(Target.DirectionXZ(out Vector3 dir))
            return dir.magnitude < Mathf.Epsilon;

        return true;
    }
}
