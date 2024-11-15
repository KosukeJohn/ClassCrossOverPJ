using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State : MonoBehaviour
{
    public int enemyState;//ステータス
    public GameObject State;//参照元オブジェクト
    private bool DestroyFlag;
    //ステータスに名前をつける
    public enum EnemyState
    {
        Non, Idel, Patrol, Chase, Attak
    };
    private void Start()
    {
        //初期化
        enemyState = (int)EnemyState.Non;//最初は0(Non)
        DestroyFlag = false;
    }
    private void Update()
    {
        //1度だけNon->Idelにする
        if (enemyState == (int)EnemyState.Non)
        {
            DestroyFlag = true;
            SetState(EnemyState.Idel);
        }

        //再びNonになったらオブジェクトを破壊
        if (enemyState == (int)EnemyState.Non && DestroyFlag)
        {
            Destroy(this.State);
        }
    }
    //ステータス変更関数
    public void SetState(EnemyState state)
    {
        this.enemyState = (int)state;
    }
    //ステータス参照関数
    public int GetState()
    {
        return this.enemyState;
    }

}
