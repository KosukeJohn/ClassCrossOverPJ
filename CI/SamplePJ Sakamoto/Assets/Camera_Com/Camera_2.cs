using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_2 : MonoBehaviour
{
    public GameObject Camera2;
    public GameObject Player;
    public bool       LookPlayer;

    private Transform cameraTrans;//CameraのRotaiton
    private Transform playerTrans;//PlayerのRotaiton
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        cameraTrans = Camera2.transform;
        playerTrans = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (LookPlayer)//プレイヤーを見る
        {
            CameraRotation();
        }
        CameraMove();
    }

    void CameraMove()
    {
        //プレイヤーに合わせて座標を更新
        {
            Vector3 PlayerPos = Player.transform.localPosition;
            Camera2.transform.localPosition =
                new Vector3(PlayerPos.x, transform.localPosition.y, transform.localPosition.z);//Playerの横の位置に更新
        }
    }

    void CameraRotation()
    {
        //プレイヤーを見る
        {
            cameraTrans.LookAt(playerTrans);
        }
    }
}
