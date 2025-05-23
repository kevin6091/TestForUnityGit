using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Work
{
    void Start()
    {
        
    }
    protected override void OnTriggerEnter(Collider other)
    {
        CheckPlayer();
    }

    protected override void OnTriggerStay(Collider other)
    {

    }

    protected override void OnTriggerExit(Collider other)
    {

    }
}
