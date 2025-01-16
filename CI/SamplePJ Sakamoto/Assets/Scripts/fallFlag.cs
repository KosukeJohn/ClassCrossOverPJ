using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallFlag : MonoBehaviour
{
    private Collider coll;
    private Light greenLihgt;
    private bool enemymove;
    private bool fall;
    bool AtkSEPlayed = false;
    private void Start()
    {
        coll = GetComponent<Collider>();
        enemymove = false;
        fall= false;
        greenLihgt = transform.GetChild(0).GetComponent<Light>();
        greenLihgt.enabled = true;
    }

    private void Update()
    {
        if (enemymove)
        {
            greenLihgt.enabled = false;
        }
        if (fall) {
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemymove = true;
        }
    }

    public bool GetEnemyMoveFlag()
    {
        return this.enemymove;
    }

    public void SetFallFlag(bool flag)
    {
        fall = flag;
    }
    public bool GetFallFlag()
    {
        return this.fall;
    }
}
