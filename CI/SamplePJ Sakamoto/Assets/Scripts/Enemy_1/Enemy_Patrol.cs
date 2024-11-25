using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Patrol : MonoBehaviour
{
    private int EnemyState;//���X�e�[�^�X
    public GameObject Enemy;//�Q�Ɨp�I�u�W�F�N�g
    [SerializeField]
    public int NextPoint = 0;//���̈ړ���seed�l
    private Vector3 PrePos;
    private void Start()
    {
        PrePos = Enemy_GetPosition();
        //�ŏ��̒n�_�Ɉړ�
        Enemy.transform.position = SetNextPosition(NextPoint);
    }
    private void FixedUpdate()
    {
        //�I�u�W�F�N�g�̃X�e�[�^�X���擾
        EnemyState = Enemy.GetComponent<Enemy_State>().GetState();

        //�T��("Patrol")�̎��̂ݍX�V����
        if (EnemyState == (int)Enemy_State.EnemyState.Patrol)
        {
            Debug.Log("Patrol");

            //�w��n�_�Ɉړ�������ҋ@(Idel)�ɑJ��
            if (Enemy_Move(SetNextPosition(NextPoint % 8)))
            {
                //���̈ړ��n�_�̍X�V
                NextPoint++;

                //�ҋ@
                Debug.Log("Patrol->Idel");
                Enemy.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Idle);
                return;
            }
        } 
    }
    private Vector3 SetNextPosition(int point)
    {
        //seed�l�����ƂɈړ�����w��
        //�K���Ɍ��߂܂���
        float Length = 2.0f;
        float posY = Enemy.transform.position.y;

        Vector3 pos = new(Length, posY, Length);
        switch (point)
        {
            case 0:
                pos = new Vector3(1 * Length + PrePos.x, posY, 1 * Length + PrePos.z);
                break;
            case 1:
                pos = new Vector3(0 * Length + PrePos.x, posY, 1 * Length + PrePos.z);
                break;
            case 2:
                pos = new Vector3(-1 * Length + PrePos.x, posY, 1 * Length + PrePos.z);
                break;
            case 3:
                pos = new Vector3(-1 * Length + PrePos.x, posY, 0 * Length + PrePos.z);
                break;
            case 4:
                pos = new Vector3(-1 * Length + PrePos.x, posY, -1 * Length + PrePos.z);
                break;
            case 5:
                pos = new Vector3(0 * Length + PrePos.x, posY, -1 * Length + PrePos.z);
                break;
            case 6:
                pos = new Vector3(1 * Length + PrePos.x, posY, -1 * Length + PrePos.z);
                break;
            case 7:
                pos = new Vector3(1 * Length + PrePos.x, posY, 0 * Length + PrePos.z);
                break;
            default:
                break;
        }
        return pos;
    }
    private Vector3 Enemy_GetPosition()
    {
        //Enemy�̈ʒu���擾
        return this.Enemy.transform.position;
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
}
