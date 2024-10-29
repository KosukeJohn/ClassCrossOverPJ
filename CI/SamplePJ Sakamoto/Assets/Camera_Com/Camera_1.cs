using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_1 : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g�̒�`
    public GameObject Camera1;
    public GameObject Player;

    private Transform   cameraTrans;//Camera��Rotaiton
    private Transform   playerTrans;//Player��Rotaiton
    private float       AngleSpeed;//��]���x
    private Vector3     Angle_axis;//��]�̎�

    void Start()
    {
        //������
        cameraTrans = Camera1.transform;
        playerTrans = Player.transform;

        //Player�̉�]���x���擾
        AngleSpeed = Player.GetComponent<Player>().ro_speed;

        //Camera�̌�����Player�̌����ɏ�����
        Camera1.transform.rotation = Player.GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera�̍��W�̍X�V
        {
            Vector3 PlayerPos = Player.transform.localPosition;
            Camera1.transform.localPosition =
                new Vector3(PlayerPos.x, transform.localPosition.y, PlayerPos.z);//Player�̌��̈ʒu�ɍX�V
        }

        //Camera�̊p�x
        {
            //��]���̎擾
            Angle_axis = playerTrans.up;

            //y�̑J��
            float angle = 0;

            if (Input.GetKey(KeyCode.A))
            {
                angle = -AngleSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                angle = AngleSpeed * Time.deltaTime;
            }

            cameraTrans.localRotation = //Quaternion.AngleAxis(angle, Angle_axis)�Ŏw�肵�����ŉ�]������
                Quaternion.AngleAxis(angle, Angle_axis) * cameraTrans.localRotation;
        }      
    }
}
