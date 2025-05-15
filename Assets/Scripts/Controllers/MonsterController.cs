using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterController : CreatureController
{
    Stat _stat;
    public override void Init()
    {
        _stat = gameObject.GetComponent<Stat>();

        //if (gameObject.GetComponentsInChildren<UI_HPBar>() == null)
        //    Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        //  Debug.Log("Monster UpdateIdle");
    }

    protected override void UpdateMoving()
    {
        //  Debug.Log("Monster UpdateMoving");
    }

    protected override void UpdateSkill()
    {
        //  Debug.Log("Monster UpdateSkill");
    }

    void OnHitEvent()
    {
        Debug.Log("Monster OnHitEvent");
    }
}
