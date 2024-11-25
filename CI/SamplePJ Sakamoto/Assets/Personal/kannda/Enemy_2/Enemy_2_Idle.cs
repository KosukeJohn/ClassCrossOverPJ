using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy_2_Idle : MonoBehaviour
{
    private GameObject Player;//プレイヤー
    private GameObject Enemy;//Enemy
    private Vector3 Pos1;//プレイヤの位置を覚えるため
    private Vector3 Pos2;//次の位置を覚える
    private float TimeCnt = 0;//カウント
    private float FindDir = 300.0f;//PlayerFindの指定範囲
    private float EnemyHight = 10.0f;//高さは固定

    private void Start()
    {
        //プレイヤーオブジェクトを取得
        Player = GameObject.Find("Player");
        //Lightをtrueにする
        GetComponentInChildren<Light>().enabled = true;
        //Enemyを取得する
        this.Enemy = this.gameObject;
    }
    private void FixedUpdate()
    { 
        if (PlayerFind())
        {
            if(TimeCnt == 0)
            {
                //最初の位置を覚える
                Pos1 = Player_GetPosition();
            }
            if(TimeCnt >= 1.0)
            {
                //１秒間に進んだ距離を覚える
                Pos2 = Player_GetPosition();
                this.Enemy.transform.position = NextPosition(Pos1, Pos2); 
            }
            if(TimeCnt >= 2.0)
            {
                //AddComponentでChaseを呼び出して不要なこのスクリプトを破壊
                GetComponentInChildren<Light>().enabled = false;
                Enemy.AddComponent<Enemy_2_Chase>();
                Destroy(Enemy.GetComponent<Enemy_2_Idle>());
                return;
            }
            //時間を測る
            TimeCnt += 1.0f * Time.deltaTime;

        }
    }
    private Vector3 Player_GetPosition()
    {
        //プレイヤーの位置を取得
        return this.Player.transform.position;
    }
    private bool PlayerFind()
    {
        //X,Z座標で判断する
        Vector2 playerdir;
        playerdir.x = Player.transform.position.x;
        playerdir.y = Player.transform.position.z;
        Vector2 enemydir;
        enemydir.x = Enemy.transform.position.x;
        enemydir.y = Enemy.transform.position.z;
        Vector2 dir = playerdir - enemydir;
        float d = dir.magnitude;

        //指定された範囲にプレイヤーが存在するか
        if (d < FindDir)
        {
            return true;
        }
        return false;
    }
    private Vector3 NextPosition(Vector3 pos1,Vector3 pos2)
    {
        //進んだ距離を求める
        Vector3 pos = pos2 - pos1;
        //今のプレイヤーの位置に次に進みそうな距離を足す
        pos += Player_GetPosition();
        //高さをもどす
        pos.y = EnemyHight;
        return pos;
    }
}
