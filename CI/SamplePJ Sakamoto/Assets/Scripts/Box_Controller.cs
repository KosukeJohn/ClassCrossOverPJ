using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Controller : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject enemy;
    private GameObject spotLight;
    private GameObject box;
    private GameObject player;
    private Animator anim;
    private Light redlight;
    private bool instanceFlag;
    private float timeCnt;
    private State state;
    private State prestate;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    public GameObject prefab;//ここに召喚したい敵をアタッチする
    //---------------------------------------------
    //ステータスに定義
    //---------------------------------------------
    private enum State
    {
        Non,Idle ,Find, Instance
    }

    private void Start()
    {
        //初期化
        player = GameObject.Find("Player");
        box = this.gameObject;
        anim = box.GetComponent<Animator>();
        instanceFlag = true;
        spotLight = transform.GetChild(0).gameObject;
        redlight = spotLight.GetComponent<Light>();
        redlight.enabled = false;
        timeCnt = 0;
        state = State.Idle;
    }
    private void FixedUpdate()
    {
        if(state == State.Idle)
        {
            if(PlayerFind())
            {
                state = State.Find;
            }
        }

        if(state == State.Find)
        {
            timeCnt += Time.deltaTime;

            AnimStart("Idle");

            if(timeCnt >= 0.7f)
            {
                state = State.Instance;
                timeCnt = 0;
            }
        }
        if(state == State.Instance)//プレイヤーが近づいたら
        {
            //アニメーションを再生
            AnimStart("Open");

            if (instanceFlag)
            {
                //アタッチした敵を生成
                enemy = Instantiate(prefab);

                //箱の中心かつ地面の上に生成
                Vector3 pos = box.transform.position;
                enemy.transform.position = new Vector3(pos.x, 0, pos.z);

                source.clip = clip;
                source.Play();


                //1回だけ生成させるためのフラグ
                instanceFlag = false;

                redlight.enabled = true;
            }

            if (redlight.enabled) {

                timeCnt += Time.deltaTime;

                if(timeCnt >= 1.0f)
                {
                    timeCnt = 0;
                    redlight.enabled = false;
                }
            }
        }
        prestate = state;
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

    private void AnimStart(string name)
    {
        if (prestate == state) { return; }

        anim.Play(name, 0, 0);
    }
}
