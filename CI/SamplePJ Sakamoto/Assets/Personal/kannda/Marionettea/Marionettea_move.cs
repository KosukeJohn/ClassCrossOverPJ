using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Marionettea_move : MonoBehaviour
{
    private GameObject player;//プレイヤーオブジェクト
    private GameObject enemy;//敵のオブジェクト
    private Animation anim;//アニメーション
    private float ChaseCnt;//追いかける時間
    private float ChaseCntMax;//あきらめるまでの時間
    private float DestroyCnt;//死ぬ時間
    private float DestroyCntMax;//死ぬまでの時間
    private State state;//ステータス
    //stateの定義
    private enum State {
        Non,Born,Idle,Chase
    };

    private void Start()
    {
        //初期化
        {
            player = GameObject.Find("Player");
            enemy = this.gameObject;
            ChaseCnt = 0;
            ChaseCntMax = 5.0f;
            DestroyCnt = 0;
            DestroyCntMax = 5.0f;
            state = State.Chase;
        }
        //未実装
#if false
        anim = GetComponent<Animation>();
#endif
    }

    private void FixedUpdate()
    {
        //プレイヤーが隠れているか判断
        //bool playerhide = player.GetComponent<PlayerController>().isHiding;<-Not public

        
#if false

    if(!playerhide)
    {
        if (state == State.Chase)
        {
            if(Enemy_MoveToPlayer(Player_GetPosition()))
            {
                state = State.Idle;
            }
        }
    }
#else
        if (state == State.Chase)//ステータスが追いかける(Chase)の時
        {
            if(Enemy_MoveToPlayer(Player_GetPosition()))//プレイヤーに向かっていく
            {
                //あきらめたら待機状態にする
                state = State.Idle;
            }
        }
#endif

        if(state == State.Idle)//ステータスが待機(Idle)の時
        {
            if(Enemy_DestroyCnt())//カウントする
            {
                //破壊
                Destroy(enemy);
            }
        }
    }
    private bool Enemy_MoveToPlayer(Vector3 pos)
    {
        //プレイヤーに向かって進む

        //タイマーを動かす
        ChaseCnt += 1.0f * Time.deltaTime;

        //CntMaxになったら追いかけるのをやめる
        if (ChaseCnt >= ChaseCntMax)
        {
            return true;
        }
        //プレイヤーが扇の範囲にいるか判断
        bool find = enemy.GetComponentInChildren<Collider_Controller>().playerfind;
        if (find)
        {
            //移動
            float move = 3.0f;
            enemy.transform.position =
                Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);
        }
        else
        {
            //回転させる
            enemy.transform.Rotate
                (0f, 3.0f * enemy.GetComponentInChildren<Collider_Controller>().num, 0f);
        }
        return false;
    }
    private bool Enemy_DestroyCnt()
    {
        //タイマーを動かす
        DestroyCnt += 1.0f * Time.deltaTime;

        if(DestroyCnt > DestroyCntMax)//時間になった
        {
            return true;
        }
        return false;
    }
    private Vector3 Enemy_GetPosition()
    {
        //Enemyの位置を取得
        Vector3 pos = 
            new Vector3(this.enemy.transform.position.x, this.enemy.transform.position.y + 0.5f, this.enemy.transform.position.z);
        return pos;
    }
    private Vector3 Player_GetPosition()
    {
        //プレイヤーのX,Z座標を取得
        Vector3 pos =
            new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z);
        return pos;
    }
    //アニメーション未実装
    private void Animation(State state)
    {
        switch(state)
        {
            case State.Idle:

                break;
            case State.Born:

                break;
            case State.Chase:

                break;
            default:
                break;
        }
    }
}
