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
        //�J�����̏�����(�ŏ��͑S���f��Ȃ�)
        for(int i=0; i<Camera.Length; i++)
        {
            Camera[i].GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�{�^���������ƃJ�������؂�ւ��
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Cnt++;
        }
        //�J�E���g�̃��Z�b�g
        if (Cnt >= Camera.Length)
        {
            Cnt = 0;
        }

        Camera_endbled(Cnt);
    }

    //�J�����̐؂�ւ�
    private void Camera_endbled(int C)
    {
        //�e�[�u���̏�����
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

        //�J�����̑J��
        for (int i = 0; i < Camera.Length; i++)
        {
            Camera[i].GetComponent<Camera>().enabled = table[i];
        }
    }
}
