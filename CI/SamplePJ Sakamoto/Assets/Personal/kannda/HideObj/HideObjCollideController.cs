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
    private Vector3 hidePos;

    private void Start()
    {
        hidePos = transform.position;
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
        bool EnterHide =
            other.GetComponent<PlayerHide>().GetOnHide();

        //�����ĂȂ���Ώ������Ȃ�
        if (!EnterHide) { return; }

        //�A��ꏊ�̎擾
        playerPrePos = other.transform.position;
        other.gameObject.GetComponent<PlayerHide>().SetHideobjPos(transform.position);
        DebugLog("EnterHide");
    }

    private void IsHide(Collider other)
    {
        //�v���C���[���{�^���������Ă��邩�擾
        bool StayHide = 
            other.GetComponent<PlayerHide>().GetIsHIde();

        //�����ĂȂ���Ώ������Ȃ�
        if (!StayHide) { return; }

        //�B��鏈��
        //�����Ă���Ԃ͈ړ��ł��Ȃ��悤�ɂ��Ăق����ł��B
        other.transform.position = hidePos;
        other.GetComponent<PlayerHide>().SetPlayerIsHide(true);
        DebugLog("StayHide");
    }

    private void ExitHide(Collider other) 
    {
        if (other.tag == "Player")
        {
            //�v���C���[���{�^���𗣂������擾
            bool ExitHide =
                other.GetComponent<PlayerHide>().GetUpHide();

            //�{�^���������Ă���Ԃ͏������Ȃ�
            if (!ExitHide) { return; }

            //���̏ꏊ�ɖ߂�
            other.transform.position = playerPrePos;
            other.GetComponent<PlayerHide>().SetPlayerIsHide(false);
            DebugLog("ExitHide");
        }
    }

    private void DebugLog(string code) { Debug.Log(code); }
}
