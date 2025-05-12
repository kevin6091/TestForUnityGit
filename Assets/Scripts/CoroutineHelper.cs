using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CoroutineHelper : MonoBehaviour
{
    private Dictionary<GameObject, Dictionary<string,Coroutine>> _coroutines = new Dictionary<GameObject, Dictionary<string, Coroutine>>();
    private Dictionary<GameObject, Dictionary<string, Coroutine>> Coroutines { get; set; }

}
