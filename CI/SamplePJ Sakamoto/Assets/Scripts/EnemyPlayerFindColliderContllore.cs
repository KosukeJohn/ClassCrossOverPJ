using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPlayerFindColliderContllore : MonoBehaviour
{
    private Collider coll;
    private bool playerfind;
    private int direction;

    [SerializeField] private float angle;

    private void Start()
    {
        coll = GetComponent<Collider>();
        angle = 10.0f;
    }
    private void OnTriggerStay(Collider other)
    {
        //�͈͂ɓ����Ă���̂��v���C���[��
        if (other.gameObject.tag == "Player")
        {
            //���E�̊p�x�ɂ����܂��Ă��邩
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            transform.parent.GetComponent<EnemyCoppy>().SetPlayerFind(true);

            //player�̏�Ԃ��擾
            bool isHide = false;
            if (other.GetComponent<PlayerController>())
            {
                isHide = other.GetComponent<PlayerController>().IsHiding;
            }             
            
            transform.parent.GetComponent<EnemyCoppy>().SetPlayerState(isHide);
            
            //target_angle��angle�ɂ����܂��Ă��邩
            if (target_angle < angle)
            {
                //���������Ƀv���C���[�����邩ray���΂�
                Ray ray = new(this.transform.position, posDelta);
                Debug.DrawRay(ray.origin, ray.direction , Color.red, Time.deltaTime, false);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider == other)
                    {
                        Debug.Log("player find");
                        Vector3 playerPos = other.transform.position;
                        transform.parent.GetComponent<EnemyCoppy>().SetGoPlayer(true);
                        transform.parent.GetComponent<EnemyCoppy>().SetPlayerPosition(playerPos);                       
                    }
                    
                    //ray���v���C���[�ȊO�ɓ���������
                    if (hit.collider != other)
                    {
                        Debug.Log("Toys Find");
                        //ray�̋����𑪂��Ĉ�苗���ȉ��Ȃ��щz��������
                        Vector3 enemypos = this.transform.position;
                        Vector3 toypos = hit.point;
                        Vector3 lenge = toypos - enemypos;

                        transform.parent.GetComponent<EnemyCoppy>().SetGoPlayer(true);
                        transform.parent.GetComponent<EnemyCoppy>().SetPlayerPosition(hit.point);

                        float prelenge = 0.5f;
                        if (Mathf.Abs(lenge.x) <= prelenge){
                            if (Mathf.Abs(lenge.z) <= prelenge) {
                                transform.parent.GetComponent<EnemyCoppy>().SetGoPlayer(false);
                                transform.parent.GetComponent<EnemyCoppy>().SetIsJump(true);
                            }
                        }
                    }
                }
            }
            else//�O�ɏo��
            {
                Debug.Log("player not find");
                //��]������������߂�
                float right_angle = Vector3.Angle(this.transform.right, posDelta);
                if (right_angle < 90f)
                {
                    transform.parent.GetComponent<EnemyCoppy>().SetMoveDirection(1);
                }
                else
                {
                    transform.parent.GetComponent<EnemyCoppy>().SetMoveDirection(-1);
                }
                transform.parent.GetComponent<EnemyCoppy>().SetGoPlayer(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<EnemyCoppy>().SetPlayerFind(false);
        }
    }
}
