using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class Enemy2_Continue : Enemy2
{
    //private float timeCnt;
    //private bool animPlay;
    //protected override void UpdateAttack()
    //{
    //    timeCnt += Time.deltaTime;
    //    ChangeLightColor(true);
    //    if (timeCnt > 1.0f)
    //    {
    //        if (!animPlay) 
    //        { 
    //            anim.Play("Find+", 0, 0);
    //            animPlay = true;
    //        }
    //    }

    //    if (timeCnt > 1.5f)
    //    {
    //        attackFlag = true;
    //    }

    //    if (timeCnt > 2.5f)
    //    {
    //        attackFlag = false;
    //        ChangeLightColor(false);
    //    }

    //    if (timeCnt >= 3.0f)
    //    {
    //        timeCnt = 0;
    //        animPlay = false;
    //        stateMachine.ExecuteTrigger(TriggerType.EnterIdle);
    //    }

    //}

    //protected override void UpdateIdle()
    //{
    //    SpeedY -= 9.8f * Time.deltaTime;
    //    if (SpeedY < 0) { SpeedY = 0; }

    //    if (this.transform.position.x >= 226.68f)
    //    {
    //        stateMachine.ExecuteTrigger(TriggerType.EnterDeath);
    //    }

    //    if (this.transform.position.x <= player.transform.position.x + 0.5f)
    //    {
    //        this.transform.Translate(SpeedX, SpeedY, 0);

    //        if (this.transform.position.x >= 207f)
    //        {
    //            SpeedY = 12f * Time.deltaTime;
    //        }
    //    }
    //    else
    //    {
    //        if (this.transform.position.x >= 193f)
    //            if (this.transform.position.x <= 198f)
    //            { return; }

    //        if (this.transform.position.x >= 220.5f)
    //            if (this.transform.position.x <= 225f)
    //            { return; }

    //        // ŽžŠÔ‚ð’²®‚µ‚ÄSEÄ¶
    //        Invoke("AtkSoundPlay", 1.0f);
    //        stateMachine.ExecuteTrigger(TriggerType.EnterAttack);
    //    }
    //}

    protected override void NextStateType()
    {
        nextTriggerType = TriggerType.EnterIdle;
    }
}
