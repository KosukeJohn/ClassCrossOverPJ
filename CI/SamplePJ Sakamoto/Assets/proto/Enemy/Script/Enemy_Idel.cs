using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idel : MonoBehaviour
{
    private int         EnemyState;//仮ステータス
    public  GameObject  State;//参照用オブジェクト
    private GameObject Player;//プレイヤーオブジェクト
    public float FindDir;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        //オブジェクトのステータスを取得
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //待機("Idel")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Idel)
        {
            Debug.Log("Idel");

            //ステータスの遷移
            if (Player_Find(State.transform.position.x, State.transform.position.z))
            {
                Debug.Log("Idel->Chase");
                State.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Chase);
            }
            else
            {
                Debug.Log("Idel->Patrol");
                State.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Patrol);
            }
        }
            
    }
    private bool Player_Find(float enemyX,float enemyZ)
    {
        Vector3 playerpos = Player.transform.position;
        float playerX = playerpos.x;
        float playerZ = playerpos.z;
        float dir = (playerX + enemyX) * (playerX + enemyX) +
            (playerZ + enemyZ) * (playerZ + enemyZ);

        if (Mathf.Sqrt(dir) < FindDir)
        {
            return true;
        }

        return false;
    }
}
