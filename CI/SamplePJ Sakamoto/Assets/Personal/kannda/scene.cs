using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene : MonoBehaviour
{
    public GameObject hitCheck;
    //public GameObject endCheck;
    private bool hitFlag;
    private bool clearFlag;

    private void Start()
    {
        hitFlag = false;
        clearFlag = false;
    }

    private void Update()
    {
        hitFlag = hitCheck.GetComponent<PlayerHitCheck>().GetPlayerHitCheck();
        //clearFlag = endCheck.GetComponent<GOl>().GetEndFlag();

        if (hitFlag)
        {
            SceneManager.LoadScene("GameOverScene");
        }

        //if (clearFlag)
        //{
        //    SceneManager.LoadScene("EndingScene");
        //}
    }
}
