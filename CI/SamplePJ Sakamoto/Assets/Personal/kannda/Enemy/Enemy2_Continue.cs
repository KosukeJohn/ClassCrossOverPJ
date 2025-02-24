using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_Continue : Enemy2
{
    protected override void NextStateType()
    {
        nextTriggerType = TriggerType.EnterIdle;
    }
}
