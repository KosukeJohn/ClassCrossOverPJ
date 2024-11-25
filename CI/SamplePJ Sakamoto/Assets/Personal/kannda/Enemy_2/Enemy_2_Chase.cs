using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy_2_Chase : MonoBehaviour
{
    private GameObject Enemy;//Enemy
    private GameObject Player;//プレイヤー
    private Rigidbody rig;//Rigidbody
    private Vector3 PrePos;//戻る位置
    private bool Flag;//落下と上昇のフラグ
    private float PreY = 10000.0f;//とりあえずの初期値

    private void Start()
    {
        //Enemyを取得
        this.Enemy = this.gameObject;
        //戻る位置を取得
        PrePos = this.Enemy.transform.position;
        //プレイヤーを取得
        this.Player= GameObject.Find("Player");
        //Rigidbodyを追加
        this.rig = Enemy.AddComponent<Rigidbody>();
        //Rotationを全てオン
        this.rig.constraints = RigidbodyConstraints.FreezeRotation;  
        Flag = false;
    }
    private void FixedUpdate()
    {
        if (!Flag)
        {
            if (EnemyMove())
            {
                //フラグをtrueにして上昇へ
                Flag = true;
            }
        }
        if(Flag)
        {
            if(MoveToPrePos(PrePos))
            {
                //AddComponentでIdleを呼び出して不要なこのスクリプトを破壊
                Enemy.AddComponent<Enemy_2_Idle>();
                Destroy(Enemy.GetComponent<Enemy_2_Chase>());
                return;
            }
        }
    }
    private bool EnemyMove()
    {
        //1フレーム前のＹの値が今と同じならtrue 
        if(Mathf.Approximately
            (this.Enemy.transform.position.y, PreY))
        {
            Destroy(Enemy.GetComponent<Rigidbody>());
            return true;
        }

        //Yの値を取得
        PreY = this.Enemy.transform.position.y;
        return false;
    }
    private bool MoveToPrePos(Vector3 pos)
    {
        //元の位置に戻ったらtrue
        if (Mathf.Approximately
            (this.Enemy.transform.position.y, pos.y))
        {
            return true;
        }

        //元の位置に向かう
        float move = 5.0f;
        Enemy.transform.position =
            Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        return false;
    }
}
