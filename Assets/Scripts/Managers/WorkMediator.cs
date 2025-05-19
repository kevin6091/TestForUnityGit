using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkMediator : BaseManager
{
    public override void OnUpdate()
    {
        Work work = Managers.Work.TryPeekToWorks;
        EmployeeController employee = Managers.Employee.TryPeekEmployees;

        if(work != null && employee != null)
        {
            // 알바를 Work로 이동시키는 코루틴 실행
            work.StartCoroutine(work.Co_MoveToWorkRoutine(employee.transform, employee.Stat.MoveSpeed));
            work.StartCoroutine(work.Co_CheckIsWork(employee.WorkEscapeRoutine()));
        }
    }
}
