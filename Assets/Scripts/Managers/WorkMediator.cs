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

            // 알바를 Work로 이동시키는 코루틴 실행
            CoroutineHelper.CoOutInfo outInfo = CoroutineHelper.MyStartCoroutine(work, work.Co_MoveToWorkRoutine(employee, employee.Co_WorkEscapeRoutine()), false);
            CoroutineHelper.MyStartCoroutine(work, work.Co_CheckIsWorking(employee.Co_WorkEscapeRoutine(), outInfo.OutRoutine), false);
        }
    }
}