using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    private List<KeyValuePair<float, Func<IEnumerator>>> _funcs = new List<KeyValuePair<float, Func<IEnumerator>>>();

    public void Add(float delayTime, Func<IEnumerator> coFunc)
    {
        KeyValuePair<float, Func<IEnumerator>> pair = new KeyValuePair<float, Func<IEnumerator>>(delayTime, coFunc);
        _funcs.Add(pair);
    }

    public void Clear()
    {
        StopAllCoroutines();

        _funcs.Clear();
    }

    public void StartCoroutine()
    {
        StartCoroutine(Co_Initiate()); 
    }

    public IEnumerator Co_Initiate()
    {
        float accTime = 0f;

        Func<IEnumerator> func = this.Co_Initiate;

        _funcs.Sort((src, dst) => src.Key.CompareTo(dst.Key));

        int idx = 0;
        while (idx < _funcs.Count)
        {
            accTime += Time.deltaTime;

            while (idx < _funcs.Count &&
                _funcs[idx].Key <= accTime)
            {
                StartCoroutine(_funcs[idx].Value());
                ++idx;
            }

            yield return null;
        }
    }
}
