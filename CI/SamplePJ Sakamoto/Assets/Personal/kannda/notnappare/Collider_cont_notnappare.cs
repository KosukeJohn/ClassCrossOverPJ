using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_cont_notnappare : MonoBehaviour
{
    private Collider coll;
    private GameObject obj;
    private float angle;

    [SerializeField] private bool playerfind;

    private void Start()
    {
        obj = this.gameObject;
        coll = obj.GetComponent<Collider>();
        playerfind = false;
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

            //target_angleがangleにおさまっているか
            if (target_angle < angle)
            {
                //直線距離にプレイヤーがあるかrayを飛ばす
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    if (hit.collider == other)
                    {
                        Debug.Log("player find");
                        playerfind = true;
                    }
                }
            }
        }
    }

    public bool GetPlayerFind()
    {
        return this.playerfind;
    }

    public void SetPlayerFind(bool flag)
    {
        this.playerfind = flag;
    }
}
