using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marionetto : MonoBehaviour
{
    private Collider coll;
    private GameObject hitcheck;
    private bool hitFlag;
    private void Start()
    {
        coll = GetComponent<Collider>();
        hitcheck = GameObject.Find("HitCheck");
    }
    private void Update()
    {
        hitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitFlag);

        if (hitFlag)
        {
            hitFlag = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // âBÇÍÇƒÇ¢ÇÈä‘ÇÃèàóù
            bool isHide = false;
            if (other.GetComponent<PlayerController>())
            {
                isHide = other.GetComponent<PlayerController>().IsHiding;
            }
            if (isHide) { return; }

            hitFlag = true;
        }
    }
}
