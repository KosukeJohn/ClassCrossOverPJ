using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_cont : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g�̒�`
    public GameObject Camera;
    public GameObject Player;

    private Transform cameraTrans;//Camera��Rotaiton
    private Transform playerTrans;//Player��Rotaiton

    void Start()
    {
        //������
        cameraTrans = Camera.transform;
        playerTrans = Player.transform;

        //Camera�̌�����Player�̌����ɏ�����
        //Camera.transform.rotation = Player.GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera�̍��W�̍X�V
        {
            Vector3 PlayerPos = Player.transform.localPosition;
            Camera.transform.localPosition =
                new Vector3(transform.localPosition.x, transform.localPosition.y, PlayerPos.z); 
        }
    }
}
