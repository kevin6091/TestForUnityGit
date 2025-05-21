using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        //  Managers.UI.ShowSceneUI<UI_Inven>();
        //  Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        //  Todo : Refactoring
        //  TEST Zone
        Managers.Input.Actions[(Define.InputEvent.KeyEvent, Define.InputType.Down)] -= OnKeyboard;
        Managers.Input.Actions[(Define.InputEvent.KeyEvent, Define.InputType.Down)] += OnKeyboard;

        List<GameObject> matchingObjects = new List<GameObject>();
        GameObject[] allObjects = FindObjectsOfType<GameObject>(); // 씬 내 모든 활성 오브젝트 검색

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Customer") // 이름 비교
            {
                customers.Add(obj.GetComponent<CustomerController>());
            }
        }
        
        _stand = Component.FindAnyObjectByType<StandController>();

        GameObject go = Managers.Prob.CreateProb(Define.ProbType.Table);
        go.transform.position = Vector3.zero;
    }

    public override void Clear()
    {

    }

    StandController _stand = null;
    List<CustomerController> customers = new List<CustomerController>();
    int index = 0;
    private void OnKeyboard(object[] objects)
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _stand.WaitingLine.Enqueue(customers[index++]);
            while (index >= customers.Count)
                --index;
        }
    }
}
