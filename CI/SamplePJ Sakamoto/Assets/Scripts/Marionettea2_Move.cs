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
    private Light redLight;
    private Light blueLight;
    private Animator anim;
    private bool hitFlag;
    private float jumpCnt;
    private float jumpSpeed = 4;
    private float hight = 9.3f;
    //private float endposX = 0;
    private Vector3 prePos;
    private float timeCnt;
    //private float maxCnt;
    private int jumpFlag;
    //---------------------------------------------
    //インスペクター参照可
    //---------------------------------------------
    public float speedX;
    [SerializeField] private float speedY;
    public float chasePos = 50.0f;
    [SerializeField] private State state;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip atk;
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
        speedY = 5.0f;
        timeCnt = 0;
        //maxCnt = 5;
        state = State.Born;
        jumpCnt = 0;
        prePos = enemy.transform.position;
        jumpFlag = 0;
        blueLight = transform.GetChild(0).GetComponent<Light>();
        redLight = transform.GetChild(1).GetComponent<Light>();
        blueLight.enabled = true;
        redLight.enabled = false;
    }

    private void FixedUpdate()
    {
        if (state == State.Non) { return; }

        if (state == State.Born)
        {
            enemy.transform.Translate(speedX, speedY, 0);
            speedY -= 9.8f * Time.deltaTime;

            if (enemy.transform.position.x >= prePos.x + 8.0f)
            {
                enemy.transform.position =
                    new Vector3(prePos.x + 8.0f, hight, prePos.z);
                speedY = 0;
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
            hitFlag = false;
            speedY -= 9.8f * Time.deltaTime;

            if (speedY < 0)
            {
                speedY = 0;
            }

            enemy.transform.Translate(speedX, speedY, 0);

            if (enemy.transform.position.x >= 190f)
            {
                if (jumpFlag == 0)
                {
                    state = State.Attack;
                    //speedX = -15f * Time.deltaTime;
                    source.clip = atk;
                    source.Play();
                    jumpFlag++;
                }
            }

            if (enemy.transform.position.x >= 198f)
            {
                if (jumpFlag == 1)
                {
                    state = State.Attack;
                    source.clip = atk;
                    source.Play();
                    speedX = -20f * Time.deltaTime;
                    jumpFlag++;
                }
            }

            if (enemy.transform.position.x >= 207f)
            {
                if (jumpFlag == 2)
                {
                    state = State.Attack;
                    source.clip = atk;
                    source.Play();
                    speedY = 50f * Time.deltaTime;
                    speedX = -25f * Time.deltaTime;
                    jumpFlag++;
                }
            }

            if (enemy.transform.position.x >= 215f)
            {
                if (jumpFlag == 3)
                {
                    enemy.transform.position =
                    new(enemy.transform.position.x, 9.5f, enemy.transform.position.z);

                    state = State.Attack;
                    source.clip = atk;
                    source.Play();
                    speedY = 50f * Time.deltaTime;
                    speedX = -30f * Time.deltaTime;
                    jumpFlag++;
                }
            }

            if (enemy.transform.position.x >= 223f)
            {
                if (jumpFlag == 4)
                {
                    jumpFlag++;
                    source.clip = atk;
                    source.Play();
                    speedX = -35f * Time.deltaTime;
                    speedY = 0;
                }

                enemy.transform.position =
                    new(enemy.transform.position.x, 14.5f, enemy.transform.position.z);

            }

            if (enemy.transform.position.x >= 225f)
            {
                if (jumpFlag == 5)
                {
                    jumpFlag++;
                    source.clip = atk;
                    source.Play();
                    speedX = -5f * Time.deltaTime;
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
            anim.SetBool("Find", true);
            timeCnt += Time.deltaTime;
            ChangeLightColor(true);

            if (timeCnt > 0.5f)
            {
                hitFlag = true;
            }

            if (timeCnt > 1.5f)
            {
                hitFlag = false;
                ChangeLightColor(false);
            }

            if (timeCnt >= 2.0f)
            {
                anim.SetBool("Find", false);
                timeCnt = 0;
                state = State.Normal;
            }
        }

        if (state == State.Death)
        {
            if (!enemy.GetComponent<Rigidbody>())
            {
                enemy.AddComponent<Rigidbody>();
            }

            if (enemy.transform.position.y <= -20)
            {
                redLight.enabled = false;
                blueLight.enabled = false;
                Destroy(enemy);
            }
        }
    }
    public bool GetHitFlag()
    {
        return this.hitFlag;
    }

    private void ChangeLightColor(bool onLight)
    {
        if (onLight)
        {
            redLight.enabled = true;
            blueLight.enabled = false;
        }
        else
        {
            redLight.enabled = false;
            blueLight.enabled = true;
        }
    }
}
