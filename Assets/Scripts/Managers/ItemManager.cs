using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemManager : BaseManager
{
    Dictionary<GameObject, Item> Items { get; set; } = new Dictionary<GameObject, Item>();

    public override void Init()
    {
    }
    public override void Clear()
    {
        Items.Clear();
    }

    public Define.ItemType GetType(GameObject itemObject)
    {
        Item item = GetItem(itemObject);
        if (item == null)
        {
            return Define.ItemType.END;
        }

        return item.ItemType;
    }

    public Item GetItem(GameObject itemObject)
    {
        if (itemObject == null ||
            Items.TryGetValue(itemObject, out Item item) == false)
        {
            return null;
        }

        return item;
    }

    public Item CreateItem(Define.ItemType type)
    {
        GameObject gameObject = Managers.Resource.Instantiate($"Items/{type.ToString()}");
        Item item = null;

        if (gameObject != null)
        {
            item = gameObject.GetComponent<Item>();
        }

        if (item != null)
        {
            Items.Add(gameObject, item);
            return item;
        }
        else
        {
            Debug.Log($"{type.ToString()} Item Prefab Unequipped Item Component");
        }

        Debug.Log($"Failed To Created Item {type.ToString()}");

        return null;
    }

    public bool DestoyItem(GameObject itemObject, float time = 0f)
    {
        if (Items.ContainsKey(itemObject) == false)
        {
            return false;
        }

        Items.Remove(itemObject);
        Managers.Resource.Destroy(itemObject, time);

        return true;
    }
}
