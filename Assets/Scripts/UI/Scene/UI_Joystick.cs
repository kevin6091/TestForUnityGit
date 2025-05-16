using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : UI_Scene
{
    [SerializeField] float _joystickRadius = 5.0f;

    enum Images
    {
        JoystickOutline,
        JoystickHandle,
        Panel,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));

        GameObject outline = GetImage((int)Images.JoystickOutline).gameObject;
        GameObject handle = GetImage((int)Images.JoystickHandle).gameObject;
        GameObject panel = GetImage((int)Images.Panel).gameObject;

        outline.SetActive(false);
        handle.SetActive(false);

        panel.BindEvent(ShowJoystick, Define.UIEvent.Down);
        panel.BindEvent(HideJoystick, Define.UIEvent.Up);
        panel.BindEvent(DragJoystickHandle, Define.UIEvent.Drag);
    }

    private void ShowJoystick(PointerEventData data)
    {
        GameObject outline = GetImage((int)Images.JoystickOutline).gameObject;
        GameObject handle = GetImage((int)Images.JoystickHandle).gameObject;

        outline.SetActive(true);
        handle.SetActive(true);

        outline.transform.position = data.position;
        handle.transform.position = data.position;
        handle.BindEvent(DragJoystickHandle, Define.UIEvent.Drag);
    }

    private void HideJoystick(PointerEventData data)
    {
        GameObject outline = GetImage((int)Images.JoystickOutline).gameObject;
        GameObject handle = GetImage((int)Images.JoystickHandle).gameObject;

        outline.SetActive(false);
        handle.SetActive(false);

        handle.BindEvent(DragJoystickHandle, Define.UIEvent.Drag);
    }

    private void DragJoystickHandle(PointerEventData data)
    {
        GameObject handle = GetImage((int)Images.JoystickHandle).gameObject;
        GameObject outline = GetImage((int)Images.JoystickOutline).gameObject;

        Vector2 dir = data.position - (Vector2)outline.transform.position;

        dir = dir.normalized * MathF.Min(dir.magnitude, _joystickRadius);

        handle.transform.position = outline.transform.position + (Vector3)dir;
    }
}
