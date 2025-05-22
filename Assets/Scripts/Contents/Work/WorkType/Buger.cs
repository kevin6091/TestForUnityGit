using System.Collections;
using System.Collections.Generic;
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
            Co_ArrivedWork(Define.Worker.Player);
        }
    }

    public override IEnumerator Co_WorkRoutine(IEnumerator employeeEscapeRoutine)
    {
        yield return null;

        StartCoroutine(employeeEscapeRoutine);

        yield return new WaitForSeconds(1.5f);
        Worker = Define.Worker.None;
        IsWorking = false;
        Managers.Work.AddWork(this);

        yield break;
    }
}
