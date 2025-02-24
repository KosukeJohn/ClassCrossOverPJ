using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Camera : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position =
            new(player.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
