using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bikkuribakoSEplayer : MonoBehaviour
{
    bool play = false;
    GameObject player;

   
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

     void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name=="Player")
        {
           source.Play();
            
        }
    }
}
