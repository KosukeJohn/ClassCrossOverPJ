using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Collider coll;
    private bool hitFlag;

    private void Start()
    {
        coll = GetComponent<Collider>();
        hitFlag = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitFlag = true;
        }
    }
    public bool GetHitFlag()
    {
        return this.hitFlag;
    }
}
