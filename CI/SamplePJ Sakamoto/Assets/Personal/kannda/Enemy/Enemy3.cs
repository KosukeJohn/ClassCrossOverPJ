using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyCoppy
{
    float angle;
    float lenge;
    float lenge2;
    Vector3 HideObjPos;
    Vector3[] Pos = new Vector3[4];
    [SerializeField]int seed;
    [SerializeField]Vector3 nextpos;

    protected override void EnterBack()
    {
        anim.Play("Run", 0, 0);
        Debug.Log("Hide");

        // 一番近い点を検索
        FindNerePosition();
    }
    protected override void UpdateBack()
    {
        if (!playerFindFlag) { ChangeStateMachine(TriggerType.EnterIdle); return; }
        if (!checkHideFlag) { ChangeStateMachine(TriggerType.EnterChase); return; }
        bool isDeath = ChangeStateDeath();
        if (isDeath) { return; }

        // オブジェクトの周りを回る

        if (playerPos == null) { return; }

        if (Mathf.Approximately(transform.position.x, nextpos.x))
            if(Mathf.Approximately(transform.position.z,nextpos.z))
            {
                seed++;
                if (seed == 4) { seed = 0; }
                nextpos = Pos[seed];
            }

        transform.position =
           Vector3.MoveTowards(transform.position, nextpos, ChaseSpeed * Time.deltaTime);
    }

    private void FindNerePosition() 
    {
        if (HideObjPos == null) { return; }       
        float[] _lenge = new float[4];

        Pos[0] = new(HideObjPos.x + lenge, 0, HideObjPos.z);// 右
        Pos[1] = new(HideObjPos.x, 0, HideObjPos.z - lenge);// 下
        Pos[2] = new(HideObjPos.x - lenge, 0, HideObjPos.z);// 左
        Pos[3] = new(HideObjPos.x, 0, HideObjPos.z + lenge);// 上

        for (int i = 0; i < 4; i++) 
        {
            Vector3 vector = transform.position - Pos[i];
            _lenge[i] = vector.magnitude;
        }

        int min_index = 0;

        for (int i = 0; i < 4; i++)
        {
            if (_lenge[i] < _lenge[min_index]) 
            {
                min_index = i;
            }
        }

        seed = min_index;
        nextpos = Pos[seed];
    }

    public void SetLenge(float num) { lenge = num; }
    public void SetPos(Vector3 pos) { HideObjPos = pos; }
}
