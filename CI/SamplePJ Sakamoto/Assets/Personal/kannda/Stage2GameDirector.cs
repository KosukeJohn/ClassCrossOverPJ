using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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
        InstanceFlag = false;
    }
    private void Update()
    {
        if (!InstanceFlag)
        {
            if(player.transform.position.x >= this.transform.position.x)
            {
                InstanceFlag = true;
            }
        }

        if(InstanceFlag)
        {
            enemy = Instantiate(prefab);
            enemy.transform.position = this.transform.position;
        }
    }
}
