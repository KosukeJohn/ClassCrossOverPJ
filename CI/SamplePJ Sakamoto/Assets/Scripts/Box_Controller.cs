using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Controller : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject enemy;
    private GameObject spotLight;
    private GameObject box;
    private GameObject player;
    private Animator anim;
    private Light redlight;
    private bool instanceFlag;
    private float timeCnt;
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
        spotLight = transform.GetChild(0).gameObject;
        redlight = spotLight.GetComponent<Light>();
        redlight.enabled = false;
        timeCnt = 0;
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

                redlight.enabled = true;
            }

            if (redlight.enabled) {

                timeCnt += Time.deltaTime;

                if(timeCnt >= 1.0f)
                {
                    timeCnt = 0;
                    redlight.enabled = false;
                }
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
