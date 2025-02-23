using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class Enemy2_NewGame : Enemy2
{
    private float timeCnt;
    private bool animPlay;
    private int attackCnt = 0;
    private int LightCnt = 0;
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

    //    if (this.transform.position.x >= GetAttackPos(attackCnt) - 5f)
    //    {
    //        LightContllore(8);
    //    }

    //    if (this.transform.position.x >= GetAttackPos(attackCnt))
    //    {
    //        attackCnt++;
    //        Invoke("AtkSoundPlay", 1.0f);
    //        stateMachine.ExecuteTrigger(TriggerType.EnterAttack);
    //    }
    //}

    protected override void NextStateType()
    {
        nextTriggerType = TriggerType.EnterNext;
    }
    private float GetAttackPos(int cnt)
    {
        float attackPos = 10000;

        switch (cnt)
        {
            case 0:
                attackPos = 184.31f;
                break;
            case 1:
                attackPos = 197.513f;
                break;
            case 2:
                attackPos = 212.1f;
                break;
            case 3:
                attackPos = 219.85f;
                break;
            case 4:
                attackPos = 10000;
                break;
        }

        return attackPos;
    }
    
}
