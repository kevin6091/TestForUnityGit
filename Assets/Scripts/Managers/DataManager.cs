using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager : BaseManager
{
    public Dictionary<int, Data.Stat> StatDict {  get; private set; } = new Dictionary<int, Data.Stat>();

    public override void Init()
    {
        StatData statData = LoadJson<Data.StatData, int, Data.Stat>("StatData");
        StatDict = statData.MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
