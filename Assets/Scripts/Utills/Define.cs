using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public enum Layer
    {
        Wall = 8,
        Floor,
        Block,
        Monster,
    }

    public enum Scene
    {
        Unknown,    //  Default
        Login,      //  Main
        Lobby,      
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum TouchEvent
    {
        Begin,
        Pressing,
        Drag,
        End,
    }


    public enum CameraMode
    {
        QuarterView,
        TopView,
        Free,
    }
}
