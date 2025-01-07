using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void OnJump()
    {
        SceneManager.LoadScene("mainScene");
    }

    private void OnHide()
    {
        SceneManager.LoadScene("Title Scene");
    }

}
