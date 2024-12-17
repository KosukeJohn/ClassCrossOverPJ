using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class Notnappare_leader : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject leader;//�G�̃I�u�W�F�N�g
    private GameObject player;//�v���C���[�̃I�u�W�F�N�g
    private Vector3 firstpos;//�ŏ��̈ʒu
    private Vector3 prepos;//�߂�ʒu
    private Vector3 playerpos;//�����������̃v���C���[�̈ʒu
    private float lenge;//�ړ��̕�
    private float moveCnt;//�ړ��̎���
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    [SerializeField] private State state;//�X�e�[�^�X
    [SerializeField] private int nextseed;//�V�[�h�l
    //---------------------------------------------
    //�X�e�[�^�X�̒�`
    //---------------------------------------------
    private enum State
    {
        Non,Idle, Patrol, Find, Chase, Back 
    };

    private void Start()
    {
        //������
        {
            leader = this.gameObject;
            player = GameObject.Find("Player");
            firstpos = leader.transform.position;
            leader.transform.position = NextPosition(6);
            nextseed = 7;
            lenge = 3.0f;//�ړ��̕�
            moveCnt = 0;
            SetState(State.Idle);
        }
    }

    private void FixedUpdate()
    {
        if(state == State.Idle)
        {
            //�S���W�܂�����Patrol
            SetState(State.Patrol);
        }

        if (state == State.Patrol)
        {
            if(PatrolMove(NextPosition(nextseed)))
            {
                //�v���C���[����������Find�̂Ȃ�
                state = State.Find;
            }
        }

        if (state == State.Find)
        {
            //�ʒu���o����
            playerpos = PlayerGetPosition();
            prepos = leader.transform.position;

            //�ǐՂɈڍs
            SetState(State.Chase);
        }
        if (state == State.Chase)
        {
            if(ChaseMove(playerpos))
            { 
                //playerfind��false�ɂ���
                leader.GetComponentInChildren<Collider_cont_notnappare>().SetPlayerFind(false);

                //���̈ʒu�ɖ߂�
                SetState(State.Back);
            }
        }
        if(state == State.Back)
        {
            if (BackMove(prepos))
            {
                //���̈ʒu�ɖ߂�����ҋ@�Ɉڍs
                SetState(State.Idle);
            }
        }
    }
    private State GetState(){ return this.state; }
    private void SetState(State s) {  this.state = s; }
    private Vector3 PlayerGetPosition()
    {
        //�v���C���[�̈ʒu���擾
        return player.transform.position;
    }
    private bool PlayerFind()
    {
        //�q�I�u�W�F�N�g������G�͈͂Ƀv���C���[�����������擾
        bool playerfind = 
            leader.GetComponentInChildren<Collider_cont_notnappare>().GetPlayerFind();

        if(playerfind)//�v���C���[��������
        {
            return true;
        }

        return false;
    }
    private bool PatrolMove(Vector3 pos)
    {
        if(PlayerFind())//�v���C���[��������
        {
            return true;
        }

        //���̈ʒu�ɓG��������
        if (Mathf.Approximately(leader.transform.position.x,pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            //���������_�΍�
            leader.transform.position = pos;

            if(nextseed % 2 == 0)//�Ȃ���p���ǂ������f
            {
                if (EnemyTurn(nextseed))//��]������
                {
                    //�V�[�h�l�𑝂₷
                    nextseed++;
                    return false;
                }
            }
            else
            {
                //�V�[�h�l�𑝂₷
                nextseed++;
                return false;
            }
            
        }
        else
        {
            //���̈ʒu�܂ňړ�������
            float move = 3.0f;
            leader.transform.position =
                Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);

            //MoveJump();
        }
       
        return false;
    }
    private void MoveJump(/*Vector3 pos*/)
    {
        Vector3 vector = leader.transform.position;
        int jumpCnt = 0;
        float height = 3.0f;

        moveCnt += 1.0f * Time.deltaTime;

        if(moveCnt >= 1.0f)
        {
            moveCnt = 0;
            jumpCnt++;
        }

        //���ړ�
        {
            float posx = (lenge / 3 * jumpCnt) + lenge / 3 * moveCnt;
            vector.x += posx;
        }

        //�c�ړ�
        {
            float X = moveCnt - (lenge / 3 * jumpCnt + lenge / 6);
            vector.y = -(X * X) + height;
        }

        leader.transform.position = vector;
    }
    private bool EnemyTurn(int seed)
    {
        float anglespeed = 3.0f;//��]���x
        float turnMax = 0.6f;//��]����

        //�J�E���g������
        moveCnt += 1.0f * Time.deltaTime;

        if (moveCnt >= turnMax)//��]���ԂɂȂ�����
        {
            //�J�E���g��0�ɂ���
            moveCnt = 0;
            return true;
        }

        //��]������
        transform.Rotate(0, anglespeed, 0, Space.Self);

        return false;
    }
    private Vector3 NextPosition(int next)
    {
        Vector3 pos = firstpos;

        switch(next % 8)
        {
            case 0:
                pos.x -= lenge;
                pos.z += lenge;
                break;
            case 1:
                pos.z += lenge;
                break;
            case 2:
                pos.x += lenge;
                pos.z += lenge;
                break;
            case 3:
                pos.x += lenge;
                break;
            case 4:
                pos.x += lenge;
                pos.z -= lenge;
                break;
            case 5:
                pos.z -= lenge;
                break;
            case 6:
                pos.x -= lenge;
                pos.z -= lenge;
                break;
            case 7:
                pos.x -= lenge;
                break;
        }

        return pos;
    }
    private bool ChaseMove(Vector3 pos)
    {
        //�v���C���[�������ʒu�ɓG��������
        if (Mathf.Approximately(leader.transform.position.x, pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            //���������_�΍�
            leader.transform.position = pos;
            return true;
        }

        //�ړ�������
        float move = 10.0f;
        leader.transform.position =
            Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);

        return false;
    }
    private bool BackMove(Vector3 pos)
    {
        //���̈ʒu�ɖ߂�����
        if (Mathf.Approximately(leader.transform.position.x, pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            //���������_�΍�
            leader.transform.position = pos;
            return true;
        }

        //���̈ʒu�ɖ߂�
        float move = 3.0f;
        leader.transform.position =
            Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);

        return false;
    }
}