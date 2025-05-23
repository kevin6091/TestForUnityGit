using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public GameObject HoldObject { get; set; } = null;

    public bool IsEmpty { get { return HoldObject == null; } }
}
