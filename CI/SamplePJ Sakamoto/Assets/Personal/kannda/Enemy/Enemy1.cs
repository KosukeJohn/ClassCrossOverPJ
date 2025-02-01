using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyCoppy
{
    protected override void EnterBack()
    {
        { anim.Play("Idle", 0, 0); }
    }
    protected override void UpdateBack() 
    {
        if (!playerFindFlag) { ChangeStateMachine(TriggerType.EnterIdle); return; }
        if (!checkHideFlag) { ChangeStateMachine(TriggerType.EnterChase);return; }
    }
}
