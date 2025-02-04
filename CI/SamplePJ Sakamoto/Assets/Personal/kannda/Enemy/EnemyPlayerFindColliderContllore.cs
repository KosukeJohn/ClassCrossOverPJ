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
            bool isHide = other.GetComponent<PlayerHide>().GetPlayerisHide();

            transform.parent.GetComponent<EnemyCoppy>().SetPlayerState(isHide);
            if (isHide) 
            {
                if (transform.parent.GetComponent<Enemy3>())
                {
                    transform.parent.GetComponent<Enemy3>().SetLenge(3);
                    transform.parent.GetComponent<Enemy3>().SetPos(other.GetComponent<PlayerHide>().GetHideObjPos());
                }
                return;
            }           
            
            //target_angleがangleにおさまっているか
            if (target_angle < angle)
            {
                //直線距離にプレイヤーがあるかrayを飛ばす
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    if (hit.collider == other)
                    {
                        Debug.Log("player find");
                        Vector3 playerPos = other.transform.position;
                        transform.parent.GetComponent<EnemyCoppy>().SetPlayerPosition(playerPos);
                        transform.parent.GetComponent<EnemyCoppy>().SetGoPlayer(true);
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
