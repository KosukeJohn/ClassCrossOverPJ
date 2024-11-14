using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idel : MonoBehaviour
{
    private int         EnemyState;//仮ステータス
    public  GameObject  State;//参照用オブジェクト

    private void Update()
    {
        //オブジェクトのステータスを取得
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //待機("Idel")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Idel)
        {
            Debug.Log("Idel");

            //ステータスの遷移
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Idel->Attak");
                State.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Attak);
            }
        }
            
    }
}
