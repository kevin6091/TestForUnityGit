using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    public Action<bool, bool, bool, bool> MoveKeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;     

        if(null != MoveKeyAction && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            MoveKeyAction.Invoke(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.A), Input .GetKey(KeyCode.S), Input.GetKey(KeyCode.D));
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
