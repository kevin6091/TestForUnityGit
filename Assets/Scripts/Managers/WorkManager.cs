using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorkManager : BaseManager
{
    private Queue<Work> ToWorks { get; } = new Queue<Work>();

    public Work TryPeekToWorks { get { return ToWorks.Count() > 0 ? ToWorks.Peek() : null; } }
    public Work DeQueueToWorks { get { Work ele = ToWorks.Dequeue(); OuterWork.Add(ele); return ele; } }

    private List<Work> OuterWork = new List<Work>();

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@Work_Root");
            if (root == null)
                root = new GameObject { name = "@Work_Root" };
            return root;
        }
    }

    public void GenerateWork(Define.WorkType workType)
    {
        switch(workType)
        {
            case Define.WorkType.Buger:
                AddWork(InstantiateWork<Buger>("Buger"));
                break;
            case Define.WorkType.BugerServing:
                AddWork(InstantiateWork<BugerServing>("BugerServing"));
                break;
            case Define.WorkType.Counter:
                AddWork(InstantiateWork<Counter>("Counter"));
                break;
            case Define.WorkType.Trash:
                AddWork(InstantiateWork<Trash>("Trash"));
                break;
        }
    }

    public void AddWork(Work work)
    {
        OuterWork.Remove(work);
        if (OuterWork.Contains(work))
        {
            int a = 0;
            a += 1;
        }
        ToWorks.Enqueue(work);
    }

    private Work InstantiateWork<T>(string name) where T : Work
    {
        GameObject gameObject = Managers.Resource.Instantiate($"Works/{name}");
        gameObject.transform.SetParent(Root.transform);

        return gameObject.GetOrAddComponent<T>();
    }
}
