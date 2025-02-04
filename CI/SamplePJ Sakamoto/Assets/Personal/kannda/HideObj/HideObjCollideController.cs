using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------
// ������
//---------------------------

public class HideObjCollideController : MonoBehaviour
{
    private Collider coll;
    private Vector3 playerPrePos;
    private bool onHide;
    private bool isHide;
    private bool upHide;
    private Vector3 hidePos;

    private void Start()
    {
        hidePos = transform.position;
    }
    private void Update()
    {
        isHide = false;
        upHide = false;
        onHide = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onHide = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            isHide = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            upHide = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnHide(other);
        IsHide(other);
        ExitHide(other);
    }

    private void OnHide(Collider other)
    {
        //�v���C���[���{�^���𐄂����u�Ԃ��擾
        bool EnterHide = onHide;
        //other.GetComponent<PlayerController>().

        //�����ĂȂ���Ώ������Ȃ�
        if (!EnterHide) { return; }

        //�A��ꏊ�̎擾
        playerPrePos = other.transform.position;
        DebugLog("EnterHide");
    }

    private void IsHide(Collider other)
    {
        //�v���C���[���{�^���������Ă��邩�擾
        bool StayHide = isHide;
        //other.GetComponent<PlayerController>().

        //�����ĂȂ���Ώ������Ȃ�
        if (!StayHide) { return; }

        //�B��鏈��
        //�����Ă���Ԃ͈ړ��ł��Ȃ��悤�ɂ��Ăق����ł��B
        other.transform.position = hidePos;
        DebugLog("StayHide");
    }

    private void ExitHide(Collider other) 
    {
        if (other.tag == "Player")
        {
            //�v���C���[���{�^���𗣂������擾
            bool ExitHide = upHide;
            //other.GetComponent<PlayerController>().

            //�{�^���������Ă���Ԃ͏������Ȃ�
            if (!ExitHide) { return; }

            //���̏ꏊ�ɖ߂�
            other.transform.position = playerPrePos;
            DebugLog("ExitHide");
        }
    }

    private void DebugLog(string code) { Debug.Log(code); }
}
