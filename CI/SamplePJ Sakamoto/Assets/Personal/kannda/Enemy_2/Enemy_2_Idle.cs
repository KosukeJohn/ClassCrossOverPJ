using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy_2_Idle : MonoBehaviour
{
    private GameObject Player;//�v���C���[
    private GameObject Enemy;//Enemy
    private Vector3 Pos1;//�v���C���̈ʒu���o���邽��
    private Vector3 Pos2;//���̈ʒu���o����
    private float TimeCnt = 0;//�J�E���g
    private float FindDir = 300.0f;//PlayerFind�̎w��͈�
    private float EnemyHight = 10.0f;//�����͌Œ�

    private void Start()
    {
        //�v���C���[�I�u�W�F�N�g���擾
        Player = GameObject.Find("Player");
        //Light��true�ɂ���
        GetComponentInChildren<Light>().enabled = true;
        //Enemy���擾����
        this.Enemy = this.gameObject;
    }
    private void FixedUpdate()
    { 
        if (PlayerFind())
        {
            if(TimeCnt == 0)
            {
                //�ŏ��̈ʒu���o����
                Pos1 = Player_GetPosition();
            }
            if(TimeCnt >= 1.0)
            {
                //�P�b�Ԃɐi�񂾋������o����
                Pos2 = Player_GetPosition();
                this.Enemy.transform.position = NextPosition(Pos1, Pos2); 
            }
            if(TimeCnt >= 2.0)
            {
                //AddComponent��Chase���Ăяo���ĕs�v�Ȃ��̃X�N���v�g��j��
                GetComponentInChildren<Light>().enabled = false;
                Enemy.AddComponent<Enemy_2_Chase>();
                Destroy(Enemy.GetComponent<Enemy_2_Idle>());
                return;
            }
            //���Ԃ𑪂�
            TimeCnt += 1.0f * Time.deltaTime;

        }
    }
    private Vector3 Player_GetPosition()
    {
        //�v���C���[�̈ʒu���擾
        return this.Player.transform.position;
    }
    private bool PlayerFind()
    {
        //X,Z���W�Ŕ��f����
        Vector2 playerdir;
        playerdir.x = Player.transform.position.x;
        playerdir.y = Player.transform.position.z;
        Vector2 enemydir;
        enemydir.x = Enemy.transform.position.x;
        enemydir.y = Enemy.transform.position.z;
        Vector2 dir = playerdir - enemydir;
        float d = dir.magnitude;

        //�w�肳�ꂽ�͈͂Ƀv���C���[�����݂��邩
        if (d < FindDir)
        {
            return true;
        }
        return false;
    }
    private Vector3 NextPosition(Vector3 pos1,Vector3 pos2)
    {
        //�i�񂾋��������߂�
        Vector3 pos = pos2 - pos1;
        //���̃v���C���[�̈ʒu�Ɏ��ɐi�݂����ȋ����𑫂�
        pos += Player_GetPosition();
        //���������ǂ�
        pos.y = EnemyHight;
        return pos;
    }
}
