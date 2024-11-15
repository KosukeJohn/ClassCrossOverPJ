using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase : MonoBehaviour
{
    private int EnemyState;//仮ステータス
    public GameObject State;//参照用オブジェクト

    private void Update()
    {
        //オブジェクトのステータスを取得
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //追跡("Chase")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Chase)
        {
            Debug.Log("Chase");
        }   
    }
}
