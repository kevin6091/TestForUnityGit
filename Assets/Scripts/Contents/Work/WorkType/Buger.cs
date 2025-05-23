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
        // Todo : Test 
        if(Worker == Define.Worker.None && (((GameScene)Managers.Scene.CurrentScene).GamePlayer.transform.position - transform.position).magnitude < 2.0f)
        {
            ArrivedWork(Define.Worker.Player);
            StartCoroutine(Co_WorkRoutine(null));
            Managers.Resource.Instantiate("Test/WorkDoneParticleRed", transform);
        }
    }

    public override IEnumerator Co_WorkRoutine(IEnumerator employeeWorkDoneRoutine)
    {
        if (Worker == Define.Worker.Employee)
        {
            if(employeeWorkDoneRoutine != null)
                StartCoroutine(employeeWorkDoneRoutine);
            Managers.Resource.Instantiate("Test/WorkDoneParticle", transform);
        }

        yield return new WaitForSeconds(0.5f);

        if(Worker == Define.Worker.Employee)
            Managers.Work.AddWork(this);
        
        Worker = Define.Worker.None;
        IsWorking = false;

        yield break;
    }
}
