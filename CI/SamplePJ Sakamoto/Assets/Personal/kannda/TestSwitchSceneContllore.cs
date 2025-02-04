using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSwitchSceneContllore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Test");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Test 1");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Test 2");
        }
    }
}
