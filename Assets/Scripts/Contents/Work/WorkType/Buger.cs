using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buger : Work
{
    void Start()
    {
        Managers.Work.AddWork(this);
    }

    void Update()
    {
        
    }

    public override IEnumerator Co_WorkRoutine(IEnumerator employeeWorkDoneRoutine)
    {
        #region Buger Generator 
        // BugerGenerator Stacker -> Employee Stacker
        while (IsWorkDone == false && Employee.Stat.MaxStackSize > Employee.Stacker.Count) 
        {
            if(Stacker.IsEmpty == false)
                Employee.Stacker.Push(Stacker.Pop());

            yield return new WaitForSeconds(0.1f);
        }
        #endregion

        #region BugerStacker
        // Employee Stacker -> Stand Stacker
        StandController standController = Managers.Prob.GetNearestProb(Define.ProbType.Stand, Employee.transform.position) as StandController;
        Employee.Target.TargetObj = standController.Holder.gameObject;
        Employee.State = Define.State.Move;

        while (Employee.Stacker.IsEmpty == false && Employee.State == Define.State.Idle)
        {
            standController.Stacker.Push(Employee.Stacker.Pop());
            yield return new WaitForSeconds(0.1f);
        }
        #endregion

        if (Worker == Define.Worker.Employee)
        {
            if (employeeWorkDoneRoutine != null)
                StartCoroutine(employeeWorkDoneRoutine);
            Managers.Resource.Instantiate("Test/WorkDoneParticle", transform);
        }

        if(Worker == Define.Worker.Employee)
            AddWork();

        LeaveWork();

        yield break;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        CheckPlayer();
    }

    protected override void OnTriggerStay(Collider other)
    {
        
    }

    protected override void OnTriggerExit(Collider other)
    {
        LeaveWork();
    }
}
