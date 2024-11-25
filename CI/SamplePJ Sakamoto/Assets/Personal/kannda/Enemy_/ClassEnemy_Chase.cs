using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Enemy_State;

public class ClassEnemy_Chase : ClassEnemy_State
{
    private float ChaseCnt = 0;//�ǂ������鎞�Ԃ��J�E���g
    private float CntMax = 3.0f;//�R�b��
    private bool PrePosFlag = true;//1�񂾂��o����̂ɕK�v
    [SerializeField]
    private Vector3 PrePos;//�A����W
    protected override void EnemyUpdate()
    {
       // base.EnemyUpdate();
        //�ǐ�("Chase")�̎��̂ݍX�V����
        if (enemyState == (int)EnemyState.Chase)
        {
            Debug.Log("Chase");

            //�ǂ������Ă�Ƃ��͐Ԃ��Ȃ�
            //Enemy.GetComponent<Enemy_Material>().ChangeValueRed();

            //�߂邽�߂̍��W���擾
            if (PrePosFlag)
            {
                PrePos = Enemy_GetPosition();
                PrePosFlag = false;
            }

            //�ǂ�������
            if (Enemy_MoveToPlayer(Player_GetPosition()))
            {
                Debug.Log("return");

                //
                if (Enemy_MoveToPrePos(PrePos))
                {
                    //������
                    PrePosFlag = true;
                    ChaseCnt = 0;

                    //�ҋ@
                    Debug.Log("Chase->Idel");
                    SetState(EnemyState.Idel);
                    return;
                }
            }

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
        if (Player_FindRay())
        {
            float move = 3.0f;
            Enemy.transform.position =
                Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        }
        else
        {
            return true;
        }
        return false;
    }
    private bool Enemy_MoveToPrePos(Vector3 pos)
    {
        if (Enemy_GetPosition().x >= pos.x - 0.01 && Enemy_GetPosition().x <= pos.x + 0.01)
            if (Enemy_GetPosition().z >= pos.z - 0.01 && Enemy_GetPosition().z <= pos.z + 0.01)
            {
                //���������_���΍�
                Enemy.transform.position = pos;

                //������true
                Debug.Log("Pos = true");
                return true;
            }

        //�ړ�
        {
            float move = 3.0f;
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        }

        return false;
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
