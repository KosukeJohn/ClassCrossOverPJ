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
            //��]������
            transform.Rotate(0f,3.0f * enemy.GetComponentInChildren<Collider_Controller>().num,0f);
        }
        
    }
    private bool Enemy_MoveToPlayer(Vector3 pos)
    {
        //�v���C���[�Ɍ������Đi��

        ChaseCnt += 1.0f * Time.deltaTime;

        //CntMax�ɂȂ�����ǂ�������̂���߂�
        if (ChaseCnt >= CntMax)
        {
            return true;
        }

        //�ړ�
        float move = 3.0f;
        enemy.transform.position =
            Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);

        return false;
    }
    private Vector3 Enemy_GetPosition()
    {
        //Enemy�̈ʒu���擾
        Vector3 pos = 
            new Vector3(this.enemy.transform.position.x, this.enemy.transform.position.y + 0.5f, this.enemy.transform.position.z);
        return pos;
    }
    private Vector3 Player_GetPosition()
    {
        //�v���C���[��X,Z���W���擾
        Vector3 pos =
            new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z);
        return pos;
    }

}
