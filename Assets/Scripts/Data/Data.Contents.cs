using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Stat

[Serializable]
public struct Stat
{
    public int level;
    public int hp;
    public int attack;
}

[Serializable]
public struct StatData : ILoader<int, Stat>
{
    public List<Stat> stats;

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in stats)
            dict.Add(stat.level, stat);
        return dict;
    }
}

#endregion