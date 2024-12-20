using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Controller : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject enemy;
    private GameObject box;
    private GameObject player;
    private Animator anim;
    private bool instanceFlag;
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    public GameObject prefab;//ここに召喚したい敵をアタッチする
    private void Start()
    {
        //初期化
        player = GameObject.Find("Player");
        box = this.gameObject;
        anim = box.GetComponent<Animator>();
        instanceFlag = true;
    }
    private void FixedUpdate()
    {
        if(PlayerFind())//プレイヤーが近づいたら
        {
            //アニメーションを再生
            anim.SetBool("open", true);

            if(instanceFlag)
            {
                //アタッチした敵を生成
                enemy = Instantiate(prefab);

                //箱の中心かつ地面の上に生成
                Vector3 pos = box.transform.position;
                enemy.transform.position = new Vector3(pos.x, 0, pos.z);

                //1回だけ生成させるためのフラグ
                instanceFlag = false;
            }
        }
    }

    private bool PlayerFind()
    {
        //プレイヤーが箱を通り過ぎるとtrueを返す
        if(player.transform.position.x >= box.transform.position.x)
        {
            return true;
        }

        return false;
    }
}
