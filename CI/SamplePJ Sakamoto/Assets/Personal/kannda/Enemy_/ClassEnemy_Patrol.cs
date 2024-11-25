using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassEnemy_Patrol : ClassEnemy_State
{
    //public EnemyMove move;
    [SerializeField]
    public int NextPoint = 0;//���̈ړ���seed�l
    protected override void EnemyUpdate()
    {
        //base.EnemyUpdate();
        //�T��("Patrol")�̎��̂ݍX�V����
        if (enemyState == (int)EnemyState.Patrol)
        {
            Debug.Log("Patrol");

            //�w��n�_�Ɉړ�������ҋ@(Idel)�ɑJ��
            if (Enemy_Move(SetNextPosition(NextPoint % 8)))
            {
                //���̈ړ��n�_�̍X�V
                NextPoint++;

                //�ҋ@
                Debug.Log("Patrol->Idel");
                SetState(EnemyState.Idel);
                return;
            }
        }
    }
    private Vector3 SetNextPosition(int point)
    {
        //seed�l�����ƂɈړ�����w��
        //�K���Ɍ��߂܂���
        float Length = 3;
        float posY = Enemy.transform.position.y;

        Vector3 pos = new(Length, posY, Length);
        switch (point)
        {
            case 0:
                pos = new Vector3(1 * Length, posY, 1 * Length);
                break;
            case 1:
                pos = new Vector3(0 * Length, posY, 1 * Length);
                break;
            case 2:
                pos = new Vector3(-1 * Length, posY, 1 * Length);
                break;
            case 3:
                pos = new Vector3(-1 * Length, posY, 0 * Length);
                break;
            case 4:
                pos = new Vector3(-1 * Length, posY, -1 * Length);
                break;
            case 5:
                pos = new Vector3(0 * Length, posY, -1 * Length);
                break;
            case 6:
                pos = new Vector3(1 * Length, posY, -1 * Length);
                break;
            case 7:
                pos = new Vector3(1 * Length, posY, 0 * Length);
                break;
            default:
                break;
        }
        return pos;
    }
    private bool Enemy_Move(Vector3 pos)
    {
        //Enemy���w�肳�ꂽ�n�_�Ɉړ����������ׂĈړ����Ă��Ȃ���Έړ�
        //�w��n�_�ɂ��������f
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
            Enemy.transform.position =
                Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        }

        return false;
    }

    //private void Start()
    //{
    //    move = new EnemyMoveIdle();
    //}
}
