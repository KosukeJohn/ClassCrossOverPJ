using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Enemy_State;

public class ClassEnemy_Chase : ClassEnemy_State
{
    private float ChaseCnt = 0;//追いかける時間をカウント
    private float CntMax = 3.0f;//３秒間
    private bool PrePosFlag = true;//1回だけ覚えるのに必要
    [SerializeField]
    private Vector3 PrePos;//帰る座標
    protected override void EnemyUpdate()
    {
       // base.EnemyUpdate();
        //追跡("Chase")の時のみ更新処理
        if (enemyState == (int)EnemyState.Chase)
        {
            Debug.Log("Chase");

            //追いかけてるときは赤くなる
            //Enemy.GetComponent<Enemy_Material>().ChangeValueRed();

            //戻るための座標を取得
            if (PrePosFlag)
            {
                PrePos = Enemy_GetPosition();
                PrePosFlag = false;
            }

            //追いかける
            if (Enemy_MoveToPlayer(Player_GetPosition()))
            {
                Debug.Log("return");

                //
                if (Enemy_MoveToPrePos(PrePos))
                {
                    //初期化
                    PrePosFlag = true;
                    ChaseCnt = 0;

                    //待機
                    Debug.Log("Chase->Idel");
                    SetState(EnemyState.Idel);
                    return;
                }
            }

        }
    }
    private bool Enemy_MoveToPlayer(Vector3 pos)
    {
        //プレイヤーに向かって進む

        ChaseCnt += 1.0f * Time.deltaTime;

        //CntMaxになったら追いかけるのをやめる
        if (ChaseCnt >= CntMax)
        {
            return true;
        }

        //移動
        if (Player_FindRay())
        {
            float move = 3.0f;
            Enemy.transform.position =
                Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        }
        else
        {
            return true;
        }
        return false;
    }
    private bool Enemy_MoveToPrePos(Vector3 pos)
    {
        if (Enemy_GetPosition().x >= pos.x - 0.01 && Enemy_GetPosition().x <= pos.x + 0.01)
            if (Enemy_GetPosition().z >= pos.z - 0.01 && Enemy_GetPosition().z <= pos.z + 0.01)
            {
                //浮動小数点数対策
                Enemy.transform.position = pos;

                //ついたらtrue
                Debug.Log("Pos = true");
                return true;
            }

        //移動
        {
            float move = 3.0f;
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        }

        return false;
    }
    private bool Player_FindRay()
    {
        //Rayの生成
        Ray ray = new Ray(Enemy_GetPosition(), Player_GetPosition() - Enemy_GetPosition());
        Debug.DrawRay(ray.origin, ray.direction * 900, Color.blue, 5.0f);
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
}
