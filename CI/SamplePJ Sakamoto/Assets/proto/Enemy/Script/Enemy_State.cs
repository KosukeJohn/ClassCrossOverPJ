using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State : MonoBehaviour
{
    public int enemyState;//�X�e�[�^�X
    public GameObject Enemy;//�Q�ƌ��I�u�W�F�N�g
    private GameObject Player;
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

        //�v���C���[�I�u�W�F�N�g���擾
        Player = GameObject.Find("Player");

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
            Destroy(this.Enemy);
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
    //�߂܂��鉼����
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            if(enemyState != (int)EnemyState.Non)
            {
                //enemyState = (int)EnemyState.Non;
                Player.GetComponent<Player_cont>().enabled = false;
                //
                Player.GetComponent<Material_Change>().ChangeValue();
            }
        }
    }

}
