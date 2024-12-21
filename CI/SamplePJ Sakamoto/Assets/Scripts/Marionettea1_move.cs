using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Marionettea1_move : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject player;//プレイヤーオブジェクト
    private GameObject enemy;//敵のオブジェクト   
    private Animator anim;//アニメーション
    private float BornCnt;//動くまでの時間
    private float ChaseCnt;//追いかける時間
    private float ChaseCntMax;//あきらめるまでの時間
    private float DestroyCnt;//死ぬ時間
    private float DestroyCntMax;//死ぬまでの時間
    private float animCnt;//アニメーションの時間
    private bool destroyFlag;//破壊フラグ
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    [SerializeField] private State state;//ステータス
    [SerializeField] private Vector3 pos;//マリオネットの位置
    //---------------------------------------------
    //stateの定義
    //---------------------------------------------
    private enum State {
        Non, Born, Chase, Idle
    };

    private void Start()
    {
        //初期化
        {
            player = GameObject.Find("Player");
            enemy = this.gameObject;
            BornCnt = 2.0f;
            ChaseCnt = 0;
            ChaseCntMax = 15.0f;
            DestroyCnt = 0;
            DestroyCntMax = 1.0f;
            state = State.Born;
            animCnt = 0;
            anim = GetComponent<Animator>();
            destroyFlag = false;
        }
    }

    private void FixedUpdate()
    {

        if(state == State.Born)
        {
            //カウントさせる
            if(AnimCnt(BornCnt))
            {
                if (destroyFlag)//2回目の処理
                {
                    //カウントさせなくする
                    animCnt = BornCnt;

                    //移動
                    float move = 10.0f;
                    enemy.transform.position =
                        Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);

                    //プレイヤーと離れたら破壊
                    if (player.transform.position.x - enemy.transform.position.x >= 20.0f)
                    {
                        //破壊
                        Destroy(enemy);
                    }
                }
                else//1回目の処理
                {
                    pos = transform.position;//帰る位置を定義
                    ChangeStateAnim(State.Chase);//ステータスの変更
                    destroyFlag = true;//処理を変更させるためのフラグ
                }
            }
        }
     
        if (state == State.Chase)//ステータスが追いかける(Chase)の時
        {
            if (Enemy_MoveToPlayer(Player_GetPosition()))//プレイヤーに向かっていく
            {
                //あきらめたら待機状態にする
                ChangeStateAnim(State.Idle);
            }
        }

        if (state == State.Idle)//ステータスが待機(Idle)の時
        {
            if(Enemy_DestroyCnt())//カウントする
            {
                //戻る
                ChangeStateAnim(State.Born);
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
        //プレイヤーが索敵範囲にいるか判断
        bool find = enemy.GetComponentInChildren<Collider_Controller>().GetFindFlag();
        if (find)
        {
            //移動
            float move = 5.0f;
            enemy.transform.position =
                Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);
        }
        else
        {
            //回転させる
            enemy.transform.Rotate
                (0f, 3.0f * enemy.GetComponentInChildren<Collider_Controller>().GetDirection(), 0f);
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
    private bool AnimCnt(float cntMax)
    {
        //カウントさせる
        animCnt += 1.0f * Time.deltaTime;

        //引数をもとに指定の時間がたつとカウント終了
        if (animCnt >= cntMax)
        {
            animCnt = 0;
            return true;
        }

        return false;
    }
    private void ChangeStateAnim(State state_)
    {
        //引数をもとにアニメーションを変更
        this.state = state_;
        int Anim = (int)this.state;
        anim.SetInteger("Marionnett_anim", Anim);
    }
}