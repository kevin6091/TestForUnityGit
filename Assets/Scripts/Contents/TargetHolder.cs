using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetobj = null;
    public GameObject TargetObj { get { return _targetobj; } set { _targetobj = value; } }
    public Vector3 Offset { get; set; } = Vector3.zero;
    public float Range { get; set; } = 0.0f;

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

    public bool DirectionXZ(out Vector3 direction)
    {
        if (Direction(out direction) == false)
            return false;

        direction.y = 0f;
        return true;
    }
}
