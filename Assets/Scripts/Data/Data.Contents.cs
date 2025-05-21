using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat

    [Serializable]
    public struct Stat
    {
        public int level;
        public int maxStackSize;
        public float moveSpeed;
        public float rotateSpeed;
    }

    [Serializable]
    public struct StatData : ILoader<int, Stat>
    {
        public List<Stat> stats;

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add((int)stat.level, stat);
            return dict;
        }
    }

    #endregion
}