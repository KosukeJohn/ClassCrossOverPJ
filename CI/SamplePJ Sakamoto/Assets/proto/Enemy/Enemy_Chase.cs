using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase : MonoBehaviour
{
    private int EnemyState;//���X�e�[�^�X
    public GameObject State;//�Q�Ɨp�I�u�W�F�N�g

    private void Update()
    {
        //�I�u�W�F�N�g�̃X�e�[�^�X���擾
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //�ǐ�("Chase")�̎��̂ݍX�V����
        if (EnemyState == (int)Enemy_State.EnemyState.Chase)
        {
            Debug.Log("Chase");
        }   
    }
}
