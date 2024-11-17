using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State : MonoBehaviour
{
    public int enemyState;//ステータス
    public GameObject Enemy;//参照元オブジェクト
    private GameObject Player;
    private bool DestroyFlag;
    //ステータスに名前をつける
    public enum EnemyState
    {
        Non, Idel, Patrol, Chase, Attak
    };
    private void Start()
    {
        //初期化
        enemyState = (int)EnemyState.Non;//最初は0(Non)

        //プレイヤーオブジェクトを取得
        Player = GameObject.Find("Player");

        DestroyFlag = false;
    }
    private void Update()
    {
        //1度だけNon->Idelにする
        if (enemyState == (int)EnemyState.Non)
        {
            DestroyFlag = true;
            SetState(EnemyState.Idel);
        }

        //再びNonになったらオブジェクトを破壊
        if (enemyState == (int)EnemyState.Non && DestroyFlag)
        {
            Destroy(this.Enemy);
        }
    }
    public void SetState(EnemyState state)
    {
        //ステータス変更関数
        this.enemyState = (int)state;
    } 
    public int GetState()
    {
        //ステータス参照関数
        return this.enemyState;
    }
    //捕まえる仮実装
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            if(enemyState != (int)EnemyState.Non)
            {
                //enemyState = (int)EnemyState.Non;
                Player.GetComponent<Player_cont>().enabled = false;
                //
                Player.GetComponent<Material_Change>().ChangeValue();
            }
        }
    }

}
