using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Marionettea2_Move : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject enemy;
    private Animator anim;
    private bool hitFlag;
    private float jumpCnt;
    private float jumpSpeed = 4;
    private float hight = 9.3f;
    private float endposX = 0;
    private Vector3 prePos;
    private float timeCnt;
    private float maxCnt;
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    public float speed;
    public float chasePos = 50.0f;
    [SerializeField] private State state;
    //---------------------------------------------
    //ステータスの定義
    //---------------------------------------------
    private enum State
    {
        Non,Born,Normal, Death
    };

    private void Start()
    {
        enemy = this.gameObject;
        anim = GetComponent<Animator>();
        hitFlag = false;
        speed /= -100;
        timeCnt = 0;
        maxCnt = 5;
        state = State.Born;
        jumpCnt = 0;
        prePos = enemy.transform.position;
    }

    private void Update()
    {
        if (state == State.Non) { return; }

        if (state == State.Born)
        {
            enemy.transform.Translate(speed * 8, 0, 0);

            jumpCnt += Time.deltaTime * 1.0f;
            float speedY = jumpSpeed + (-9.8f * jumpCnt);
            enemy.transform.Translate(0, speedY, 0);

            if (enemy.transform.position.x >= prePos.x + 8.0f)
            {
                enemy.transform.position =
                    new Vector3(prePos.x + 8.0f, hight, prePos.z);
                state = State.Normal;
            }
        }

#if false
        if(state == State.Normal)
        {
            enemy.transform.Translate(speed, 0, 0);
            anim.SetBool("Find", false);
            timeCnt += Time.deltaTime;

            if (timeCnt >= maxCnt)
            {
                anim.SetBool("Find", true);
                timeCnt = 0;
            }

            if (enemy.transform.position.x >= 226.68f)
            {
                state = State.Death;
            }
        }

#else
        //被弾タイマーを取得
        hitFlag = enemy.GetComponent<Stage2HitCheck>().GetAttackFlag();

        if (state == State.Normal)
        {
            if (hitFlag)
            {
                anim.SetBool("Find", true);
            }
            else
            {
                anim.SetBool("Find", false);
                enemy.transform.Translate(speed, 0, 0);
            }

            if (enemy.transform.position.x >= 226.68f)
            {
                state = State.Death;
                state = State.Death;
            }
        }
#endif

        if (state == State.Death)
        {
            if(!enemy.GetComponent<Rigidbody>())
            {
                enemy.AddComponent<Rigidbody>();
            }
            
            if(enemy.transform.position.y<=-20)
            {
                Destroy(enemy);
            }
        }
    }
}
