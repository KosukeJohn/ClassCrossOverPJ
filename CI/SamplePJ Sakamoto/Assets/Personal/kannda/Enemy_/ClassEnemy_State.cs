using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
<<<<<<< HEAD
=======
//using UnityEditor.MemoryProfiler;
>>>>>>> ecf1f8eaa9c08250d9d43bd5e233d0e86f9cea83
using UnityEngine;

//[System.Serializable]
//public class EnemyMove
//{
//    public string MoveName;
//    public float timer;
//    public int step;
//    public Player target;

//    public void SetTarget(Player t)
//    {
//        target = t;
//    }

//    public void NextStep()
//    {
//        step++;
//        timer = 0;
//    }

//    public void InitStep()
//    {
//        step = 0;
//        timer = 0;
//    }

//    public virtual void Move(Enemy enemy)
//    {
//        Debug.Log("���̃N���X�̓x�[�X�N���X�Ȃ̂Œ��Ŏg��Ȃ��ł�������");
//    }
//}

//public class EnemyMoveIdle : EnemyMove
//{
//    public EnemyMoveIdle()
//    {
//        MoveName = "Idle";
//    }
    
//    public override void Move(Enemy enemy)
//    {
//        // IDLE�Ȃ�̏�����������
//    }
//}

public class ClassEnemy_State : MonoBehaviour
{
    public GameObject Enemy;//�Q�ƌ��I�u�W�F�N�g
    protected int enemyState;//�X�e�[�^�X
    protected GameObject Player;
    private bool DestroyFlag;
    protected virtual void EnemyUpdate()
    {
        Debug.Log("�I�[�o�[���C�h��");
    }

    //�X�e�[�^�X�ɖ��O������
    protected enum EnemyState
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
    private void FixedUpdate()
    {
        //1�x����Non->Idel�ɂ���
        if (enemyState == (int)EnemyState.Non)
        {
            DestroyFlag = true;
            SetState(EnemyState.Idel);
        }

        //�X�e�[�^�X��Non�łȂ���΃I�[�o�[���C�h
        if(enemyState!=(int)EnemyState.Non)
        {
            EnemyUpdate();
        }

        //�Ă�Non�ɂȂ�����I�u�W�F�N�g��j��
        if (enemyState == (int)EnemyState.Non && DestroyFlag)
        {
            Destroy(this.Enemy);
        }
    }
    protected void SetState(EnemyState state)
    {
        //�X�e�[�^�X�ύX�֐�
        this.enemyState = (int)state;
    }
    protected int GetState()
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
            if (enemyState != (int)EnemyState.Non)
            {
                //enemyState = (int)EnemyState.Non;
                //Player.GetComponent<Player_cont>().enabled = false;
                //
               // Player.GetComponent<Material_Change>().ChangeValue();
            }
        }
    }
    protected Vector3 Enemy_GetPosition()
    {
        //Enemy�̈ʒu���擾
        return this.Enemy.transform.position;
    }
    protected Vector3 Player_GetPosition()
    {
        //�v���C���[��X,Z���W���擾
        Vector3 pos =
            new Vector3(Player.transform.position.x, Enemy.transform.position.y, Player.transform.position.z);
        return pos;
    }
}