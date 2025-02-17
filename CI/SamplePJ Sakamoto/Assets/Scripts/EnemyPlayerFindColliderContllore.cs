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
        //範囲に入っているのがプレイヤーか
        if (other.gameObject.tag == "Player")
        {
            //視界の角度におさまっているか
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            transform.parent.GetComponent<EnemyCoppy>().SetPlayerFind(true);

            //playerの状態を取得
            bool isHide = false;
            if (other.GetComponent<PlayerController>())
            {
                isHide = other.GetComponent<PlayerController>().IsHiding;
            }             
            
            transform.parent.GetComponent<EnemyCoppy>().SetPlayerState(isHide);
            
            //target_angleがangleにおさまっているか
            if (target_angle < angle)
            {
                //直線距離にプレイヤーがあるかrayを飛ばす
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
                    
                    //rayがプレイヤー以外に当たったら
                    if (hit.collider != other)
                    {
                        Debug.Log("Toys Find");
                        //rayの距離を測って一定距離以下なら飛び越えさせる
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
            else//外に出た
            {
                Debug.Log("player not find");
                //回転する向きを決める
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
