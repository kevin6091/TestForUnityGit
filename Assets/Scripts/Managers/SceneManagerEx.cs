using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : BaseManager
{
    BaseScene _currentScene = null;
    public BaseScene CurrentScene
    {
        get 
        { 
            if(null == _currentScene)
                _currentScene = GameObject.FindObjectOfType<BaseScene>();

            return _currentScene;
        }
        private set { _currentScene = value; }
    }

    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
        BaseScene temp = CurrentScene;
        //  SceneManager.LoadSceneAsync(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public override void Clear()
    {
        CurrentScene.Clear();
        CurrentScene = null;
    }
}
