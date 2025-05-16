using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMovePlayer : StatePlayer, IKeyHandler
{
    public StateMovePlayer(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    {

    }

    public override void Enter() 
    {
        Context.CrossfadeAnim("RUN", 0.2f);
        Context.LeanStacker(Quaternion.Euler(new Vector3(-5f, 0f, 0f)), 0.2f);
    }

    public override void Execute() 
    {
        Vector3 dir;
        float dist;

        if (Context.LockTarget != null)
        {
            Context.TargetPos = Context.LockTarget.transform.position;

            dir = Context.TargetPos - Context.transform.position;
            dist = dir.magnitude;

            if ((Context.TargetPos - Context.transform.position).magnitude <= 1f)
            {
                Context.State = Define.State.Skill;
                return;
            }
        }
        else
        {
            dir = Context.TargetPos - Context.transform.position;
            dist = dir.magnitude;
        }


        if (dist < 0.1f)
            Context.State = Define.State.Idle;

        else
        {
            NavMeshAgent nma = Context.gameObject.GetComponent<NavMeshAgent>();
            //  nma.CalculatePath();

            //  Move
            float moveDist = Mathf.Clamp(Context.Stat.MoveSpeed * Time.deltaTime, 0f, dist);

            Debug.DrawRay(Context.transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(Context.transform.position + Vector3.up * 0.5f, dir, 1f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    Context.State = Define.State.Idle;
                return;
            }

            nma.Move(dir.normalized * moveDist);

            //  Rotate
            Context.transform.rotation = Quaternion.RotateTowards(Context.transform.rotation, Quaternion.LookRotation(dir), Context.Stat.RotateSpeed * Time.deltaTime);
        }
    }

    public override void Exit() 
    {
        Context.LeanStacker(Quaternion.Euler(new Vector3(2f, 0f, 0f)), 0.15f);
        Context.LeanStacker(Quaternion.identity, 0.1f, 0.15f);
    }

    public void OnKeyboard()
    {
        return;
    }
}
