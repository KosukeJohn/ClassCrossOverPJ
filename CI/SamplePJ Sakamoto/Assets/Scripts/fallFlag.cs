using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallFlag : MonoBehaviour
{
    private Collider coll;
    private bool fall;
    bool AtkSEPlayed = false;
    private void Start()
    {
        coll = GetComponent<Collider>();
        fall= false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(transform.GetChild(0).GetComponent<Enemy3>())
            transform.GetChild(0).GetComponent<Enemy3>().SetOnMove(true);
        }
    }

    public void SetFallFlag(bool fallFlag = false) { fall = fallFlag; }
    public bool GetFallFlag() { return fall; }
}
