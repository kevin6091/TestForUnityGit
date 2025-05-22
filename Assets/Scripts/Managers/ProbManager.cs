using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ProbManager : BaseManager
{
    Dictionary<Define.ProbType, HashSet<ProbController>> Probs { get; set; } = new Dictionary<Define.ProbType, HashSet<ProbController>>();
    private Dictionary<Define.ProbType, GameObject> ProbRoots { get; set; } = new Dictionary<Define.ProbType, GameObject>();

    public override void Init()
    {
        GameObject root = new GameObject() { name = "@Probs" };
        
        for(int i = 0; i < (int)Define.ProbType.END; ++i)
        {
            string typeName = ((Define.ProbType)i).ToString();
            GameObject child = new GameObject() { name = $"Prob_{typeName}" };
            child.transform.SetParent(root.transform, false);

            ProbRoots.Add((Define.ProbType)i, child);

            Probs.Add((Define.ProbType)i, new HashSet<ProbController>());
        }
    }

    public override void Clear()
    {
        foreach(HashSet<ProbController> controllers in Probs.Values)
        {
            controllers.Clear(); 
        }

        Probs.Clear();
    }

    public HashSet<ProbController> GetProbs(Define.ProbType type)
    {
        return Probs[type];
    }

    public GameObject CreateProb(Define.ProbType type)
    {
        GameObject gameObject = Managers.Resource.Instantiate($"Probs/{type.ToString()}");
        ProbController controller = null;

        if (gameObject != null)
        {
            controller = gameObject.GetComponent<ProbController>();
        }

        if (controller != null)
        {
            Probs[type].Add(controller);
            controller.transform.SetParent(ProbRoots[type].transform);

            return gameObject;
        }

        Debug.Log($"Failed To CreateProb {type.ToString()}");

        return null;
    }

    public bool DestoryProb(ProbController controller, float time = 0f)
    {
        if(controller == null)
        {
            return false;
        }    

        GameObject gameObject = controller.gameObject;
        Define.ProbType type = controller.ProbType;

        if (Probs.ContainsKey(type) == false ||
            Probs[type].Contains(controller) == false)
        {
            return false;
        }

        Probs[type].Remove(controller);
        Managers.Resource.Destroy(gameObject, time);

        return true;
    }

    //  외부에서 selectorFunc 를 추가로 던지면 제약조건이된다. 
    //  ex) Table의 빈좌석이있는지 확인하는 함수를 던진다.
    public ProbController GetNearestProb(Define.ProbType type, Vector3 pos, Func<bool, ProbController> selectFunc = null)
    {
        ProbController controller = null;

        if (Probs.TryGetValue(type, out HashSet<ProbController> controllers) == true)
        {
            float minDist = Mathf.Infinity;
            foreach(ProbController item in controllers)
            {
                if (selectFunc != null &&
                    selectFunc(item) == false)
                {
                    continue;
                }

                float dist = (item.transform.position - pos).magnitude;
                if(dist < minDist)
                {
                    controller = item;
                    minDist = dist;
                }
            }
        }

        return controller;
    }
}
