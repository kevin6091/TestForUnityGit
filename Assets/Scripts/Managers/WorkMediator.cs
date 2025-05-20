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
            CoroutineHelper.CoOutInfo outInfo =  CoroutineHelper.MyStartCoroutine(work, work.Co_MoveToWorkRoutine(employee.transform, employee.Stat.MoveSpeed));
            CoroutineHelper.MyStartCoroutine(work, work.Co_CheckIsWorking(employee.WorkEscapeRoutine(), outInfo.OutRoutine));
        }
    }
}
