using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marionnett1_HitBox : MonoBehaviour
{
    private Collider coll;
    private GameObject hitcheck;
    private Vector3 attackPos;
    private bool hitFlag;
    private float timeCnt;
    private float maxCnt;

    private void Start()
    {
        coll = GetComponent<Collider>();
        hitcheck = GameObject.Find("HitCheck");
        hitFlag = false;
        timeCnt = 0;
        maxCnt = 0.8f;
    }
    private void Update()
    {
        timeCnt += Time.deltaTime;
        if (timeCnt >= maxCnt)
        {
            Destroy(this.gameObject);
        }

        this.transform.localPosition += this.attackPos / 10;

        hitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitFlag);

        if (hitFlag) 
        {
            hitFlag = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitFlag = true;
        }
    }

    public void SetAttackPosition(Vector3 pos)
    {
        this.attackPos = pos;
    }
}
