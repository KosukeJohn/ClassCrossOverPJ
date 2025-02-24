using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCameraMoveController : MonoBehaviour
{
    public float speed = 5f; // カメラの移動速度

    // Update is called once per frame
    void Update()
    {
        // X軸方向に speed の速さで移動する
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}