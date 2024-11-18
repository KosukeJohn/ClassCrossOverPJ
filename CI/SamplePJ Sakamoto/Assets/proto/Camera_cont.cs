using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_cont : MonoBehaviour
{
    //ゲームオブジェクトの定義
    public GameObject Camera;
    public GameObject Player;

    private Transform cameraTrans;//CameraのRotaiton
    private Transform playerTrans;//PlayerのRotaiton

    void Start()
    {
        //初期化
        cameraTrans = Camera.transform;
        playerTrans = Player.transform;

        //Cameraの向きをPlayerの向きに初期化
        //Camera.transform.rotation = Player.GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Cameraの座標の更新
        {
            Vector3 PlayerPos = Player.transform.localPosition;
            Camera.transform.localPosition =
                new Vector3(transform.localPosition.x, transform.localPosition.y, PlayerPos.z); 
        }
    }
}
