using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;

public class CoroutineHelper : MonoBehaviour
{
    public static Dictionary<object, Dictionary<string, Coroutine>> CoroutineDict { get; } = new Dictionary<object, Dictionary<string, Coroutine>>();

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

    public static CoOutInfo MyStartCoroutine(object obj, IEnumerator routine)
    {
        string routineName = routine.GetType().Name;

        if (CoroutineDict.ContainsKey(obj) == false)
        {
            CoroutineDict.Add(obj, new Dictionary<string, Coroutine>());
        }

        // 이미 있던 루틴이었다면 어떻게 할지.. -> 중복 실행을 허용한다. 키를 새로 만들어준다.
        if (CoroutineDict[obj].ContainsKey(routineName) == true)
        {
            _duplicatedKeys.TryAdd(routineName, 0);
            _duplicatedKeys[routineName]++;
            routineName += _duplicatedKeys[routineName];
        }

        Coroutine coroutine = Instance.StartCoroutine(routine);
        
        if(coroutine != null)
        {
            CoroutineDict[obj].Add(routineName, coroutine);
            return new CoOutInfo(routineName, routine, coroutine);
        }

        Debug.LogError("CoroutineHelper : Failed StartCoroutine");
        return new CoOutInfo();
    }

    public static bool MyStopCoroutine(object obj, IEnumerator routine)
    {
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

        CoroutineDict[obj].Remove(routineName);
        Instance.StopCoroutine(routine);
        return true;
    }

    public static bool MyStopCoroutine(object obj, string routineName)
    {
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

        CoroutineDict[obj].Remove(routineName);
        Instance.StopCoroutine(routineName);
        return true;
    }

    public static bool MyStopAllCoroutines(object obj)
    {
        if (CoroutineDict.ContainsKey(obj) == false)
        {
            Debug.LogError("CoroutineHelper : Not contains key obj");
            return false;
        }

        foreach(var pair in CoroutineDict[obj])
        {
            Instance.StopCoroutine(pair.Value);
        }
        CoroutineDict[obj].Clear();
        
        return true;
    }

    public static bool RemoveCoroutineDict(object obj, IEnumerator routine) 
    {
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

    public static bool RemoveCoroutineDict(object obj, string routineName)
    {
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

        CoroutineDict[obj].Remove(routineName);
        return true;
    }
}
