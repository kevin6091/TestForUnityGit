using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyManagers : MonoBehaviour
{
    private static StudyManagers s_Instance;
    private static StudyManagers Instance {  get { Init();  return s_Instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate(); 
    }

    private static void Init()
    {
        if(null == s_Instance)
        {
            GameObject gameObject = GameObject.Find("@Managers");
            if(null == gameObject)
            {
                gameObject = new GameObject { name = "@GameObject" };
                gameObject.AddComponent<StudyManagers>();
            }            

            DontDestroyOnLoad(gameObject);
            s_Instance = gameObject.GetComponent<StudyManagers>();
        }
    }
}
