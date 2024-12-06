using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Notnappare_leader : MonoBehaviour
{
    private GameObject leader;
    private GameObject player;
    private State state;
    private Vector3 firstpos;
    enum State
    {
        Idle,Patrol, Find, Chase, Back, Non
    };

    private void Start()
    {
        leader = this.gameObject;
        player = GameObject.Find("Player");
        firstpos = leader.transform.position;
    }

    private void FixedUpdate()
    {
        
    }


    private State GetState(){ return this.state; }
    private void SetState(State s) {  this.state = s; }
    private Vector3 PlayerGetPosition()
    {
        return player.transform.position;
    }
    private bool PlayerFind()
    {
        bool playerfind = leader.GetComponentInChildren<Collider_cont_notnappare>().playerfind;

        if(playerfind)
        {
            SetState(State.Find);
            return true;
        }

        SetState(State.Idle);
        return false;
    }
    private bool PatrolMove()
    {
        if(PlayerFind())
        {
            return true;
        }



        return false;
    } 

    private Vector3 NextPosition(int next)
    {
        Vector3 pos;


        pos = firstpos;

        return pos;
    }

}
