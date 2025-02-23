using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marionnett3_move : MonoBehaviour
{
    private GameObject enemy;
    private GameObject fall;
    private Animator anim;
    private bool moveFlag = true;
    private bool attackFlag = false;
    private bool destroyFlag = false;
    private float timeCnt;
    public float speedY;   
    public float hightPos;
    private bool playedAtkSE = false;
    bool AtkSEPlayed=false;
    float time;
    [SerializeReference] private State state;
   
    private enum State
    { ryoute,katate}
    private string hand;

    private void Start()
    {
        enemy = this.gameObject;
        fall= transform.parent.gameObject;
        enemy.transform.position =
            new(this.transform.position.x, -40, this.transform.position.z);
        anim = GetComponent<Animator>();
        //speedY = 15 * Time.deltaTime;
        time = 0.0f;
        if (state == State.ryoute) { hand = "ryoute"; }
        else { hand= "katate";}
    }
    private void Update()
    {
        //moveFlag = fall.GetComponent<fallFlag>().GetEnemyMoveFlag();

        if (moveFlag)
        {
            if (enemy.transform.position.y < hightPos)
            {
                enemy.transform.Translate(0, speedY * Time.deltaTime, 0);
            }
            else
            {
                enemy.transform.position =
                     new(this.transform.position.x, hightPos, this.transform.position.z);

                AttackSE();

                //if (!AtkSEPlayed)
                //{
                //    AtkSEPlayed = true;
                 
                    
                //        GameObject atkSE = GameObject.Find("atkSE");
                //        atkSE.GetComponent<AtkSEPlayer>().AtkSEPlay();
                //        Debug.Log("atkSEplay");
                    
                   
                //}
                attackFlag = true;
            }
        }

        if(attackFlag)
        {
            anim.SetBool(hand, true);
            fall.GetComponent<fallFlag>().SetFallFlag(true);
            


            if (TimeCnt(2.0f))
            {
               
                attackFlag = false;
                destroyFlag = true;
                speedY = 0;
            }
        }

        if(destroyFlag)
        {
            if(GetComponent<Collider>())
            {
                Collider coll = GetComponent<Collider>();
                Destroy(coll);
            }
           
            if (!enemy.GetComponent<Rigidbody>())
            {
                enemy.AddComponent<Rigidbody>();
            }

            if (enemy.transform.position.y <= -20)
            {
                Destroy(enemy);
            }
        }
    }

    private bool TimeCnt(float max)
    {
        timeCnt += Time.deltaTime;
        if (timeCnt >= max)
        {
            timeCnt = 0;
            return true;
        }
        return false;
    }
    private void AttackSE()
    {
        time += Time.deltaTime;
        if(time >= 0.7f)
        {
            if (!AtkSEPlayed)
            {
                AtkSEPlayed = true;


                GameObject atkSE = GameObject.Find("atkSE");
                atkSE.GetComponent<AtkSEPlayer>().AtkSEPlay();
                Debug.Log("atkSEplay");


            }
        }
    }
}
