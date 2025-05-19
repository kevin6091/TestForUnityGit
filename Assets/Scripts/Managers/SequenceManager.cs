using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    List<Sequence> _sequences = new List<Sequence>();
    List<Coroutine> _cache = new List<Coroutine>();

    public void Init()
    {
        GameObject rootObj = new GameObject("@Sequences");
        DontDestroyOnLoad(rootObj);

        for (Define.Sequence i = 0; i < Define.Sequence.END; ++i)
        {
            GameObject sequenceObj = new GameObject($"Sequence{i.ToString()}");
            sequenceObj.transform.parent = rootObj.transform;
            sequenceObj.AddComponent<Sequence>();

            _sequences.Add(sequenceObj.GetComponent<Sequence>());
            _cache.Add(null);
        }
    }

    public void Add(Define.Sequence sequence, float delayTime, Func<IEnumerator> coFunc)
    {
        _sequences[(int)sequence].Add(delayTime, coFunc);
    }

    public void Initiate(Define.Sequence sequence)
    {
        if (_cache[(int)sequence] != null)
        {
            _sequences[(int)sequence].StopAllCoroutines();
            StopCoroutine(_cache[(int)sequence]);
        }

        _cache[(int)sequence] = StartCoroutine(_sequences[(int)sequence].Co_Initiate());
    }

    public void Clear()
    {
        for(int i = 0;i < _cache.Count; ++i)
        {
            StopCoroutine(_cache[i]);
            _cache[i] = null;
        }
        
        for(int i = 0; i < _sequences.Count; ++i)
            _sequences[i].Clear();
    }
}
