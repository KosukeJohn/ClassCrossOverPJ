using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject player;//プレイヤー
    private GameObject obj;//このオブジェクト   
    private GameObject enemy;//召喚したprefab
    private bool destroyFlag;//1回だけ召喚するためのフラグ
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    public GameObject prefab;//ここに召喚したい敵をアタッチする

    private void Start()
    {
        //初期化
        {
            player = GameObject.Find("Player");
            obj = this.gameObject;
            destroyFlag = true;
        }
    }

    private void Update()
    {
        if (FindPlayer())
        {
            if (destroyFlag)
            {
                //召喚不可にする
                destroyFlag = false;

                //このオブジェクトの座標に召喚させる
                enemy = Instantiate(prefab);
                enemy.transform.position = obj.transform.position;
            }
        }
        else
        {
            //召喚可能にする
            destroyFlag = true;

            //召喚した敵を破壊
            if (enemy != null) { Destroy(enemy); }
        }
    }

    private bool FindPlayer()
    {
        //プレイヤーが近づくとtrueを返す
        if(obj.transform.position.x <= PlayerPosMaxX())
        {
            if(obj.transform.position.x >= PlayerPosMinX())
            {
                return true;
            }
        }
        return false;
    }
    private float PlayerPosMaxX()
    {
        //x座標の最大値
        return player.transform.position.x + 11.0f;
    }
    private float PlayerPosMinX()
    {
        //x座標の最小値
        return player.transform.position.x - 11.0f;
    }
}
