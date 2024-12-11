using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Notnappare_leader : MonoBehaviour
{
    private GameObject leader;
    private GameObject player;
    private State state;
    private Vector3 firstpos;
    private Vector3 prepos;
    private Vector3 playerpos;
    private float lenge;

    [SerializeField] private int nextseed;
    enum State
    {
        Non,Idle, Patrol, Find, Chase, Back 
    };

    private void Start()
    {
        leader = this.gameObject;
        player = GameObject.Find("Player");
        firstpos = leader.transform.position;
        leader.transform.position = NextPosition(0);
        nextseed = 1;
        lenge = 3.0f;
        SetState(State.Idle);
    }

    private void FixedUpdate()
    {
        if(state == State.Idle)
        {
            //ëSàıèWÇ‹Ç¡ÇΩÇÁPatrol
            SetState(State.Patrol);
        }

        if (state == State.Patrol)
        {
            if(PatrolMove(NextPosition(nextseed)))
            {
                
            }
        }
        if (state == State.Find)
        {
            playerpos = PlayerGetPosition();
            prepos = leader.transform.position;
            SetState(State.Chase);
        }
        if (state == State.Chase)
        {
            if(ChaseMove(playerpos))
            { 
                SetState(State.Back);
            }
        }
        if(state == State.Back)
        {
            if(BackMove(prepos))
            {
                SetState(State.Idle);
            }
        }
    }


    private State GetState(){ return this.state; }
    private void SetState(State s) {  this.state = s; }
    private Vector3 PlayerGetPosition()
    {
        return player.transform.position;
    }
    private bool PlayerFind()
    {
        bool playerfind = 
            leader.GetComponentInChildren<Collider_cont_notnappare>().GetPlayerFind();

        if(playerfind)
        {
            return true;
        }

        return false;
    }
    private bool PatrolMove(Vector3 pos)
    {
        if(PlayerFind())
        {
            return true;
        }

        if (Mathf.Approximately(leader.transform.position.x,pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            leader.transform.position = pos;

            if(nextseed % 2 == 0)
            {
                if (EnemyTurn(nextseed))
                {
                    nextseed++;
                    return true;
                }
            }
            else
            {
                nextseed++;
                return true;
            }
            
        }
        else
        {
            float move = 3.0f;
            leader.transform.position =
                Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);
        }
       
        return false;
    } 

    private bool EnemyTurn(int seed)
    {
        float anglespeed = 3.0f;

        if (leader.transform.localEulerAngles.y >= 90 * (seed % 8))
        {
            return true; 
        }

        transform.Rotate(0, anglespeed, 0);

        return false;
    }
    private Vector3 NextPosition(int next)
    {
        Vector3 pos = firstpos;

        switch(next % 8)
        {
            case 0:
                pos.x -= lenge;
                pos.z += lenge;
                break;
            case 1:
                pos.z += lenge;
                break;
            case 2:
                pos.x += lenge;
                pos.z += lenge;
                break;
            case 3:
                pos.x += lenge;
                break;
            case 4:
                pos.x += lenge;
                pos.z -= lenge;
                break;
            case 5:
                pos.z -= lenge;
                break;
            case 6:
                pos.x -= lenge;
                pos.z -= lenge;
                break;
            case 7:
                pos.x -= lenge;
                break;
        }

        return pos;
    }
    private bool ChaseMove(Vector3 pos)
    {
        if (Mathf.Approximately(leader.transform.position.x, pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            leader.transform.position = pos;
            return true;
        }

        float move = 3.0f;
        leader.transform.position =
            Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);

        return false;
    }
    private bool BackMove(Vector3 pos)
    {
        if (Mathf.Approximately(leader.transform.position.x, pos.x)
            && Mathf.Approximately(leader.transform.position.z, pos.z))
        {
            leader.transform.position = pos;
            return true;
        }
        else
        {
            float move = 3.0f;
            leader.transform.position =
                Vector3.MoveTowards(leader.transform.position, pos, move * Time.deltaTime);
        }
        return false;
    }

}
