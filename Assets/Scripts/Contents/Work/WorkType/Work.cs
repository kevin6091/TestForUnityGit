using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Work : MonoBehaviour
{
    public bool IsWorking { get; protected set; }
    public float WorkRange { get; protected set; }
    public Define.Worker Worker { get; protected set; } = Define.Worker.None;
    public Define.Worker PreWorker { get; protected set; } = Define.Worker.None;
    public Coroutine MoveToWorkCoroutine { get; set; } = null;

    public void ArrivedWork(Define.Worker worker)
    {
        Worker = worker;
        IsWorking = true;
    }

    public virtual IEnumerator Co_WorkRoutine(IEnumerator employeeEscapeRoutine)
    {
        // Todo : 각자 Work에서 해야할 Routine을 자식 클래스가 override해야함.
        yield break;
    }

    public IEnumerator Co_CheckIsWorking(IEnumerator employeeEscapeRoutine)
    {
        // 여기의 전제는 큐에서 뽑아서 알바와 매칭됐을 때 들어온다.
        while (true)
        {
            if (IsWorking == true)
            {
                if(Worker == Define.Worker.Player) // 알바가 뺏긴거다.
                {
                    StartCoroutine(employeeEscapeRoutine);
                    StopCoroutine(MoveToWorkCoroutine);
                    Managers.Work.AddWork(this);
                }
                else if (Worker == Define.Worker.Employee) // 알바가 뺏기지 않고 일에 도착했다.
                {
                    StartCoroutine(this.Co_WorkRoutine(employeeEscapeRoutine));
                }

                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator Co_MoveToWorkRoutine(EmployeeController employee, IEnumerator employeeEscapeRoutine)
    {
        yield return null;

        employee.Target.TargetObj = gameObject;
        employee.State = Define.State.Move;
        employee.Target.Range = WorkRange = 0.25f;

        while (true)
        {
            if ((transform.position - employee.transform.position).magnitude <= WorkRange)
            {
                ArrivedWork(Define.Worker.Employee);

                yield break;
            }

            yield return null;
        }
    }
}
