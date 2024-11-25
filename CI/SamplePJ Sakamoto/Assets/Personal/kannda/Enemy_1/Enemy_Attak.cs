using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attak : MonoBehaviour
{
    private int EnemyState;//仮ステータス
    public GameObject State;//参照用オブジェクト

    private void Update()
    {
        //オブジェクトのステータスを取得
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //攻撃("Attak")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Attak)
        {
            Debug.Log("Attak");
        }
    }
}
