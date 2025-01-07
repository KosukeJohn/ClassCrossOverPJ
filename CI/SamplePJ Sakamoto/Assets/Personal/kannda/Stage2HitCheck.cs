using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2HitCheck : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hitcheck;
    private Collider coll;
    [SerializeField] private bool attackFlag;
    [SerializeField] private bool hitFlag;
    
    void Start()
    {
        enemy = this.gameObject;
        hitcheck = GameObject.Find("HitCheck");
        coll = GetComponent<Collider>();
        attackFlag = false;
        hitFlag = false;
    }

    void Update()
    {
        attackFlag = enemy.GetComponent<Marionettea2_Move>().GetHitFlag();
        hitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitFlag);

        if (hitFlag)
        {
            hitFlag = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (attackFlag) 
        {
            if(other.tag == "Player")
            {
                hitFlag = true;
            }
        }
    }


}
