using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    private int EnemyState;//仮ステータス
    public GameObject State;//参照用オブジェクト

    private void Update()
    {
        //オブジェクトのステータスを取得
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //探索("Patrol")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Patrol)
        {
            Debug.Log("Patrol");
        } 
    }
}
