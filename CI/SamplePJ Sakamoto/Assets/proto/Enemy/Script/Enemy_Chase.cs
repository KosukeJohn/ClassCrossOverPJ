using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Chase : MonoBehaviour
{
    private int EnemyState;//仮ステータス
    public GameObject Enemy;//参照用オブジェクト
    private GameObject Player;//プレイヤーオブジェクト
    private float ChaseCnt = 0;//追いかける時間をカウント
    private float CntMax = 3.0f;//３秒間
    private bool PrePosFlag = true;//1回だけ覚えるのに必要
    [SerializeField]
    private Vector3 PrePos;//帰る座標

    private void Start()
    {
        //プレイヤーオブジェクトを取得
        Player = GameObject.Find("Player");
    }
    private void FixedUpdate()
    {
        //オブジェクトのステータスを取得
        EnemyState = Enemy.GetComponent<Enemy_State>().GetState();

        //追跡("Chase")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Chase)
        {
            Debug.Log("Chase");

            //追いかけてるときは赤くなる
            Enemy.GetComponent<Enemy_Material>().ChangeValueRed();

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
                    PrePosFlag = false;
                    ChaseCnt = 0;

                    //待機
                    Debug.Log("Chase->Idel");
                    Enemy.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Idel);
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
        if(ChaseCnt >= CntMax)
        {
            return true;
        }
        
        //移動
        {
            float move = 3.0f;
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
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
    private Vector3 Enemy_GetPosition()
    {
        //Enemyの位置を取得
        return this.Enemy.transform.position;
    }
    private Vector3 Player_GetPosition()
    {
        //プレイヤーのX,Z座標を取得
        Vector3 pos = 
            new Vector3(Player.transform.position.x, Enemy.transform.position.y, Player.transform.position.z);
        return pos;
    }
}
