using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCameraMoveController : MonoBehaviour
{
    public float speed = 5f; // �J�����̈ړ����x

    // Update is called once per frame
    void Update()
    {
        // X�������� speed �̑����ňړ�����
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}