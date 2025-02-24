using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeatherContllore : MonoBehaviour
{
    private static bool[] isFeather = new []{ true, true, true };
    private bool isCread;
    private GameObject[] _feather = new GameObject[isFeather.Length];
    private string sceneName;

    [Header("�H�I�u�W�F�N�g")]
    [SerializeField] private GameObject feather;
    [SerializeField] private Vector3[] featherPos;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Title Scene") { AllInstance();return; }

        for (int i = 0;i < isFeather.Length;i++)
        {
            if (isFeather[i])
            {
                _feather[i] = Instantiate(feather);
                _feather[i].transform.position = featherPos[i];
            }
            else { _feather[i] = null; }
        }

        isCread = false;
    }

    private void Update()
    {
        if (sceneName == "Title Scene") { return; }

        AllGetFeather();
        InstanxeFlag();
    }
    private void InstanxeFlag() 
    {
        for (int i = 0; i < isFeather.Length; i++)
        {
            if (_feather[i] == null)
            {
                isFeather[i] = false;
            }
        }
    }
    private void AllGetFeather()
    {
        for (int i = 0; i < isFeather.Length; i++)
        {
            if (_feather[i] != null)
            {
                return;
            }
        }

        isCread = true;
    }
    private void AllInstance()
    {
        for (int i = 0; i < isFeather.Length; i++)
        {
            isFeather[i] = true;
        }
    }

    // �Q�Ɖ\
    public bool GetIsCread() { return isCread; }
}
