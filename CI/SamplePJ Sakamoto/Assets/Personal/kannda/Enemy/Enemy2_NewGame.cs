using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class Enemy2_NewGame : Enemy2
{
    protected override void NextStateType()
    {
        nextTriggerType = TriggerType.EnterNext;
    }
}
