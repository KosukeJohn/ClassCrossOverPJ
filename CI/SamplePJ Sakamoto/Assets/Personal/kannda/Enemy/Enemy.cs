using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_1 : Enemy 
{
    //private float life = 10;
    public Enemy_1(float life) : base(life + 10)
    {

    }
    protected override void EnemyUpDate()
    {
        Debug.Log("ê¨å˜1");
        
        //SetLife(life);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            float temp = GetLife(); 
            temp -= 10;
            SetLife(temp);
        }

        if(GetLife() <= 0)
        {
            Debug.Log("éÄñS");
            //EnemyDestroy(this.gameObject);
        }
    }
}
