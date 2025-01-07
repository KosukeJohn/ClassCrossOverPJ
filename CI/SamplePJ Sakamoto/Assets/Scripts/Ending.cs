using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private float timeCnt;
    private float timeMax;
    private void Start()
    {
        timeCnt = 0;
        timeMax = 30;
    }
    private void Update()
    {
        timeCnt += Time.deltaTime;
        if (timeCnt >= timeMax)
        {
            timeCnt = 0;
            SceneManager.LoadScene("Title Scene");
        }
    }
}
