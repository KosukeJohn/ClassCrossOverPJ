using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private bool hit;//�e���ʂ���hit����������
    private GameObject player;//�v���C���[
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    [SerializeField] private bool hitFlag;//hit������true��Ԃ��A�������݂̂��Q�Ƃ���I�I

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
            //���������������\�ɂȂ�
            Debug.Log("PlayerHit");

            hitFlag = hit;

            hit = false;
        }
    }

    //---------------------------------------------
    //�Q�Ɖ\�֐�
    //---------------------------------------------
    public void SetPlayerHitCheck(bool flag)
    {
        if (flag == false) { return; }

        //hit���������f���鎞�Ɏg��
        this.hit = flag;
    }
    public bool GetPlayerHitCheck()
    {
        //�Q�Ɨp
        return this.hitFlag;
    }
}
