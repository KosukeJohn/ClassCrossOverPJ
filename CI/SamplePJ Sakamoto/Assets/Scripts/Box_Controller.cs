using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Controller : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject enemy;
    private GameObject box;
    private GameObject player;
    private Animator anim;
    private bool instanceFlag;
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    public GameObject prefab;//�����ɏ����������G���A�^�b�`����
    private void Start()
    {
        //������
        player = GameObject.Find("Player");
        box = this.gameObject;
        anim = box.GetComponent<Animator>();
        instanceFlag = true;
    }
    private void FixedUpdate()
    {
        if(PlayerFind())//�v���C���[���߂Â�����
        {
            //�A�j���[�V�������Đ�
            anim.SetBool("open", true);

            if(instanceFlag)
            {
                //�A�^�b�`�����G�𐶐�
                enemy = Instantiate(prefab);

                //���̒��S���n�ʂ̏�ɐ���
                Vector3 pos = box.transform.position;
                enemy.transform.position = new Vector3(pos.x, 0, pos.z);

                //1�񂾂����������邽�߂̃t���O
                instanceFlag = false;
            }
        }
    }

    private bool PlayerFind()
    {
        //�v���C���[������ʂ�߂����true��Ԃ�
        if(player.transform.position.x >= box.transform.position.x)
        {
            return true;
        }

        return false;
    }
}
