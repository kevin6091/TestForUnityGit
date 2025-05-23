using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needs<T>
{
    public Needs(T type, int numNeeds)
    {
        NumNeeds = numNeeds;
    }

    public T NeedType { get; private set; }
    public int NumNeeds { get; private set; } = 0;
    public bool IsEnough { get { return NumNeeds == 0; } }

    public void AquireNeed()
    {
        NumNeeds = Mathf.Max(NumNeeds - 1, 0);
    }
}
