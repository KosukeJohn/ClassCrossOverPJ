using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Controller : MonoBehaviour
{
    private Collider _collider;
    public bool playerfind;
    private GameObject obj;
    private float angle;
    public int num;
    private void Start()
    {
        obj = this.gameObject;
        _collider = obj.GetComponent<Collider>();
        playerfind = false;
        angle = 10.0f;
    }
    private void OnTriggerStay(Collider other)
    {
        //範囲に入っているのがプレイヤーか
        if(other.gameObject.tag == "Player")
        {
            //視界の角度におさまっているか
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            //target_angleがangleにおさまっているか
            if (target_angle < angle)
            {
                //直線距離にプレイヤーがあるかrayを飛ばす
                if(Physics.Raycast(this.transform.position,posDelta,out RaycastHit hit))
                {
                    if(hit.collider　==　other)
                    {
                        Debug.Log("player find");
                        playerfind = true;
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
                    num = 1;
                }
                else
                {
                    num = -1;
                }
                playerfind = false;
            }
        }
    }
}
