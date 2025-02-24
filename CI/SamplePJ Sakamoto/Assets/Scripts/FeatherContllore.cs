using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class FeatherContllore : MonoBehaviour
{
    private static bool[] isFeather = new []{ true, true, true };
    private bool[] isSound = new bool[isFeather.Length];
    private bool isCread;
    private GameObject[] _feather = new GameObject[isFeather.Length];
    private string sceneName;

    [Header("羽オブジェクト")]
    [SerializeField] private GameObject feather;
    [SerializeField] private Vector3[] featherPos;
    [SerializeField] private Image[] image = new Image[isFeather.Length];

    [Header("効果音")]
    [SerializeField] AudioSource source;//オーディオソース
    [SerializeField] AudioClip Get;

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
                image[i].color = new Color(255, 255, 255, 0);
                isSound[i] = true;
            }
            else 
            {
                _feather[i] = null;
            }
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
                image[i].color = new Color(255, 255, 255, 255);
                if (isSound[i]) { isSound[i] = false; Sound(); }
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
    private void Sound()
    {
        source.clip = Get;
        source.Play();
    }
    // 参照可能
    public bool GetIsCread() { return isCread; }
}
