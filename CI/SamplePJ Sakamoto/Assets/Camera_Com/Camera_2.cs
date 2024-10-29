using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_2 : MonoBehaviour
{
    public GameObject Camera2;
    public GameObject Player;
    public bool       LookPlayer;

    private Transform cameraTrans;//Camera��Rotaiton
    private Transform playerTrans;//Player��Rotaiton
    // Start is called before the first frame update
    void Start()
    {
        //������
        cameraTrans = Camera2.transform;
        playerTrans = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (LookPlayer)//�v���C���[������
        {
            CameraRotation();
        }
        CameraMove();
    }

    void CameraMove()
    {
        //�v���C���[�ɍ��킹�č��W���X�V
        {
            Vector3 PlayerPos = Player.transform.localPosition;
            Camera2.transform.localPosition =
                new Vector3(PlayerPos.x, transform.localPosition.y, transform.localPosition.z);//Player�̉��̈ʒu�ɍX�V
        }
    }

    void CameraRotation()
    {
        //�v���C���[������
        {
            cameraTrans.LookAt(playerTrans);
        }
    }
}
