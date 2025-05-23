using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Work : MonoBehaviour
{
    public bool IsWorking { get; protected set; }
    public float WorkRange { get; protected set; }
    public Define.Worker Worker { get; protected set; } = Define.Worker.None;
    public Define.Worker PreWorker { get; protected set; } = Define.Worker.None;
    public Coroutine MoveToWorkCoroutine { get; set; } = null;
    public EmployeeController Employee { get; set; } = null;

    public bool IsWorkDone { get; protected set; }
    public Stacker Stacker { get; set; }

    protected void ArrivedWork(Define.Worker worker)
    {
        Worker = worker;
        IsWorking = true;
    }

    protected void LeaveWork()
    {
        Worker = Define.Worker.None;
        IsWorking = false;
    }

    protected void AddWork()
    {
        Managers.Work.AddWork(this);
        Employee = null;
    }

    public virtual IEnumerator Co_WorkRoutine(IEnumerator employeeEscapeRoutine)
    {
        // Todo : ���� Work���� �ؾ��� Routine�� �ڽ� Ŭ������ override�ؾ���.
        yield break;
    }

    public IEnumerator Co_CheckIsWorking(IEnumerator employeeEscapeRoutine)
    {
        // ������ ������ ť���� �̾Ƽ� �˹ٿ� ��Ī���� �� ���´�.
        while (true)
        {
            if (IsWorking == true)
            {
                if(Worker == Define.Worker.Player) // �˹ٰ� ����Ŵ�.
                {
                    StartCoroutine(employeeEscapeRoutine);
                    StopCoroutine(MoveToWorkCoroutine);
                    AddWork();
                }
                else if (Worker == Define.Worker.Employee) // �˹ٰ� ������ �ʰ� �Ͽ� �����ߴ�.
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
                Employee = employee;

                yield break;
            }

            yield return null;
        }
    }

    protected void CheckPlayer()
    {
        if (Worker == Define.Worker.None)
        {
            ArrivedWork(Define.Worker.Player);
            Managers.Resource.Instantiate("Test/WorkDoneParticleRed", transform);
        }
    }

    public void OnStackerEmpty()
    {
        IsWorkDone = true;
    }

    public void OnStackerPush()
    {
        IsWorkDone = false;
    }

    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerStay(Collider other);
    protected abstract void OnTriggerExit(Collider other);
}
