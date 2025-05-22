using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Work : MonoBehaviour
{
    public bool IsWorking { get; protected set; }
    public float WorkRange { get; protected set; }
    public Define.Worker Worker { get; protected set; }

    public void Co_ArrivedWork(Define.Worker worker)
    {
        Worker = worker;
        IsWorking = true;
    }

    public virtual IEnumerator Co_WorkRoutine(IEnumerator employeeEscapeRoutine)
    {
        // Todo : 각자 Work에서 해야할 Routine을 자식 클래스가 override해야함.
        yield return null;
        yield break;
    }

    public IEnumerator Co_CheckIsWorking(IEnumerator employeeEscapeRoutine, IEnumerator moveToWorkRoutine)
    {
        while (true)
        {
            if (IsWorking == true)
            {
                CoroutineHelper.MyStopCoroutine(this, moveToWorkRoutine);

                if(Worker == Define.Worker.Player)
                {
                    StartCoroutine(employeeEscapeRoutine);
                    Managers.Work.AddWork(this);
                }

                CoroutineHelper.RemoveCoroutine(this, Co_CheckIsWorking(null, null));
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator Co_MoveToWorkRoutine(EmployeeController employee, IEnumerator employeeEscapeRoutine)
    {
        employee.Target.TargetObj = gameObject;
        employee.State = Define.State.Move;
        employee.Target.Range = WorkRange = 0.25f;

        while (true)
        {
            if ((transform.position - employee.transform.position).magnitude <= WorkRange)
            {
                Co_ArrivedWork(Define.Worker.Employee);
                StartCoroutine(this.Co_WorkRoutine(employeeEscapeRoutine));

                yield return null;
                CoroutineHelper.RemoveCoroutine(this, Co_MoveToWorkRoutine(null, null));
                yield break;
            }

            yield return null;
        }
    }
}
