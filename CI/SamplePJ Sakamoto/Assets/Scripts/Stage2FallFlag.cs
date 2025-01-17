using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2FallFlag : MonoBehaviour
{
    private GameObject hitCheck;
    private Collider coll;
    void Start()
    {
        hitCheck = GameObject.Find("HitCheck");
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitCheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(true);
        }
    }

}
