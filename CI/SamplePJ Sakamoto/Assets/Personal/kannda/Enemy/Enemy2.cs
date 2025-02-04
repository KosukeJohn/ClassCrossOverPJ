using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyCoppy
{
    protected override void EnterBack()
    {
        anim.Play("Run", 0, 0);
        Vector3 NoeAngle = transform.eulerAngles;
        transform.eulerAngles = -1 * NoeAngle;
    }

    protected override void UpdateBack()
    {
        if (!playerFindFlag) { ChangeStateMachine(TriggerType.EnterIdle); return; }
        if (!checkHideFlag) { ChangeStateMachine(TriggerType.EnterChase); return; }

        transform.position += transform.forward * Time.deltaTime;
    }
}
