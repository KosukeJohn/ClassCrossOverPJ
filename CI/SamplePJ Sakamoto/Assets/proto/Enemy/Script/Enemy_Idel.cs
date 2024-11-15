using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idel : MonoBehaviour
{
    private int         EnemyState;//���X�e�[�^�X
    public  GameObject  State;//�Q�Ɨp�I�u�W�F�N�g
    private GameObject Player;//�v���C���[�I�u�W�F�N�g
    public float FindDir;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        //�I�u�W�F�N�g�̃X�e�[�^�X���擾
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //�ҋ@("Idel")�̎��̂ݍX�V����
        if (EnemyState == (int)Enemy_State.EnemyState.Idel)
        {
            Debug.Log("Idel");

            //�X�e�[�^�X�̑J��
            if (Player_Find(State.transform.position.x, State.transform.position.z))
            {
                Debug.Log("Idel->Chase");
                State.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Chase);
            }
            else
            {
                Debug.Log("Idel->Patrol");
                State.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Patrol);
            }
        }
            
    }
    private bool Player_Find(float enemyX,float enemyZ)
    {
        Vector3 playerpos = Player.transform.position;
        float playerX = playerpos.x;
        float playerZ = playerpos.z;
        float dir = (playerX + enemyX) * (playerX + enemyX) +
            (playerZ + enemyZ) * (playerZ + enemyZ);

        if (Mathf.Sqrt(dir) < FindDir)
        {
            return true;
        }

        return false;
    }
}
