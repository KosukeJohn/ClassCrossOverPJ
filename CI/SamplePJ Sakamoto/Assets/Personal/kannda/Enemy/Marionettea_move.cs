using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marionettea_move : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private float ChaseCnt = 0;
    private float CntMax;

    private void Start()
    {
        player = GameObject.Find("Player");
        enemy = this.gameObject;
        CntMax = 300.0f;
    }

    private void FixedUpdate()
    {
        bool flag = enemy.GetComponentInChildren<Collider_Controller>().playerfind;

        if (flag)
        {
            if (Enemy_MoveToPlayer(Player_GetPosition()))
            {
                //Destroy(enemy);
            }
        }
        else
        {
            //回転させる
            transform.Rotate(0f,3.0f * enemy.GetComponentInChildren<Collider_Controller>().num,0f);
        }
        
    }
    private bool Enemy_MoveToPlayer(Vector3 pos)
    {
        //プレイヤーに向かって進む

        ChaseCnt += 1.0f * Time.deltaTime;

        //CntMaxになったら追いかけるのをやめる
        if (ChaseCnt >= CntMax)
        {
            return true;
        }

        //移動
        float move = 3.0f;
        enemy.transform.position =
            Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);

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

}
