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
    //�X�e�[�^�X�ύX�֐�
    public void SetState(EnemyState state)
    {
        this.enemyState = (int)state;
    }
    //�X�e�[�^�X�Q�Ɗ֐�
    public int GetState()
    {
        return this.enemyState;
    }

}
