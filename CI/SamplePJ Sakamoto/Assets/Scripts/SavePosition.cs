using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    private GameObject player;
    private Collider coll;

    void Start()
    {
        player = GameObject.Find("Player");
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Vector3 pos = new(transform.position.x, transform.position.y, transform.position.z - 4);
            player.GetComponent<PlayerFirstPos>().SetFirstPos(pos);
        }
    }
}
