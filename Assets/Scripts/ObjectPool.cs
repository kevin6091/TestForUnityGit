using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    private readonly HashSet<T> _activeObjects = new HashSet<T>();
    private readonly Queue<T> _pool = new Queue<T>();
    private readonly Func<T> _objectFactory;

    public ObjectPool(int size, Func<T> factory)
    {
        if (null == factory)
        {
            Debug.Log("Failed Init Pool " + nameof(factory));
            return;
        }

        _objectFactory = factory;

        for (int i = 0; i < size; ++i)
            _pool.Enqueue(factory());
    }

    public T Get()
    {
        T item = _pool.Dequeue();
        _activeObjects.Add(item);

        return item;
    }

    public void Return(T item)
    {
        if(_activeObjects.Remove(item))
            _pool.Enqueue(item);

        else
            Debug.Log("Failed Return Pool " + typeof(T).Name);
    }
}