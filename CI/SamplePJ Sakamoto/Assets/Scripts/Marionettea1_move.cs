using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Marionettea1_move : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject player;//�v���C���[�I�u�W�F�N�g
    private GameObject enemy;//�G�̃I�u�W�F�N�g
    private Light redLight;
    private Animator anim;//�A�j���[�V����
    private float BornCnt;//�����܂ł̎���
    private float ChaseCnt;//�ǂ������鎞��
    private float animCnt;//�A�j���[�V�����̎���
    //private bool destroyFlag;//�j��t���O
    private float StageEnd_X1 = 63.66f;//�X�e�[�W�P�̏I���
    
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    [SerializeField] private State state;//�X�e�[�^�X
    [SerializeField] private Vector3 pos;//�}���I�l�b�g�̈ʒu
    [SerializeField] AudioSource source;//�I�[�f�B�I�\�[�X
    [SerializeField] AudioClip attack;
    //---------------------------------------------
    //�o�����X����
    //---------------------------------------------
    private float ChaseCntMax = 5.0f;//������߂�܂ł̎���
    private float ChaseSpeed = 6.0f;//�ǂ�������X�s�[�h
    //---------------------------------------------
    //state�̒�`
    //---------------------------------------------
    private enum State {
        Non, Born, Chase, Idle, Attack ,Death
    };

    private void Start()
    {
        //������
        {
            player = GameObject.Find("Player");
            enemy = this.gameObject;
            BornCnt = 1.5f;
            ChaseCnt = 0;
            state = State.Born;
            animCnt = 0;
            anim = GetComponent<Animator>();
            //destroyFlag = false;
            redLight = transform.GetChild(0).GetComponent<Light>();
            redLight.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        ////�X�e�[�W�P�̏I���n�_�𒴂�����
        //if(enemy.transform.position.x >= StageEnd_X1)
        //{
        //    //�}���I�l�b�g��j�󂷂�
        //    state = State.Death;
        //}

        //���܂ꂽ���̏���
        if(state == State.Born)
        {
           
            //�J�E���g������
            if(AnimCnt(BornCnt))
            {
                pos = transform.position;//�A��ʒu���`
                ChangeState(State.Chase);//�X�e�[�^�X�̕ύX
                ChangeAnim(State.Chase);//�X�e�[�^�X�̕ύX
                redLight.enabled = true;
            }
        }

        //�U�����̏���
        if (state == State.Attack)
        {
           
            //�������A�j���[�V�����ɂ���
            ChangeAnim(State.Born);
            
            if (AnimCnt(BornCnt))
            {
                ////�A�j���[�V�������I�������ǐՂɖ߂�
                //ChangeState(State.Chase);//�X�e�[�^�X�̕ύX
                //ChangeAnim(State.Chase);//�X�e�[�^�X�̕ύX

                ChangeState(State.Death);//�X�e�[�^�X�̕ύX
                ChangeAnim(State.Chase);//�X�e�[�^�X�̕ύX
            }
        }

        //�ǐՎ��̏���
        if (state == State.Chase)//�X�e�[�^�X���ǂ�������(Chase)�̎�
        {
            if (Enemy_MoveToPlayer(Player_GetPosition()))//�v���C���[�Ɍ������Ă���
            {
                //������߂���ҋ@��Ԃɂ���
                ChangeState(State.Attack);
                ChangeAnim(State.Idle);
            }
        }

        //���S���̏���
        if (state == State.Death)
        {
            //�ړ�
            float move = 10.0f;
            enemy.transform.position =
                Vector3.MoveTowards(enemy.transform.position, pos, move * Time.deltaTime);

            //��]������
            enemy.transform.Rotate(0f, 30.0f , 0f);

            //�v���C���[�Ɨ��ꂽ��j��
            if (player.transform.position.x - enemy.transform.position.x >= 20.0f)
            {
                //�j��
                Destroy(enemy);
            }
        }
    }
    private bool Enemy_MoveToPlayer(Vector3 pos)
    {
        //�v���C���[�Ɍ������Đi��

        //�X�e�[�W�P�̏I���n�_�𒴂�����
        if (enemy.transform.position.x >= StageEnd_X1)
        {
            source.clip = attack;
            source.Play();
            //�}���I�l�b�g��j�󂷂�
            return true;
        }

        ////�^�C�}�[�𓮂���
        //ChaseCnt += 1.0f * Time.deltaTime;

        ////CntMax�ɂȂ�����ǂ�������̂���߂�
        //if (ChaseCnt >= ChaseCntMax)
        //{
        //    ChaseCnt = 0;
        //    return true;
        //}

        //�v���C���[�����G�͈͂ɂ��邩���f
        bool find = enemy.GetComponentInChildren<Collider_Controller>().GetFindFlag();
        if (find)
        {
            //�ړ�
            enemy.transform.position =
                Vector3.MoveTowards(enemy.transform.position, pos, ChaseSpeed * Time.deltaTime);
        }
        else
        {
            //��]������
            enemy.transform.Rotate
                (0f, 3.0f * enemy.GetComponentInChildren<Collider_Controller>().GetDirection(), 0f);
        }

        return false;
    }
    private Vector3 Player_GetPosition()
    {
        //�v���C���[��X,Z���W���擾
        Vector3 pos =
            new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z);
        return pos;
    }
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
    private void ChangeAnim(State state_)
    {
        if (state_ == State.Non) { return; }
        if (state_ == State.Death) { return; }
        if (state_ == State.Attack) { return; }

        //���������ƂɃA�j���[�V������ύX
        int Anim = (int)state_;
        anim.SetInteger("Marionnett_anim", Anim);
    }
    private void ChangeState(State state_)
    {
        //Non�͎󂯎��Ȃ�
        if (state_ == State.Non) { return; }

        //���������ƂɃX�e�[�^�X��ύX
        this.state = state_;

    }
    public bool GetAttackFlag()
    {
        bool attackFlag = false;

        if(state == State.Attack)
        {
            attackFlag = true;
        }

        return attackFlag;
    }
}