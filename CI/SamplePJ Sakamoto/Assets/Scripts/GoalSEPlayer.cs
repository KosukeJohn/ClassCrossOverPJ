using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSEPlayer : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip ;
    // Start is called before the first frame update

    public void PlaySound()
    {
        source.clip = clip ;
        source.Play();
        Debug.Log("PlayGoalSE");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
