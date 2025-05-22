using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkMediator : BaseManager
{
    public override void OnUpdate()
    {
        Work work = Managers.Work.TryPeekToWorks;
        EmployeeController employee = Managers.Employee.TryPeekEmployees;

        if (work != null && employee != null)
        {
            work = Managers.Work.DeQueueToWorks;
            employee = Managers.Employee.DeQueueEmployees;

            IEnumerator eacaperoutine = employee.Co_WorkEscapeRoutine();

            // �˹ٸ� Work�� �̵���Ű�� �ڷ�ƾ ����
            work.MoveToWorkCoroutine = work.StartCoroutine(work.Co_MoveToWorkRoutine(employee, eacaperoutine));
            work.StartCoroutine(work.Co_CheckIsWorking(eacaperoutine));
        }
    }
}