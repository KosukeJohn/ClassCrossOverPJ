using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2GameDirector : MonoBehaviour
{
    public GameObject prefab;
    private GameObject enemy;
    private GameObject player;
    private bool InstanceFlag;
    private void Start()
    {
        player = GameObject.Find("Player");
        InstanceFlag = true;
    }
    private void Update()
    {
        if(InstanceFlag)
        {
            if (player.transform.position.x >= this.transform.position.x)
            {
                InstanceFlag = false;
                enemy = Instantiate(prefab);
                enemy.transform.position = this.transform.position;
            }
        }
    }
}
