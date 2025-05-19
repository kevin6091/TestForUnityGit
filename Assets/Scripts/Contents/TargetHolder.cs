using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetHolder : MonoBehaviour
{
    public GameObject TargetObj { get; set; } = null;
    public Vector3 Offset { get; set; } = Vector3.zero;

    public bool Position(out Vector3 position)
    {
        position = Vector3.zero;
        if (TargetObj == null)
            return false;

        position = TargetObj.transform.position + Offset;
        return true;
    }

    public bool Direction(out Vector3 direction)
    {
        direction = Vector3.zero;
        if (TargetObj == null)
            return false;

        direction = (TargetObj.transform.position + Offset) - transform.position;
        return true;
    }
}
