using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
<<<<<<< HEAD
=======
//using UnityEditor.MemoryProfiler;
>>>>>>> ecf1f8eaa9c08250d9d43bd5e233d0e86f9cea83
using UnityEngine;

//[System.Serializable]
//public class EnemyMove
//{
//    public string MoveName;
//    public float timer;
//    public int step;
//    public Player target;

//    public void SetTarget(Player t)
//    {
//        target = t;
//    }

//    public void NextStep()
//    {
//        step++;
//        timer = 0;
//    }

//    public void InitStep()
//    {
//        step = 0;
//        timer = 0;
//    }

//    public virtual void Move(Enemy enemy)
//    {
//        Debug.Log("このクラスはベースクラスなので直で使わないでください");
//    }
//}

//public class EnemyMoveIdle : EnemyMove
//{
//    public EnemyMoveIdle()
//    {
//        MoveName = "Idle";
//    }
    
//    public override void Move(Enemy enemy)
//    {
//        // IDLEなりの処理をさせる
//    }
//}

public class ClassEnemy_State : MonoBehaviour
{
    public GameObject Enemy;//参照元オブジェクト
    protected int enemyState;//ステータス
    protected GameObject Player;
    private bool DestroyFlag;
    protected virtual void EnemyUpdate()
    {
        Debug.Log("オーバーライド中");
    }

    //ステータスに名前をつける
    protected enum EnemyState
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
    private void FixedUpdate()
    {
        //1度だけNon->Idelにする
        if (enemyState == (int)EnemyState.Non)
        {
            DestroyFlag = true;
            SetState(EnemyState.Idel);
        }

        //ステータスがNonでなければオーバーライド
        if(enemyState!=(int)EnemyState.Non)
        {
            EnemyUpdate();
        }

        //再びNonになったらオブジェクトを破壊
        if (enemyState == (int)EnemyState.Non && DestroyFlag)
        {
            Destroy(this.Enemy);
        }
    }
    protected void SetState(EnemyState state)
    {
        //ステータス変更関数
        this.enemyState = (int)state;
    }
    protected int GetState()
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
            if (enemyState != (int)EnemyState.Non)
            {
                //enemyState = (int)EnemyState.Non;
                //Player.GetComponent<Player_cont>().enabled = false;
                //
               // Player.GetComponent<Material_Change>().ChangeValue();
            }
        }
    }
    protected Vector3 Enemy_GetPosition()
    {
        //Enemyの位置を取得
        return this.Enemy.transform.position;
    }
    protected Vector3 Player_GetPosition()
    {
        //プレイヤーのX,Z座標を取得
        Vector3 pos =
            new Vector3(Player.transform.position.x, Enemy.transform.position.y, Player.transform.position.z);
        return pos;
    }
}