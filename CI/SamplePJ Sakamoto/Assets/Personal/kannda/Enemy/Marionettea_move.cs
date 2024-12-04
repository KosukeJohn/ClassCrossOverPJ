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
    private bool Enemy_MoveToPrePos(Vector3 pos)
    {
        if (Enemy_GetPosition().x >= pos.x - 0.01 && Enemy_GetPosition().x <= pos.x + 0.01)
            if (Enemy_GetPosition().z >= pos.z - 0.01 && Enemy_GetPosition().z <= pos.z + 0.01)
            {
                //���������_���΍�
                enemy.transform.position = pos;

                //������true
                Debug.Log("Pos = true");
                return true;
            }

        //�ړ�
        {
            float move = 3.0f;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);
        }

        return false;
    }
    private Vector3 Enemy_GetPosition()
    {
        //Enemy�̈ʒu���擾
        return this.enemy.transform.position;
    }
    private Vector3 Player_GetPosition()
    {
        //�v���C���[��X,Z���W���擾
        Vector3 pos =
            new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z);
        return pos;
    }
    private bool Player_FindRay()
    {
        //Ray�̐���
        Ray ray = new Ray(Enemy_GetPosition(), Player_GetPosition() - Enemy_GetPosition());
        Debug.DrawRay(ray.origin, ray.direction * 900, Color.blue, 5.0f);
        RaycastHit hit;

        //�v���C���[��ray���ڐG���������f
        if (Physics.Raycast(ray, out hit))
        {
            //�v���C���[���^�O�Ŕ��f
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("hit");
                return true;
            }
        }
        return false;
    }
}
