using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ResourceManager : BaseManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject gameObject = Managers.Pool.GetOriginal(name);
            if (gameObject != null)
                return gameObject as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (null == original)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        Poolable poolable = Managers.Pool.TryGetOrCachePoolable(original);
        if (null != poolable)
        //  if(original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject gameObject = Object.Instantiate(original, parent);
        gameObject.name = original.name;

        return gameObject;
    }

    public void Destroy(GameObject gameObject, float time)
    {
        if (null == gameObject)
        {
            Debug.Log($"Failed to destroy gameobject : {gameObject.name}");
            return;
        }

        Poolable poolable = Managers.Pool.TryGetOrCachePoolable(gameObject);
        if (null != poolable)
        {
            Managers.Pool.Push(poolable);
        }

        //Poolable poolable = gameObject.GetComponent<Poolable>();
        //if (poolable != null)
        //{
        //    Managers.Pool.Push(poolable);
        //    return;
        //}

        Object.Destroy(gameObject);
    }
}
