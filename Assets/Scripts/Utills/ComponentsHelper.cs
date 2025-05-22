using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ComponentsHelper
{
    private static Dictionary<GameObject, Dictionary<string, Component>> ComponentDict { get; set; } = new Dictionary<GameObject, Dictionary<string, Component>>();

    public static bool LoadComponents<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component)
        {
            ComponentDict.TryAdd(gameObject, new Dictionary<string, Component>());

            if (ComponentDict[gameObject].ContainsKey(component.name) == true)
            {
                Debug.LogError("ComponentsHelper : Allreay key");
                return false;
            }

            ComponentDict[gameObject].Add(component.name, component);

            return true;
        }
        else
        {
            return false;
        }
    }

    public static T Get<T>(this GameObject gameObject) where T : Component
    {
        string key = typeof(T).Name;
        if (ComponentDict.ContainsKey(gameObject) == false)
        {
            Debug.LogError("ComponentHelper : Not contains key gameobject");
            return null;
        }

        if (ComponentDict[gameObject].ContainsKey(key) == false)
        {
            Debug.LogError("ComponentHelper : Not contains key component");
            return null;
        }

        return ComponentDict[gameObject][key] as T;
    }

    public static void RemoveComponent<T>(this GameObject gameObject) where T : Component
    {
        string key = typeof(T).Name;
        if (ComponentDict.ContainsKey(gameObject) == false)
        {
            Debug.LogError("ComponentHelper : Not contains key gameobject");
            return;
        }

        if (ComponentDict[gameObject].ContainsKey(key) == false)
        {
            Debug.LogError("ComponentHelper : Not contains key component");
            return;
        }

        ComponentDict[gameObject].Remove(key);
    }

    public static void RemoveAllComponents(this GameObject gameObject)
    {
        if (ComponentDict.ContainsKey(gameObject) == false)
        {
            Debug.LogError("ComponentHelper : Not contains key gameobject");
            return;
        }

        ComponentDict.Remove(gameObject);
    }
}
