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
    public void OnJump()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void OnHide()
    {
        SceneManager.LoadScene("Title Scene");
    }

}
