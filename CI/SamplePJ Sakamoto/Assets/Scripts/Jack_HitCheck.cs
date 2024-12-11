using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Jack_HitCheck : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject hitcheck;//このオブジェクト
    private GameObject jack;//親オブジェクト
    private GameObject playerhitcheck;//当たり判定管理者
    private Collider coll;//コライダー
    private Vector3 firstpos;//最初の位置
    private bool hitflag;//当たり判定
    private int state;//jackのstate参照用
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------


    private void Start()
    {
        //初期化
        {
            hitcheck = this.gameObject;
            jack = transform.parent.gameObject;
            playerhitcheck = GameObject.Find("PlayerHitCheck");
            coll = hitcheck.GetComponent<Collider>();
            hitflag = false;
            firstpos = this.transform.position;
        }
    }

    private void FixedUpdate()
    {
        //stateを参照
        state = jack.GetComponent<jack_controller>().GetState();

        //コライダーの位置をstateもとに移動
        ColliderPosUpdate(state);

        //一応findの戻ったら当たり判定を初期化、必要無いかも
        if (state == 1)
        {
            hitflag = false;
        }

        //富士見バグtodo
        //当たり判定を管理者に送る
        //playerhitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitflag);
    }

    private void ColliderPosUpdate(int state)
    {
        switch(state)
        {
            case 0:
                break;
            case 1:
                //浮動小数点対策
                hitcheck.transform.position = firstpos;
                break;
            case 2:
                //前に進める
                hitcheck.transform.position += (hitcheck.transform.forward) * 0.01f;
                break;    
            case 3:
                //処理なし
                break;
            case 4:
                //元の位置にもどす
                float move = 3.0f;
                hitcheck.transform.position =
                    Vector3.MoveTowards(hitcheck.transform.position, firstpos, move * Time.deltaTime);
                break;
            default:
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //範囲に侵入したらtrueを返す
        if(other.tag == "Player")
        {
            Debug.Log("jack->Hit");
            hitflag = true;

            //当たり判定を管理者に送る
            playerhitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitflag);
        }
    }
}
