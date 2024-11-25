using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enenmy_Idel : ClassEnemy_State
{
    public float FindDir;//プレイヤー探索範囲
    protected override void EnemyUpdate()
    {
        //base.EnemyUpdate();
        if (enemyState == (int)EnemyState.Idel)
        {
            Debug.Log("Idel");
            //Enemy.GetComponent<Enemy_Material>().ChangeValueWhite();

            //探索範囲内にプレイヤーは存在するか
            if (Player_Find(Enemy.transform.position.x, Enemy.transform.position.z))
            {
                Debug.Log("find");

                //プレイヤーと自分の間に障害物はないか
                if (Player_FindRay())
                {
                    //プレイヤーを追跡
                    Debug.Log("Idel->Chase");
                    SetState(EnemyState.Chase);
                    return;
                }
            }

            //巡回
            Debug.Log("Idel->Patrol");
            SetState(EnemyState.Patrol);
            return;
        }
    }
    private bool Player_Find(float enemyX, float enemyZ)
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
