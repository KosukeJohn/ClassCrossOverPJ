using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idel : MonoBehaviour
{
    private int         EnemyState;//仮ステータス
    public  GameObject  Enemy;//参照用オブジェクト
    private GameObject Player;//プレイヤーオブジェクト
    public float FindDir;//プレイヤー探索範囲

    private void Start()
    {
        //プレイヤーオブジェクトを取得
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        //オブジェクトのステータスを取得
        EnemyState = Enemy.GetComponent<Enemy_State>().GetState();

        //待機("Idel")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Idel)
        {
            Debug.Log("Idel");

            //探索範囲内にプレイヤーは存在するか
            if (Player_Find(Enemy.transform.position.x, Enemy.transform.position.z))
            {
                Debug.Log("find");

                //プレイヤーと自分の間に障害物はないか
                if (Player_FindRay())
                {
                    //プレイヤーを追跡
                    Debug.Log("Idel->Chase");                    
                    Enemy.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Chase);
                    return;
                }
            }

            //巡回
            Debug.Log("Idel->Patrol");          
            Enemy.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Patrol);
            return;
        }
            
    }
    private bool Player_Find(float enemyX,float enemyZ)
    {
        //X,Z座標で判断する
        Vector2 playerdir;
        playerdir.x = Player.transform.position.x;
        playerdir.y = Player.transform.position.z;
        Vector2 enemydir;
        enemydir.x = enemyX;
        enemydir.y = enemyZ;
        Vector2 dir = playerdir - enemydir;
        float d = dir.magnitude;

        //指定された範囲にプレイヤーが存在するか
        if (d < FindDir)
        {
            return true;
        }
        return false;
    }
    private bool Player_FindRay()
    {
        //Rayの生成
        Ray ray = new Ray(Enemy.transform.position, Player_coll_GetPosition() - Enemy.transform.position);
        Debug.DrawRay(ray.origin, ray.direction * 900, Color.red, 5.0f);
        RaycastHit hit;

        //プレイヤーとrayが接触したか判断
        if (Physics.Raycast(ray, out hit))
        {
            //プレイヤーかタグで判断
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("hit");
                return true;
            }
        }
        return false;
    }
    private Vector3 Player_coll_GetPosition()
    {
        //プレイヤーのコライダーの位置を取得
        return Player.GetComponent<Collider>().transform.localPosition; ;
    }
}
