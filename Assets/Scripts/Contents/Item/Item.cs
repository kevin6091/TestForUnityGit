using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public Define.ItemType ItemType { get; private set; } = Define.ItemType.END;

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        Free();
    }

    private void Init()
    {
        
    }

    private void Free()
    {

    }
}
