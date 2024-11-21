using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_1 : MonoBehaviour
{
    //ゲームオブジェクトの定義
    public GameObject Camera1;
    public GameObject Player;

    private Transform   cameraTrans;//CameraのRotaiton
    private Transform   playerTrans;//PlayerのRotaiton
    private float       AngleSpeed;//回転速度
    private Vector3     Angle_axis;//回転の軸

    void Start()
    {
        //初期化
        cameraTrans = Camera1.transform;
        playerTrans = Player.transform;

        //Playerの回転速度を取得
        AngleSpeed = Player.GetComponent<Player>().ro_speed;

        //Cameraの向きをPlayerの向きに初期化
        Camera1.transform.rotation = Player.GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Cameraの座標の更新
        {
            Vector3 PlayerPos = Player.transform.localPosition;
            Camera1.transform.localPosition =
                new Vector3(PlayerPos.x, transform.localPosition.y, PlayerPos.z);//Playerの後ろの位置に更新
        }

        //Cameraの角度
        {
            //回転軸の取得
            Angle_axis = playerTrans.up;

            //yの遷移
            float angle = 0;

            if (Input.GetKey(KeyCode.A))
            {
                angle = -AngleSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                angle = AngleSpeed * Time.deltaTime;
            }

            cameraTrans.localRotation = //Quaternion.AngleAxis(angle, Angle_axis)で指定した軸で回転させる
                Quaternion.AngleAxis(angle, Angle_axis) * cameraTrans.localRotation;
        }      
    }
}
