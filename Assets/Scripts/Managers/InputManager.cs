using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    public Action<bool, bool, bool, bool> MoveKeyAction = null;

    bool _isPressed = false;

    public void OnUpdate()
    {
        if(Input.anyKey == true &&
            null != MoveKeyAction && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            MoveKeyAction.Invoke(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.A), Input .GetKey(KeyCode.S), Input.GetKey(KeyCode.D));
        }

        if(Input.anyKey && KeyAction != null)
            KeyAction.Invoke();
        
        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _isPressed = true;
            }
            else
            {
                if (_isPressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _isPressed = false;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
