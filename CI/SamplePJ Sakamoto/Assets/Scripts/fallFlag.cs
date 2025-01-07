using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallFlag : MonoBehaviour
{
    private Collider coll;
    private bool enemymove;
    private bool fall;
    private void Start()
    {
        coll = GetComponent<Collider>();
        enemymove = false;
        fall= false;
    }

    private void Update()
    {
        
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
