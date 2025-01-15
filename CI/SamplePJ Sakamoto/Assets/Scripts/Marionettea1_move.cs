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
    private Light redLight;
    private Animator anim;//アニメーション
    private float BornCnt;//動くまでの時間
    private float ChaseCnt;//追いかける時間
    private float animCnt;//アニメーションの時間
    //private bool destroyFlag;//破壊フラグ
    private float StageEnd_X1 = 63.66f;//ステージ１の終わり
    
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    [SerializeField] private State state;//ステータス
    [SerializeField] private Vector3 pos;//マリオネットの位置
    [SerializeField] AudioSource source;//オーディオソース
    [SerializeField] AudioClip attack;
    //---------------------------------------------
    //バランス調整
    //---------------------------------------------
    private float ChaseCntMax = 5.0f;//あきらめるまでの時間
    private float ChaseSpeed = 6.0f;//追いかけるスピード
    //---------------------------------------------
    //stateの定義
    //---------------------------------------------
    private enum State {
        Non, Born, Chase, Idle, Attack ,Death
    };

    private void Start()
    {
        //初期化
        {
            player = GameObject.Find("Player");
            enemy = this.gameObject;
            BornCnt = 1.5f;
            ChaseCnt = 0;
            state = State.Born;
            animCnt = 0;
            anim = GetComponent<Animator>();
            //destroyFlag = false;
            redLight = transform.GetChild(0).GetComponent<Light>();
            redLight.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        ////ステージ１の終了地点を超えたか
        //if(enemy.transform.position.x >= StageEnd_X1)
        //{
        //    //マリオネットを破壊する
        //    state = State.Death;
        //}

        //生まれた時の処理
        if(state == State.Born)
        {
           
            //カウントさせる
            if(AnimCnt(BornCnt))
            {
                pos = transform.position;//帰る位置を定義
                ChangeState(State.Chase);//ステータスの変更
                ChangeAnim(State.Chase);//ステータスの変更
                redLight.enabled = true;
            }
        }

        //攻撃時の処理
        if (state == State.Attack)
        {
           
            //驚かすアニメーションにする
            ChangeAnim(State.Born);
            
            if (AnimCnt(BornCnt))
            {
                ////アニメーションが終わったら追跡に戻る
                //ChangeState(State.Chase);//ステータスの変更
                //ChangeAnim(State.Chase);//ステータスの変更

                ChangeState(State.Death);//ステータスの変更
                ChangeAnim(State.Chase);//ステータスの変更
            }
        }

        //追跡時の処理
        if (state == State.Chase)//ステータスが追いかける(Chase)の時
        {
            if (Enemy_MoveToPlayer(Player_GetPosition()))//プレイヤーに向かっていく
            {
                //あきらめたら待機状態にする
                ChangeState(State.Attack);
                ChangeAnim(State.Idle);
            }
        }

        //死亡時の処理
        if (state == State.Death)
        {
            //移動
            float move = 10.0f;
            enemy.transform.position =
                Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);

            //回転させる
            enemy.transform.Rotate(0f, 30.0f , 0f);

            //プレイヤーと離れたら破壊
            if (player.transform.position.x - enemy.transform.position.x >= 20.0f)
            {
                //破壊
                Destroy(enemy);
            }
        }
    }
    private bool Enemy_MoveToPlayer(Vector3 pos)
    {
        //プレイヤーに向かって進む

        //ステージ１の終了地点を超えたか
        if (enemy.transform.position.x >= StageEnd_X1)
        {
            source.clip = attack;
            source.Play();
            //マリオネットを破壊する
            return true;
        }

        ////タイマーを動かす
        //ChaseCnt += 1.0f * Time.deltaTime;

        ////CntMaxになったら追いかけるのをやめる
        //if (ChaseCnt >= ChaseCntMax)
        //{
        //    ChaseCnt = 0;
        //    return true;
        //}

        //プレイヤーが索敵範囲にいるか判断
        bool find = enemy.GetComponentInChildren<Collider_Controller>().GetFindFlag();
        if (find)
        {
            //移動
            enemy.transform.position =
                Vector3.MoveTowards(enemy.transform.position, pos, ChaseSpeed * Time.deltaTime);
        }
        else
        {
            //回転させる
            enemy.transform.Rotate
                (0f, 3.0f * enemy.GetComponentInChildren<Collider_Controller>().GetDirection(), 0f);
        }

        return false;
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
    private void ChangeAnim(State state_)
    {
        if (state_ == State.Non) { return; }
        if (state_ == State.Death) { return; }
        if (state_ == State.Attack) { return; }

        //引数をもとにアニメーションを変更
        int Anim = (int)state_;
        anim.SetInteger("Marionnett_anim", Anim);
    }
    private void ChangeState(State state_)
    {
        //Nonは受け取らない
        if (state_ == State.Non) { return; }

        //引数をもとにステータスを変更
        this.state = state_;

    }
    public bool GetAttackFlag()
    {
        bool attackFlag = false;

        if(state == State.Attack)
        {
            attackFlag = true;
        }

        return attackFlag;
    }
}