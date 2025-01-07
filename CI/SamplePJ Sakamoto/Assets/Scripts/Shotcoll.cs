using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotcoll : MonoBehaviour
{
    public GameObject prefab;
    private GameObject enemy;
    private bool attackFlag;
    private bool instantFlag;

    private void Start()
    {
        this.enemy = this.gameObject;
        instantFlag = true;
    }

    private void Update()
    {
        if (enemy == null) { return; }

        attackFlag = enemy.GetComponent<Marionettea1_move>().GetAttackFlag();

        if (attackFlag)
        {
            if (instantFlag)
            {
                instantFlag = false;
                GameObject go = Instantiate(prefab);
                go.transform.position = this.transform.position;
                go.GetComponent<Marionnett1_HitBox>().SetAttackPosition(enemy.transform.forward);
            }
        }
        else 
        {
            instantFlag = true; 
        }
    }
}
