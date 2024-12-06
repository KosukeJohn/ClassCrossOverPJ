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
        //Ray�̐���
        Ray ray = new Ray(this.box.transform.position, box.transform.up);
        Debug.DrawRay(ray.origin, ray.direction * 900, Color.blue, 5.0f);
        RaycastHit hit;

        //�v���C���[��ray���ڐG���������f
        if (Physics.Raycast(ray, out hit))
        {
            //�v���C���[���^�O�Ŕ��f
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("hit");
                return true;
            }
        }
        return false;
    }
}
