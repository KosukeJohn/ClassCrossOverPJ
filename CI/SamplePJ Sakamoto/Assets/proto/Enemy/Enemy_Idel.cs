using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idel : MonoBehaviour
{
    private int         EnemyState;//���X�e�[�^�X
    public  GameObject  State;//�Q�Ɨp�I�u�W�F�N�g

    private void Update()
    {
        //�I�u�W�F�N�g�̃X�e�[�^�X���擾
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //�ҋ@("Idel")�̎��̂ݍX�V����
        if (EnemyState == (int)Enemy_State.EnemyState.Idel)
        {
            Debug.Log("Idel");

            //�X�e�[�^�X�̑J��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Idel->Attak");
                State.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Attak);
            }
        }
            
    }
}
