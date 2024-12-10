using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private bool hit;//�e���ʂ���hit����������
    private bool hitFlag;//hit������true��Ԃ��A�������݂̂��Q�Ƃ���I�I
    private GameObject player;//�v���C���[
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    private bool playerhide;

    private void Start()
    {
        //������
        {
            hit = false;
            hitFlag = false;
            this.player = GameObject.Find("Player");
        }
    }

    private void Update()
    {
        if (hit)
        {
            //�܂�������������
            //player���B��Ă��邩�Q�Ƃ���
            playerhide = player.GetComponent<PlayerController>().IsHiding;

            //�B��Ă��Ȃ���Γ����������Ƃɂ���
            if (!playerhide)
            {
                hitFlag = true;
            }
        }

        if (hitFlag)
        {
            //���������������\�ɂȂ�
            Debug.Log("PlayerHit");
        }
    }

    //---------------------------------------------
    //�Q�Ɖ\�֐�
    //---------------------------------------------
    public void SetPlayerHitCheck(bool flag)
    {
        //hit���������f���鎞�Ɏg��
        this.hit = flag;
    }
    public bool GetPlayerHitCheck()
    {
        //�Q�Ɨp
        return this.hitFlag;
    }
}
