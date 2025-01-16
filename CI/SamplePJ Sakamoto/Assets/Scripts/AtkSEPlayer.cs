using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkSEPlayer : MonoBehaviour
{

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip Atk;
    // Start is called before the first frame update
    public void AtkSEPlay()
    {
        source.clip = Atk;
        source.Play();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
