using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private bool hit;//各方面からhitを検索する
    private bool hitFlag;//hitしたらtrueを返す、こっちのみを参照する！！
    private GameObject player;//プレイヤー
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    private bool playerhide;

    private void Start()
    {
        //初期化
        {
            hit = false;
            hitFlag = false;
            this.player = GameObject.Find("Player");
        }
    }

    private void Update()
    {
        if (hit)
        {
            //まだ当たっただけ
            //playerが隠れているか参照する
            playerhide = player.GetComponent<PlayerController>().IsHiding;

            //隠れていなければ当たったことにする
            if (!playerhide)
            {
                hitFlag = true;
            }
        }

        if (hitFlag)
        {
            //当たった処理が可能になる
            Debug.Log("PlayerHit");
        }
    }

    //---------------------------------------------
    //参照可能関数
    //---------------------------------------------
    public void SetPlayerHitCheck(bool flag)
    {
        //hitしたか判断する時に使う
        this.hit = flag;
    }
    public bool GetPlayerHitCheck()
    {
        //参照用
        return this.hitFlag;
    }
}
