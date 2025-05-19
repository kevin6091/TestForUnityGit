using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : BaseManager
{
    List<Sequence> _sequences = new List<Sequence>();

    public override void Init()
    {
        GameObject rootObj = new GameObject("@Sequences");
        GameObject.DontDestroyOnLoad(rootObj);        

        for (Define.Sequence i = 0; i < Define.Sequence.END; ++i)
        {
            GameObject sequenceObj = new GameObject($"Sequence{i.ToString()}");
            sequenceObj.transform.parent = rootObj.transform;
            sequenceObj.AddComponent<Sequence>();

            _sequences.Add(sequenceObj.GetComponent<Sequence>());
        }
    }

    public void Add(Define.Sequence sequence, float delayTime, Func<IEnumerator> coFunc)
    {
        _sequences[(int)sequence].Add(delayTime, coFunc);
    }

    public void Initiate(Define.Sequence sequence)
    {
        _sequences[(int)sequence].StopAllCoroutines();
        _sequences[(int)sequence].StartCoroutine();
    }

    public override void Clear()
    {        
        for(int i = 0; i < _sequences.Count; ++i)
            _sequences[i].Clear();
    }
}
