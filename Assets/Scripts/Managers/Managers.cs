using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance
    {
        get { Init(); return s_instance; }
    }

    InputManager _input = new InputManager();
    public static InputManager Input
    {
        get { return Instance._input; }
    }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource
    {
        get { return Instance._resource; }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(null == s_instance)
        {
            GameObject gameobject = GameObject.Find("@Managers");
            if(null == gameobject)
            {
                gameobject = new GameObject { name = "@Managers" };
                gameobject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(gameobject);
            s_instance = gameobject.GetComponent<Managers>();
        }
    }
}
