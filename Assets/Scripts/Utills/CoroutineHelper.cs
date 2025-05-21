using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.UI;

public class CoroutineHelper : MonoBehaviour
{
    public static Dictionary<object, Dictionary<string, CoOutInfo>> CoroutineDict { get; } = new Dictionary<object, Dictionary<string, CoOutInfo>>();

    private static CoroutineHelper _instance;

    private static Dictionary<string, int> _duplicatedKeys = new Dictionary<string, int>();

    public struct CoOutInfo
    {
        public string OutRoutineName;
        public IEnumerator OutRoutine;
        public Coroutine OutCoroutine;

        public CoOutInfo(string name, IEnumerator routine, Coroutine coroutine)
        {
            OutRoutineName = name;
            OutRoutine = routine;
            OutCoroutine = coroutine;
        }
    }
    
    public static CoroutineHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("CoroutineHelper");
                _instance = go.AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public static CoOutInfo GetCoInfo(object obj, IEnumerator routine)
    {
        if (obj == null || routine == null)
        {
            Debug.LogError("CoroutineHelper : null obj, routine");
            return new CoOutInfo();
        }

        string routineName = routine.GetType().Name;

        if (CoroutineDict.ContainsKey(obj) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key obj");
            return new CoOutInfo();
        }

        if (CoroutineDict[obj].ContainsKey(routineName) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key routine");
            return new CoOutInfo();
        }

        return CoroutineDict[obj][routineName];
    }

    public static CoOutInfo MyStartCoroutine(object obj, IEnumerator routine)
    {
        if (obj == null || routine == null)
        {
            Debug.LogError("CoroutineHelper : null obj, routine");
            return new CoOutInfo();
        }

        string routineName = routine.GetType().Name;

        if (CoroutineDict.ContainsKey(obj) == false)
        {
            CoroutineDict.Add(obj, new Dictionary<string, CoOutInfo>());
        }

        // 이미 있던 루틴이었다면 어떻게 할지.. -> 중복 실행을 허용한다. 키를 새로 만들어준다.
        if (CoroutineDict[obj].ContainsKey(routineName) == true)
        {
            _duplicatedKeys.TryAdd(routineName, 0);
            _duplicatedKeys[routineName]++;
            routineName += _duplicatedKeys[routineName];
        }

        Coroutine coroutine = Instance.StartCoroutine(routine);

        //Debug.Log(coroutine + $"{Time.time}");

        if(coroutine != null)
        {
            CoOutInfo info = new CoOutInfo(routineName, routine, coroutine);
            CoroutineDict[obj].Add(routineName, info);
            return info;
        }

        Debug.LogError("CoroutineHelper : Failed StartCoroutine");
        return new CoOutInfo();
    }

    public static bool MyStopCoroutine(object obj, IEnumerator routine)
    {
        if (obj == null || routine == null)
        {
            Debug.LogError("CoroutineHelper : null obj, routine");
            return false;
        }

        string routineName = routine.GetType().Name;

        if (CoroutineDict.ContainsKey(obj) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key obj");
            return false;
        }

        if (CoroutineDict[obj].ContainsKey(routineName) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key routine");
            return false;
        }

        Instance.StopCoroutine(CoroutineDict[obj][routineName].OutCoroutine);
        CoroutineDict[obj].Remove(routineName);
        return true;
    }

    public static bool MyStopAllCoroutines(object obj)
    {
        if (obj == null)
        {
            Debug.LogError("CoroutineHelper : null obj, routine");
            return false;
        }

        if (CoroutineDict.ContainsKey(obj) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key obj");
            return false;
        }

        foreach(var pair in CoroutineDict[obj])
        {
            Instance.StopCoroutine(pair.Value.OutCoroutine);
        }
        CoroutineDict[obj].Clear();
        
        return true;
    }

    public static bool RemoveCoroutineDict(object obj, IEnumerator routine) 
    {
        if (obj == null || routine == null)
        {
            Debug.LogError("CoroutineHelper : null obj, routine");
            return false;
        }

        string routineName = routine.GetType().Name;

        if(CoroutineDict.ContainsKey(obj) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key obj");
            return false;
        }

        if (CoroutineDict[obj].ContainsKey(routineName) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key routine");
            return false;
        }

        CoroutineDict[obj].Remove(routineName);
        return true;
    }
}
