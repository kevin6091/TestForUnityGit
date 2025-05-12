using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ComponentsHelper
{
    public static bool LoadComponents<T>(this GameObject gameObject, ref Dictionary<string, Component> components) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if(component) 
        {
            components.Add(component.name, component);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static T Get<T>(this Dictionary<string, Component> components) where T : Component
    {
        string key = typeof(T).Name;
        if (components.TryGetValue(key, out Component comp))
            return comp as T;

        Debug.LogError($"Can't found {key}");
        return null;
    }
}
