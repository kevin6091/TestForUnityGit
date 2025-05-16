using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{

    private void Start()
    {
        //  Todo : Need Data Parse
        MoveSpeed = 5.0f;
        RotateSpeed = 500.0f;
    }
}
