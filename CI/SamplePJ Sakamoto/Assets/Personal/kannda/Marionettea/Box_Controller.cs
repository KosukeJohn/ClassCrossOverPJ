using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Controller : MonoBehaviour
{
    public GameObject enemy;
    private GameObject box;
    private GameObject player;
    private bool Playerfind;
    private Animator anim;
    private void Start()
    {
        player = GameObject.Find("Player");
        box = this.gameObject;
        anim = box.GetComponent<Animator>();
        Playerfind = false;
    }
    private void FixedUpdate()
    {
        if(PlayerFind())
        {

        }
    }

    private bool PlayerFind()
    {
        //Rayの生成
        Ray ray = new Ray(this.box.transform.position, box.transform.up);
        Debug.DrawRay(ray.origin, ray.direction * 900, Color.blue, 5.0f);
        RaycastHit hit;

        //プレイヤーとrayが接触したか判断
        if (Physics.Raycast(ray, out hit))
        {
            //プレイヤーかタグで判断
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("hit");
                return true;
            }
        }
        return false;
    }
}
