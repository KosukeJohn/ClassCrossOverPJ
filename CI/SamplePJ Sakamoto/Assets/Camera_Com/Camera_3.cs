using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_3 : MonoBehaviour
{
    public GameObject[] Camera;

    private int  Cnt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //カメラの初期化(最初は全部映らない)
        for(int i=0; i<Camera.Length; i++)
        {
            Camera[i].GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ボタンを押すとカメラが切り替わる
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Cnt++;
        }
        //カウントのリセット
        if (Cnt >= Camera.Length)
        {
            Cnt = 0;
        }

        Camera_endbled(Cnt);
    }

    //カメラの切り替え
    private void Camera_endbled(int C)
    {
        //テーブルの初期化
        bool[] table = new bool[Camera.Length];
        for (int i = 0; i < Camera.Length; i++)
        {
            if (i == C)
            {
                table[i] = true;
            }
            else
            {
                table[i] = false;
            }
        }

        //カメラの遷移
        for (int i = 0; i < Camera.Length; i++)
        {
            Camera[i].GetComponent<Camera>().enabled = table[i];
        }
    }
}
