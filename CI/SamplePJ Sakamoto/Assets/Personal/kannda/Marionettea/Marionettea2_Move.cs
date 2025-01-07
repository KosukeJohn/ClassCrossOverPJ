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
    [SerializeField] private float speedY = 0;
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
            speedY -= 9.8f * Time.deltaTime;
            
            if(speedY < 0)
            {
                speedY = 0;
            }

            enemy.transform.Translate(speedX, speedY, 0);

            if(enemy.transform.position.x >= 198f)
            {
                if(jumpFlag == 0)
                {
                    state = State.Attack;
                    jumpFlag++;
                }
            }

            if (enemy.transform.position.x >= 207f)
            {
                if (jumpFlag == 1)
                {
                    state = State.Attack;
                    speedY = 50f * Time.deltaTime;
                    speedX = -8f * Time.deltaTime;
                    jumpFlag++;
                }              
            }

            if (enemy.transform.position.x >= 215f)
            {
                if (jumpFlag == 2)
                {
                    enemy.transform.position =
                    new(enemy.transform.position.x, 9.5f, enemy.transform.position.z);

                    state = State.Attack;
                    speedY = 50f * Time.deltaTime;
                    speedX = -8f * Time.deltaTime;
                    jumpFlag++;
                }
            }

            if(enemy.transform.position.x >= 223f)
            {
                if(jumpFlag == 3)
                {
                    jumpFlag++;
                    speedX = -4f * Time.deltaTime;
                    speedY = 0;
                }

                enemy.transform.position =
                    new(enemy.transform.position.x, 14.5f, enemy.transform.position.z);

            }

            if (enemy.transform.position.x >= 225f)
            {
                if (jumpFlag == 4)
                {
                    jumpFlag++;
                    state = State.Attack;
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
            hitFlag = true;
            anim.SetBool("Find", true);

            if (TimeCnt(2.0f))
            {
                hitFlag = false;
                anim.SetBool("Find", false);
                state = State.Normal;
            }
        }

        if (state == State.Death)
        {
            if(!enemy.GetComponent<Rigidbody>())
            {
                enemy.AddComponent<Rigidbody>();
            }
            
            if(enemy.transform.position.y <= -20)
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

    public bool GetHitFlag()
    {
        return this.hitFlag;
    }
}
