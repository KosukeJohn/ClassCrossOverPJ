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
    private float speedY;
    private float timeCnt;
    public float hightPos;
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
        speedY = 15 * Time.deltaTime;
        if (state == State.ryoute) { hand = "ryoute"; }
        else { hand= "katate";}
    }
    private void Update()
    {
        moveFlag = fall.GetComponent<fallFlag>().GetEnemyMoveFlag();

        if (moveFlag)
        {
            if (enemy.transform.position.y < hightPos)
            {
                enemy.transform.Translate(0, speedY, 0);
            }
            else
            {
                enemy.transform.position =
                     new(this.transform.position.x, hightPos, this.transform.position.z);
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
}
