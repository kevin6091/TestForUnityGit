using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    [SerializeField]
    public Define.ItemType itemType { get; private set; } = Define.ItemType.END;
}
