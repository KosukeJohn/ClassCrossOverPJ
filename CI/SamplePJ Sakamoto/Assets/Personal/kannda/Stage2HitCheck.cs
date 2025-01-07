using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2HitCheck : MonoBehaviour
{
    private GameObject enemy;
    private float timeCnt;
    private float attackTime;
    private bool attackFlag;
    
    void Start()
    {
        enemy = this.gameObject;
        timeCnt = 0;
        attackFlag = false;
    }

    void Update()
    {
        if (timeCnt == 0)
        {
            attackTime = Random.Range(1, 6);
            attackFlag = false;
        }

        timeCnt += Time.deltaTime * 1.0f;

        if (timeCnt >= attackTime)
        { 
            attackFlag = true;
            timeCnt = 0;
        }
    }
    public bool GetAttackFlag()
    {
        return attackFlag;
    }
    
}
