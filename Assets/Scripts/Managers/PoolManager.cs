using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; ++i)
                Push(Create());
        }

        Poolable Create()
        {
            GameObject gameObject = Object.Instantiate<GameObject>(Original);
            gameObject.name = Original.name;
            return gameObject.GetOrAddComponent<Poolable>(); 
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();

            else
                poolable = Create();

            if (parent == null)     //  DontDestroyOnLoad ������ (Ǯ�� �� Root��ü�� �ڽ����� �־ DontDestroyOnLoad ������ ���Ե�)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.gameObject.SetActive(true);
            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Dictionary<string, Poolable> _poolableCache = new Dictionary<string, Poolable>();
    HashSet<string> _nonPoolableCache = new HashSet<string>();
    Transform _root;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public Poolable TryGetOrCachePoolable(GameObject gameObject)
    {
        if (_nonPoolableCache.Contains(gameObject.name))
            return null;

        Poolable poolable;
        _poolableCache.TryGetValue(gameObject.name, out poolable);
        if (null != poolable)
            return poolable;

        poolable = gameObject.GetComponent<Poolable>();
        if (null != poolable)
            _poolableCache.Add(gameObject.name, poolable);
        else
            _nonPoolableCache.Add(gameObject.name);

        return poolable;
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if(_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach(Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
