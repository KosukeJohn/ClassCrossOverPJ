using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Marionnett1_HitBox : MonoBehaviour
{
    private Collider coll;
    private Transform enemyTrans;
    private bool hitFlag;
    private bool attackFlag;
    private Vector3 attackPos;


    private void Start()
    {
        coll = GetComponent<Collider>();
        //coll.enabled = false;
        hitFlag = false;
    }
    private void Update()
    {
        attackFlag = GetComponentInParent<Marionettea1_move>().GetAttackFlag();
        enemyTrans = GetComponentInParent<Transform>();
        this.transform.rotation = enemyTrans.transform.rotation;

        if (attackFlag)
        {
            //
            attackPos = new(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.427f);
            float move = 3.0f;
            transform.localPosition =
                Vector3.MoveTowards(transform.localPosition, attackPos, move * Time.deltaTime);
        }
        else { transform.position = new(0, transform.localPosition.y, 0); }

        if(hitFlag)
        {
            hitFlag = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitFlag = true;
        }
    }
    //public bool GetHitFlag()
    //{
    //    return this.hitFlag;
    //}
}
