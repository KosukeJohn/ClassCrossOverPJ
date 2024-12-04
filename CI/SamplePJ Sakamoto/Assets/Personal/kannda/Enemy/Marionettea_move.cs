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
        CntMax = 3.0f;
    }

    private void FixedUpdate()
    {
        if(Enemy_MoveToPlayer(Player_GetPosition()))
        {
            //Destroy(enemy);
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
    private bool Enemy_MoveToPrePos(Vector3 pos)
    {
        if (Enemy_GetPosition().x >= pos.x - 0.01 && Enemy_GetPosition().x <= pos.x + 0.01)
            if (Enemy_GetPosition().z >= pos.z - 0.01 && Enemy_GetPosition().z <= pos.z + 0.01)
            {
                //浮動小数点数対策
                enemy.transform.position = pos;

                //ついたらtrue
                Debug.Log("Pos = true");
                return true;
            }

        //移動
        {
            float move = 3.0f;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);
        }

        return false;
    }
    private Vector3 Enemy_GetPosition()
    {
        //Enemyの位置を取得
        return this.enemy.transform.position;
    }
    private Vector3 Player_GetPosition()
    {
        //プレイヤーのX,Z座標を取得
        Vector3 pos =
            new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z);
        return pos;
    }
    private bool Player_FindRay()
    {
        //Rayの生成
        Ray ray = new Ray(Enemy_GetPosition(), Player_GetPosition() - Enemy_GetPosition());
        Debug.DrawRay(ray.origin, ray.direction * 900, Color.blue, 5.0f);
        RaycastHit hit;

        //プレイヤーとrayが接触したか判断
        if (Physics.Raycast(ray, out hit))
        {
            //プレイヤーかタグで判断
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("hit");
                return true;
            }
        }
        return false;
    }
}
