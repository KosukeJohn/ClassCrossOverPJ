using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy_2_Chase : MonoBehaviour
{
    private GameObject Enemy;//Enemy
    private GameObject Player;//�v���C���[
    private Rigidbody rig;//Rigidbody
    private Vector3 PrePos;//�߂�ʒu
    private bool Flag;//�����Ə㏸�̃t���O
    private float PreY = 10000.0f;//�Ƃ肠�����̏����l

    private void Start()
    {
        //Enemy���擾
        this.Enemy = this.gameObject;
        //�߂�ʒu���擾
        PrePos = this.Enemy.transform.position;
        //�v���C���[���擾
        this.Player= GameObject.Find("Player");
        //Rigidbody��ǉ�
        this.rig = Enemy.AddComponent<Rigidbody>();
        //Rotation��S�ăI��
        this.rig.constraints = RigidbodyConstraints.FreezeRotation;  
        Flag = false;
    }
    private void FixedUpdate()
    {
        if (!Flag)
        {
            if (EnemyMove())
            {
                //�t���O��true�ɂ��ď㏸��
                Flag = true;
            }
        }
        if(Flag)
        {
            if(MoveToPrePos(PrePos))
            {
                //AddComponent��Idle���Ăяo���ĕs�v�Ȃ��̃X�N���v�g��j��
                Enemy.AddComponent<Enemy_2_Idle>();
                Destroy(Enemy.GetComponent<Enemy_2_Chase>());
                return;
            }
        }
    }
    private bool EnemyMove()
    {
        //1�t���[���O�̂x�̒l�����Ɠ����Ȃ�true 
        if(Mathf.Approximately
            (this.Enemy.transform.position.y, PreY))
        {
            Destroy(Enemy.GetComponent<Rigidbody>());
            return true;
        }

        //Y�̒l���擾
        PreY = this.Enemy.transform.position.y;
        return false;
    }
    private bool MoveToPrePos(Vector3 pos)
    {
        //���̈ʒu�ɖ߂�����true
        if (Mathf.Approximately
            (this.Enemy.transform.position.y, pos.y))
        {
            return true;
        }

        //���̈ʒu�Ɍ�����
        float move = 5.0f;
        Enemy.transform.position =
            Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        return false;
    }
}
