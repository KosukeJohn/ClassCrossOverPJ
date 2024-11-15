using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Patrol : MonoBehaviour
{
    private int EnemyState;//仮ステータス
    public GameObject State;//参照用オブジェクト
    public int NextPoint = 0;
    
    private void Update()
    {
        //オブジェクトのステータスを取得
        EnemyState = State.GetComponent<Enemy_State>().GetState();

        //探索("Patrol")の時のみ更新処理
        if (EnemyState == (int)Enemy_State.EnemyState.Patrol)
        {
            Debug.Log("Patrol");

            float PreX = Enemy_GetPosition().x;
            float PreZ = Enemy_GetPosition().z;

            if (Enemy_Move(SetNextPosition(NextPoint % 9)))
            {
                NextPoint++;
                Debug.Log("Patrol->Idel");
                State.GetComponent<Enemy_State>().SetState(Enemy_State.EnemyState.Idel);
            }
        } 
    }
    private Vector3 SetNextPosition(int point)
    {
        float Length = 3;
        float posY = 0.5f;

        Vector3 pos= new(Length, posY,Length);
        switch(point)
        {
            case 0:
                pos = new Vector3(0 * Length, posY, 2 * Length);
                break;
            case 1:
                pos = new Vector3(-1 * Length, posY, 2 * Length);
                break;
            case 2:
                pos = new Vector3(-2 * Length, posY, 2 * Length);
                break;
            case 3:
                pos = new Vector3(-2 * Length, posY, 1 * Length);
                break;
            case 4:
                pos = new Vector3(-2 * Length, posY, 0 * Length);
                break;
            case 5:
                pos = new Vector3(-1* Length, posY, 0 * Length);
                break;
            case 6:
                pos = new Vector3(0 * Length, posY, 0 * Length);
                break;
            case 7:
                pos = new Vector3(0 * Length, posY, 1 * Length);
                break;
            default:
                break;
        }
        return pos;
    }
    private Vector3 Enemy_GetPosition()
    { 
        return this.State.transform.position;
    }
    private bool Enemy_Move(Vector3 pos)
    {
        float moveX = 0.1f;
        float moveZ = 0.1f;

        if (Mathf.Approximately(Enemy_GetPosition().x, pos.x))
            if (Mathf.Approximately(Enemy_GetPosition().z, pos.z))
            {
                return true;
            }
                
        if (Enemy_GetPosition().x <= pos.x)
        {
            transform.Translate( moveX , 0 , 0);
        }
        if (Enemy_GetPosition().x >= pos.x)
        {
            transform.Translate(-moveX, 0, 0);
        }
        if (Enemy_GetPosition().z <= pos.z)
        {
            transform.Translate(0, 0, moveZ);
        }
        if (Enemy_GetPosition().z >= pos.z)
        {
            transform.Translate(0, 0, -moveZ);
        }
        return false;
    }
   
}
