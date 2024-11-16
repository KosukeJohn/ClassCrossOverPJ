using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State : MonoBehaviour
{
    public int enemyState;//�X�e�[�^�X
    public GameObject State;//�Q�ƌ��I�u�W�F�N�g
    private bool DestroyFlag;
    //�X�e�[�^�X�ɖ��O������
    public enum EnemyState
    {
        Non, Idel, Patrol, Chase, Attak
    };
    private void Start()
    {
        //������
        enemyState = (int)EnemyState.Non;//�ŏ���0(Non)
        DestroyFlag = false;
    }
    private void Update()
    {
        //1�x����Non->Idel�ɂ���
        if (enemyState == (int)EnemyState.Non)
        {
            DestroyFlag = true;
            SetState(EnemyState.Idel);
        }

        //�Ă�Non�ɂȂ�����I�u�W�F�N�g��j��
        if (enemyState == (int)EnemyState.Non && DestroyFlag)
        {
            Destroy(this.State);
        }
    }
    public void SetState(EnemyState state)
    {
        //�X�e�[�^�X�ύX�֐�
        this.enemyState = (int)state;
    } 
    public int GetState()
    {
        //�X�e�[�^�X�Q�Ɗ֐�
        return this.enemyState;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            if(enemyState == (int)EnemyState.Non)
            {
                enemyState = (int)EnemyState.Non;
            }
        }
    }

}
