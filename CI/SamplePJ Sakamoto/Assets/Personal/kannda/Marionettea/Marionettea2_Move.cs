using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
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
    private int jumpFlag;
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    public float speedX;
    private float speedY = 9.8f;
    public float chasePos = 50.0f;
    [SerializeField] private State state;
    //---------------------------------------------
    //ステータスの定義
    //---------------------------------------------
    private enum State
    {
        Non,Born,Normal,Attack, Death
    };

    private void Start()
    {
        enemy = this.gameObject;
        anim = GetComponent<Animator>();
        hitFlag = false;
        speedX /= -100;
        timeCnt = 0;
        maxCnt = 5;
        state = State.Born;
        jumpCnt = 0;
        prePos = enemy.transform.position;
        jumpFlag = 0;
    }

    private void FixedUpdate()
    {
        if (state == State.Non) { return; }

        if (state == State.Born)
        {
            enemy.transform.Translate(speedX * 8, 0, 0);

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

        if (state == State.Normal)
        {
            //被弾タイマーを取得
            hitFlag = enemy.GetComponent<Stage2HitCheck>().GetAttackFlag();

            if (hitFlag)
            {
                state = State.Attack;
            }
            else
            {
                anim.SetBool("Find", false);
                enemy.transform.Translate(speedX, 0, 0);
            }

            if (enemy.transform.position.x >= 226.68f)
            {
                state = State.Death;
            }
        }

#else
        if (state == State.Normal)
        {
            enemy.transform.Translate(speedX, 0, 0);

            if(enemy.transform.position.y <= 9.3f)
            {
                enemy.transform.position =
                    new(enemy.transform.position.x, 9.3f, enemy.transform.position.z);
            }

            if (enemy.transform.position.x >= 207f)
            {
                if (jumpFlag == 0)
                {
                    speedY = 3f;
                    speedX = -8f * Time.deltaTime;
                    jumpFlag++;
                }
                else if (jumpFlag == 1) 
                {
                    speedY -= 9.8f * Time.deltaTime;
                    enemy.transform.Translate(0, speedY, 0);
                }               
            }

            if (enemy.transform.position.x >= 212f)
            {
                if (jumpFlag == 1)
                {
                    //speedY = Mathf.Sqrt(117.6f);
                    jumpFlag++;
                }
            }

            if (enemy.transform.position.x >= 226.68f)
            {
                state = State.Death;
            }
        }

#endif
        if (state == State.Attack)
        {
            anim.SetBool("Find", true);

            if (TimeCnt(2.0f))
            {
                anim.SetBool("Find", false);
                //jumpFlag = true;
                state = State.Normal;
            }
        }

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

    private bool TimeCnt(float max)
    {
        timeCnt += Time.deltaTime;

        if (timeCnt > max)
        {
            timeCnt = 0;
            return true;
        }

        return false;
    }
}
