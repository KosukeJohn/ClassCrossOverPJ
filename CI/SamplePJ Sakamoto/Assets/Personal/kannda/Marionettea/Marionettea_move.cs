using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Marionettea_move : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject player;//�v���C���[�I�u�W�F�N�g
    private GameObject enemy;//�G�̃I�u�W�F�N�g
    private Animator anim;//�A�j���[�V����
    private float ChaseCnt;//�ǂ������鎞��
    private float ChaseCntMax;//������߂�܂ł̎���
    private float DestroyCnt;//���ʎ���
    private float DestroyCntMax;//���ʂ܂ł̎���
    private float animCnt;//�A�j���[�V�����̎���
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    [SerializeField] private State state;//�X�e�[�^�X
    //---------------------------------------------
    //state�̒�`
    //---------------------------------------------
    private enum State {
        Non,Born, Chase, Idle
    };

    private void Start()
    {
        //������
        {
            player = GameObject.Find("Player");
            enemy = this.gameObject;
            ChaseCnt = 0;
            ChaseCntMax = 5.0f;
            DestroyCnt = 0;
            DestroyCntMax = 5.0f;
            state = State.Chase;
            anim = GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
     
        if (state == State.Chase)//�X�e�[�^�X���ǂ�������(Chase)�̎�
        {
            if (Enemy_MoveToPlayer(Player_GetPosition()))//�v���C���[�Ɍ������Ă���
            {
                //������߂���ҋ@��Ԃɂ���
                state = State.Idle;
                ChangeStateAnim(state);
            }
        }

        if (state == State.Idle)//�X�e�[�^�X���ҋ@(Idle)�̎�
        {
            if(Enemy_DestroyCnt())//�J�E���g����
            {
                //�j��
                Destroy(enemy);
            }
        }
    }
    private bool Enemy_MoveToPlayer(Vector3 pos)
    {
        //�v���C���[�Ɍ������Đi��

        //�^�C�}�[�𓮂���
        ChaseCnt += 1.0f * Time.deltaTime;

        //CntMax�ɂȂ�����ǂ�������̂���߂�
        if (ChaseCnt >= ChaseCntMax)
        {
            return true;
        }
        //�v���C���[����͈̔͂ɂ��邩���f
        bool find = enemy.GetComponentInChildren<Collider_Controller>().GetFindFlag();
        if (find)
        {
            //�ړ�
            float move = 3.0f;
            enemy.transform.position =
                Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);
        }
        else
        {
            //��]������
            enemy.transform.Rotate
                (0f, 3.0f * enemy.GetComponentInChildren<Collider_Controller>().GetDirection(), 0f);
        }
        return false;
    }
    private bool Enemy_DestroyCnt()
    {
        //�^�C�}�[�𓮂���
        DestroyCnt += 1.0f * Time.deltaTime;

        if(DestroyCnt > DestroyCntMax)//���ԂɂȂ���
        {
            return true;
        }
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
    //�A�j���[�V����������
    private bool AnimCnt(float cntMax)
    {
        //�J�E���g������
        animCnt += 1.0f * Time.deltaTime;

        //���������ƂɎw��̎��Ԃ����ƃJ�E���g�I��
        if (animCnt >= cntMax)
        {
            animCnt = 0;
            return true;
        }

        return false;
    }
    private void ChangeStateAnim(State state)
    {
        //���������ƂɃA�j���[�V������ύX
        this.state = state;
        int Anim = (int)this.state;
        anim.SetInteger("Marionnett_anim", Anim);
    }
}