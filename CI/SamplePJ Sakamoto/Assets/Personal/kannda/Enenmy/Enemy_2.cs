using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    private float life = 5;
    //private float target = 10;
    protected override void EnemyUpDate()
    {
        Debug.Log("ê¨å˜2");

        SetLife(life);
        SetObject(this.gameObject);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float temp = GetLife();
            temp -= 10;
            SetLife(temp);
        }

        if (GetLife() <= 0)
        {
            Debug.Log("éÄñS");
            EnemyDestroy(GetObject());
        }
    }
}
