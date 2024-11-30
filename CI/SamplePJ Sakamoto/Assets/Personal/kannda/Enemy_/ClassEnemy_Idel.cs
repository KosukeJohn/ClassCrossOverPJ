using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enenmy_Idel : ClassEnemy_State
{
    public float FindDir;//�v���C���[�T���͈�
    protected override void EnemyUpdate()
    {
        //base.EnemyUpdate();
        if (enemyState == (int)EnemyState.Idel)
        {
            Debug.Log("Idel");
            //Enemy.GetComponent<Enemy_Material>().ChangeValueWhite();

            //�T���͈͓��Ƀv���C���[�͑��݂��邩
            if (Player_Find(Enemy.transform.position.x, Enemy.transform.position.z))
            {
                Debug.Log("find");

                //�v���C���[�Ǝ����̊Ԃɏ�Q���͂Ȃ���
                if (Player_FindRay())
                {
                    //�v���C���[��ǐ�
                    Debug.Log("Idel->Chase");
                    SetState(EnemyState.Chase);
                    return;
                }
            }

            //����
            Debug.Log("Idel->Patrol");
            SetState(EnemyState.Patrol);
            return;
        }
    }
    private bool Player_Find(float enemyX, float enemyZ)
    {
        //X,Z���W�Ŕ��f����
        Vector2 playerdir;
        playerdir.x = Player.transform.position.x;
        playerdir.y = Player.transform.position.z;
        Vector2 enemydir;
        enemydir.x = enemyX;
        enemydir.y = enemyZ;
        Vector2 dir = playerdir - enemydir;
        float d = dir.magnitude;

        //�w�肳�ꂽ�͈͂Ƀv���C���[�����݂��邩
        if (d < FindDir)
        {
            return true;
        }
        return false;
    }
    private bool Player_FindRay()
    {
        //Ray�̐���
        Ray ray = new Ray(Enemy.transform.position, Player_coll_GetPosition() - Enemy.transform.position);
        Debug.DrawRay(ray.origin, ray.direction * 900, Color.red, 5.0f);
        RaycastHit hit;

        //�v���C���[��ray���ڐG���������f
        if (Physics.Raycast(ray, out hit))
        {
            //�v���C���[���^�O�Ŕ��f
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("hit");
                return true;
            }
        }
        return false;
    }
    private Vector3 Player_coll_GetPosition()
    {
        //�v���C���[�̃R���C�_�[�̈ʒu���擾
        return Player.GetComponent<Collider>().transform.localPosition; ;
    }
}
