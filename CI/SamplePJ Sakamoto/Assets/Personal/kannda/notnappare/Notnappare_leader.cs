using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class Notnappare_leader : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject leader;//敵のオブジェクト
    private GameObject player;//プレイヤーのオブジェクト
    private Vector3 firstpos;//最初の位置
    private Vector3 prepos;//戻る位置
    private Vector3 playerpos;//発見した時のプレイヤーの位置
    private float lenge;//移動の幅
    private float moveCnt;//移動の時間
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    [SerializeField] private State state;//ステータス
    [SerializeField] private int nextseed;//シード値
    //---------------------------------------------
    //ステータスの定義
    //---------------------------------------------
    private enum State
    {
        Non,Idle, Patrol, Find, Chase, Back 
    };

    private void Start()
    {
        //初期化
        {
            leader = this.gameObject;
            player = GameObject.Find("Player");
            firstpos = leader.transform.position;
            leader.transform.position = NextPosition(6);
            nextseed = 7;
            lenge = 3.0f;//移動の幅
            moveCnt = 0;
            SetState(State.Idle);
        }
    }

    private void FixedUpdate()
    {
        if(state == State.Idle)
        {
            //全員集まったらPatrol
            SetState(State.Patrol);
        }

        if (state == State.Patrol)
        {
            if(PatrolMove(NextPosition(nextseed)))
            {
                //プレイヤーを見つけたらFindのなる
                state = State.Find;
            }
        }

        if (state == State.Find)
        {
            //位置を覚える
            playerpos = PlayerGetPosition();
            prepos = leader.transform.position;

            //追跡に移行
            SetState(State.Chase);
        }
        if (state == State.Chase)
        {
            if(ChaseMove(playerpos))
            { 
                //playerfindをfalseにする
                leader.GetComponentInChildren<Collider_cont_notnappare>().SetPlayerFind(false);

                //元の位置に戻る
                SetState(State.Back);
            }
        }
        if(state == State.Back)
        {
            if (BackMove(prepos))
            {
                //元の位置に戻ったら待機に移行
                SetState(State.Idle);
            }
        }
    }
    private State GetState(){ return this.state; }
    private void SetState(State s) {  this.state = s; }
    private Vector3 PlayerGetPosition()
    {
        //プレイヤーの位置を取得
        return player.transform.position;
    }
    private bool PlayerFind()
    {
        //子オブジェクトから索敵範囲にプレイヤーが入ったか取得
        bool playerfind = 
            leader.GetComponentInChildren<Collider_cont_notnappare>().GetPlayerFind();

        if(playerfind)//プレイヤーを見つけた
        {
            return true;
        }

        return false;
    }
    private bool PatrolMove(Vector3 pos)
    {
        if(PlayerFind())//プレイヤーを見つけた
        {
            return true;
        }

        //次の位置に敵がついたら
        if (Mathf.Approximately(leader.transform.position.x,pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            //浮動小数点対策
            leader.transform.position = pos;

            if(nextseed % 2 == 0)//曲がり角かどうか判断
            {
                if (EnemyTurn(nextseed))//回転させる
                {
                    //シード値を増やす
                    nextseed++;
                    return false;
                }
            }
            else
            {
                //シード値を増やす
                nextseed++;
                return false;
            }
            
        }
        else
        {
            //次の位置まで移動させる
            float move = 3.0f;
            leader.transform.position =
                Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);

            //MoveJump();
        }
       
        return false;
    }
    private void MoveJump(/*Vector3 pos*/)
    {
        Vector3 vector = leader.transform.position;
        int jumpCnt = 0;
        float height = 3.0f;

        moveCnt += 1.0f * Time.deltaTime;

        if(moveCnt >= 1.0f)
        {
            moveCnt = 0;
            jumpCnt++;
        }

        //横移動
        {
            float posx = (lenge / 3 * jumpCnt) + lenge / 3 * moveCnt;
            vector.x += posx;
        }

        //縦移動
        {
            float X = moveCnt - (lenge / 3 * jumpCnt + lenge / 6);
            vector.y = -(X * X) + height;
        }

        leader.transform.position = vector;
    }
    private bool EnemyTurn(int seed)
    {
        float anglespeed = 3.0f;//回転速度
        float turnMax = 0.6f;//回転時間

        //カウントさせる
        moveCnt += 1.0f * Time.deltaTime;

        if (moveCnt >= turnMax)//回転時間になったら
        {
            //カウントを0にする
            moveCnt = 0;
            return true;
        }

        //回転させる
        transform.Rotate(0, anglespeed, 0, Space.Self);

        return false;
    }
    private Vector3 NextPosition(int next)
    {
        Vector3 pos = firstpos;

        switch(next % 8)
        {
            case 0:
                pos.x -= lenge;
                pos.z += lenge;
                break;
            case 1:
                pos.z += lenge;
                break;
            case 2:
                pos.x += lenge;
                pos.z += lenge;
                break;
            case 3:
                pos.x += lenge;
                break;
            case 4:
                pos.x += lenge;
                pos.z -= lenge;
                break;
            case 5:
                pos.z -= lenge;
                break;
            case 6:
                pos.x -= lenge;
                pos.z -= lenge;
                break;
            case 7:
                pos.x -= lenge;
                break;
        }

        return pos;
    }
    private bool ChaseMove(Vector3 pos)
    {
        //プレイヤーがいた位置に敵がついたら
        if (Mathf.Approximately(leader.transform.position.x, pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            //浮動小数点対策
            leader.transform.position = pos;
            return true;
        }

        //移動させる
        float move = 10.0f;
        leader.transform.position =
            Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);

        return false;
    }
    private bool BackMove(Vector3 pos)
    {
        //元の位置に戻ったら
        if (Mathf.Approximately(leader.transform.position.x, pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            //浮動小数点対策
            leader.transform.position = pos;
            return true;
        }

        //元の位置に戻る
        float move = 3.0f;
        leader.transform.position =
            Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);

        return false;
    }
}