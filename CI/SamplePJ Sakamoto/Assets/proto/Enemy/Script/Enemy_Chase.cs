using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Chase : MonoBehaviour
{
    private int EnemyState;//���X�e�[�^�X
    public GameObject Enemy;//�Q�Ɨp�I�u�W�F�N�g
    private GameObject Player;//�v���C���[�I�u�W�F�N�g
    private float ChaseCnt = 0;//�ǂ������鎞�Ԃ��J�E���g
    private float CntMax = 3.0f;//�R�b��
    private bool PrePosFlag = true;//1�񂾂��o����̂ɕK�v
    [SerializeField]
    private Vector3 PrePos;//�A����W

    private void Start()
    {
        //�v���C���[�I�u�W�F�N�g���擾
        Player = GameObject.Find("Player");
    }
    private void FixedUpdate()
    {
        //�I�u�W�F�N�g�̃X�e�[�^�X���擾
        EnemyState = Enemy.GetComponent<Enemy_State>().GetState();

        //�ǐ�("Chase")�̎��̂ݍX�V����
        if (EnemyState == (int)Enemy_State.EnemyState.Chase)
        {
            Debug.Log("Chase");

            //�ǂ������Ă�Ƃ��͐Ԃ��Ȃ�
            Enemy.GetComponent<Enemy_Material>().ChangeValueRed();

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
                    PrePosFlag = false;
                    ChaseCnt = 0;

                    //�ҋ@
                    Debug.Log("Chase->Idel");
                    Enemy.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Idel);
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
        if(ChaseCnt >= CntMax)
        {
            return true;
        }
        
        //�ړ�
        {
            float move = 3.0f;
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
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
    private Vector3 Enemy_GetPosition()
    {
        //Enemy�̈ʒu���擾
        return this.Enemy.transform.position;
    }
    private Vector3 Player_GetPosition()
    {
        //�v���C���[��X,Z���W���擾
        Vector3 pos = 
            new Vector3(Player.transform.position.x, Enemy.transform.position.y, Player.transform.position.z);
        return pos;
    }
}
