using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyMonoBehaviour : MonoBehaviour
{
    private class CoroutineHelper : MonoBehaviour
    {
        private Dictionary<GameObject, Dictionary<string, Coroutine>> _coroutines = new Dictionary<GameObject, Dictionary<string, Coroutine>>();
        public Dictionary<GameObject, Dictionary<string, Coroutine>> Coroutines { get; set; }
    }

    private CoroutineHelper coroutineHelper;

    [System.Obsolete("직접 StartCoroutine을 호출하지 마세요. MyMonoBehaviour의 RunCoroutine을 사용하세요.", true)]
    public new Coroutine StartCoroutine(IEnumerator routine)
    {
        throw new System.NotSupportedException("직접 StartCoroutine을 사용할 수 없습니다.");
    }

    [System.Obsolete("직접 StopCoroutine을 호출하지 마세요. MyMonoBehaviour의 CancelCoroutine을 사용하세요.", true)]
    public new void StopCoroutine(Coroutine routine)
    {
        throw new System.NotSupportedException("직접 StopCoroutine을 사용할 수 없습니다.");
    }

    [System.Obsolete("직접 StopAllCoroutines을 호출하지 마세요. MyMonoBehaviour의 CancelAllCoroutines을 사용하세요.", true)]
    public new void StopAllCoroutines()
    {
        throw new System.NotSupportedException("직접 StopCoroutine을 사용할 수 없습니다.");
    }

    Coroutine RunCoroutine(GameObject gameObject, IEnumerator routine)
    {
        //이미 실행중이면 로그 남기고 코루틴 덮어쓰기
        if (coroutineHelper.Coroutines[gameObject].ContainsKey(routine.ToString()))
        {
            Debug.Log("Allready Started Coroutine!! : CoroutineHelper");
            base.StopCoroutine(coroutineHelper.Coroutines[gameObject][routine.ToString()]);
            coroutineHelper.Coroutines[gameObject].Remove(routine.ToString());
        }

        Coroutine coroutine = base.StartCoroutine(routine);
        if (coroutine != null)
        {
            coroutineHelper.Coroutines[gameObject].Add(routine.ToString(), coroutine);
            return coroutine;
        }

        Debug.LogError("Failed to StartCoroutine!! : CoroutineHelper");
        return null;
    }

    public void CancelCoroutine(GameObject gameObject, string key)
    {
        if (coroutineHelper.Coroutines[gameObject].TryGetValue(key, out Coroutine coroutine))
        {
            base.StopCoroutine(coroutine);
            coroutineHelper.Coroutines[gameObject].Remove(key);
        }
        else
        {
            Debug.LogError("Not found key!! : CoroutineHelper");
        }
    }

    public void CancelMyAllCoroutines(GameObject gameObject)
    {
        if (coroutineHelper.Coroutines.TryGetValue(gameObject, out Dictionary<string, Coroutine> coroutineDiction))
        {
            foreach (var coroutine in coroutineDiction.Values)
            {
                if (coroutine != null)
                    base.StopCoroutine(coroutine);
            }
            coroutineHelper.Coroutines[gameObject].Clear();
        }
        else
        {
            Debug.LogError("Not found key!! : CoroutineHelper");
        }
    }

    public void CancelAllCoroutines()
    {
        foreach (var corutineDiction in coroutineHelper.Coroutines.Values)
        {
            foreach (var coroutine in corutineDiction.Values)
            {
                if (coroutine != null)
                    base.StopCoroutine(coroutine);
            }
        }

        coroutineHelper.Coroutines.Clear();
    }
}
