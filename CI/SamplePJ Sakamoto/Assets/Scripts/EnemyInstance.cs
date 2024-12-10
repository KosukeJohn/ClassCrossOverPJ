using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject player;//�v���C���[
    private GameObject obj;//���̃I�u�W�F�N�g   
    private GameObject enemy;//��������prefab
    private bool destroyFlag;//1�񂾂��������邽�߂̃t���O
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    public GameObject prefab;//�����ɏ����������G���A�^�b�`����

    private void Start()
    {
        //������
        {
            player = GameObject.Find("Player");
            obj = this.gameObject;
            destroyFlag = true;
        }
    }

    private void Update()
    {
        if (FindPlayer())
        {
            if (destroyFlag)
            {
                //�����s�ɂ���
                destroyFlag = false;

                //���̃I�u�W�F�N�g�̍��W�ɏ���������
                enemy = Instantiate(prefab);
                enemy.transform.position = obj.transform.position;
            }
        }
        else
        {
            //�����\�ɂ���
            destroyFlag = true;

            //���������G��j��
            if (enemy != null) { Destroy(enemy); }
        }
    }

    private bool FindPlayer()
    {
        //�v���C���[���߂Â���true��Ԃ�
        if(obj.transform.position.x <= PlayerPosMaxX())
        {
            if(obj.transform.position.x >= PlayerPosMinX())
            {
                return true;
            }
        }
        return false;
    }
    private float PlayerPosMaxX()
    {
        //x���W�̍ő�l
        return player.transform.position.x + 11.0f;
    }
    private float PlayerPosMinX()
    {
        //x���W�̍ŏ��l
        return player.transform.position.x - 11.0f;
    }
}
