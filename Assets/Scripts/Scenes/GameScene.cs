using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{
    public PlayerController GamePlayer { get; private set; } = null;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        //  Todo : Refactoring
        //  TestZone
        TestInit();
        StartCoroutine(Co_Test());

        GamePlayer = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public override void Clear()
    {

    }

    //  Todo : Refactoring
    //  TEST Zone
    List<GameObject> customerObjects = new List<GameObject>();
    public void TestInit()
    {
        GameObject table1 = Managers.Prob.CreateProb(Define.ProbType.Table);
        //  GameObject table2 = Managers.Prob.CreateProb(Define.ProbType.Table);
        //  GameObject table3 = Managers.Prob.CreateProb(Define.ProbType.Table);

        table1.transform.position = new Vector3(9f, 0f, 0f); 
        //  table2.transform.position = new Vector3(3f, 0f, 0f); 
        //  table3.transform.position = new Vector3(6f, 0f, 0f); 

        GameObject standGameObject = Managers.Prob.CreateProb(Define.ProbType.Stand);
        standGameObject.transform.position = new Vector3(6f, 0f, -6f);

        for(int i = 0; i < 10; ++i)
        {
            GameObject go = Managers.Resource.Instantiate("Customer");
            Vector3 pos = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
            go.transform.position = pos;
            customerObjects.Add(go);
        }
    }

    public IEnumerator Co_Test()
    {
        yield return null;

        for (int i = 0; i < customerObjects.Count; ++i)
        {
            GameObject go = customerObjects[i]; 
            CustomerController customer = go.GetComponent<CustomerController>();
            StandController nearestStand = Managers.Prob.GetNearestProb(Define.ProbType.Stand, go.transform.position) as StandController;

            if (customer != null &&
                nearestStand != null)
            {
                if (nearestStand.WaitingLine == null)
                {
                    Debug.Log("WaitingLine is null");
                }
                else
                {
                    nearestStand.WaitingLine.Enqueue(customer);
                }
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
