using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOl : MonoBehaviour
{
    private GameObject player;
    private bool endFlag;
    private void Start()
    {
        player = GameObject.Find("Player");
        endFlag = false;
    }
    private void Update()
    {
        if(player.transform.position.x >= 72.07f)
        {
            endFlag = true;
        }
    }

    public bool GetEndFlag()
    {
        return this.endFlag;
    }
}
