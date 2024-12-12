using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Jack_HitCheck : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject hitcheck;//���̃I�u�W�F�N�g
    private GameObject jack;//�e�I�u�W�F�N�g
    private GameObject playerhitcheck;//�����蔻��Ǘ���
    private Collider coll;//�R���C�_�[
    private Vector3 firstpos;//�ŏ��̈ʒu
    private bool hitflag;//�����蔻��
    private int state;//jack��state�Q�Ɨp
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------


    private void Start()
    {
        //������
        {
            hitcheck = this.gameObject;
            jack = transform.parent.gameObject;
            playerhitcheck = GameObject.Find("PlayerHitCheck");
            coll = hitcheck.GetComponent<Collider>();
            hitflag = false;
            firstpos = this.transform.position;
        }
    }

    private void FixedUpdate()
    {
        //state���Q��
        state = jack.GetComponent<jack_controller>().GetState();

        //�R���C�_�[�̈ʒu��state���ƂɈړ�
        ColliderPosUpdate(state);

        //�ꉞfind�̖߂����瓖���蔻����������A�K�v��������
        if (state == 1)
        {
            hitflag = false;
        }

        //�x�m���o�Otodo
        //�����蔻����Ǘ��҂ɑ���
        //playerhitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitflag);
    }

    private void ColliderPosUpdate(int state)
    {
        switch(state)
        {
            case 0:
                break;
            case 1:
                //���������_�΍�
                hitcheck.transform.position = firstpos;
                break;
            case 2:
                //�O�ɐi�߂�
                hitcheck.transform.position += (hitcheck.transform.forward) * 0.01f;
                break;    
            case 3:
                //�����Ȃ�
                break;
            case 4:
                //���̈ʒu�ɂ��ǂ�
                float move = 3.0f;
                hitcheck.transform.position =
                    Vector3.MoveTowards(hitcheck.transform.position, firstpos, move * Time.deltaTime);
                break;
            default:
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //�͈͂ɐN��������true��Ԃ�
        if(other.tag == "Player")
        {
            Debug.Log("jack->Hit");
            hitflag = true;

            //�����蔻����Ǘ��҂ɑ���
            playerhitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitflag);
        }
    }
}
