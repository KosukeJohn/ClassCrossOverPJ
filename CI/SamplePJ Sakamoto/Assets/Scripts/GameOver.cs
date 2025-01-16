using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
    
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    private float timemain;
    private float timetitle;
    private bool timecountmain;
    private bool timecounttitle;
    private void Start()
    {
        Application.targetFrameRate = 60;
        timemain = 0.0f;
        timetitle = 0.0f;
        timecountmain = false;
        timecounttitle = false;
    }
    public void OnJump()
    {
        source.clip = clip;
        source.Play();
       timecountmain = true;
           
        
      
    }

    public void OnHide()
    {
        source.clip = clip;
        source.Play();
        timecounttitle = true;
       
        

        
           
    }
    private void Update()
    {
        if (timecountmain == true)
        {
            timemain += Time.deltaTime;
            if(timemain>0.5f)
            {
                SceneManager.LoadScene("mainScene");
            }
        }

        if(timecounttitle == true)
        {
            timetitle += Time.deltaTime;
            if(timetitle>0.5f)
            {
                SceneManager.LoadScene("Title Scene");
            }
        }
    }
}
