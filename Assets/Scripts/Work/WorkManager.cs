using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorkManager : MonoBehaviour
{
    private static WorkManager _instance = null;

    //public static WorkManager Instance 
    //{
    //    //get
    //    //{
    //    //    if(_instance == null)
    //    //    {
    //    //        _instance = ;
    //    //    }
    //    //}
    //}

    Queue<Work> _toWorks = new Queue<Work>();

    private Queue<Work> ToWorks { get; }
    
    public Work PeekToWorks { get { return ToWorks.Peek(); } }

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

    void Update()
    {
    }

    public void AddWork(Define.WorkType workType)
    {
        switch(workType)
        {
            case Define.WorkType.Buger:
                _toWorks.Enqueue(CreateObject<Buger>("Buger"));
                break;
            case Define.WorkType.BugerServing:
                _toWorks.Enqueue(CreateObject<BugerServing>("BugerServing"));
                break;
            case Define.WorkType.Counter:
                _toWorks.Enqueue(CreateObject<Counter>("Counter"));
                break;
            case Define.WorkType.Trash:
                _toWorks.Enqueue(CreateObject<Trash>("Trash"));
                break;
        }
    }

    private Work CreateObject<T>(string name) where T : Work
    {
        GameObject gameObject = Managers.Resource.Instantiate($"Works/{name}");
        gameObject.transform.SetParent(Root.transform);

        return gameObject.GetOrAddComponent<T>();
    }
}
