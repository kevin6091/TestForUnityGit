using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needs
{
    public Needs(Type type, int numNeeds)
    {
        NeedType = type;
        NumNeeds = numNeeds;
    }

    public Type NeedType { get; private set; }
    public int NumNeeds { get; private set; } = 0;
    public bool IsEnough { get { return NumNeeds == 0; } }

    public void AquireNeed()
    {
        NumNeeds = Mathf.Max(NumNeeds - 1, 0);
    }
}
