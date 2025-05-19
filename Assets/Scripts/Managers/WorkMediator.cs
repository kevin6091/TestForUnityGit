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
            // �˹ٸ� Work�� �̵���Ű�� �ڷ�ƾ ����
            work.StartCoroutine(work.Co_MoveToWorkRoutine(employee.transform, employee.Stat.MoveSpeed));
            work.StartCoroutine(work.Co_CheckIsWork(employee.WorkEscapeRoutine()));
        }
    }
}
