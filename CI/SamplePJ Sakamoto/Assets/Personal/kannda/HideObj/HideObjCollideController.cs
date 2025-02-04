using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------
// 仮実装
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
        //プレイヤーがボタンを推した瞬間を取得
        bool EnterHide = onHide;
        //other.GetComponent<PlayerController>().

        //押してなければ処理しない
        if (!EnterHide) { return; }

        //帰る場所の取得
        playerPrePos = other.transform.position;
        DebugLog("EnterHide");
    }

    private void IsHide(Collider other)
    {
        //プレイヤーがボタンを押しているか取得
        bool StayHide = isHide;
        //other.GetComponent<PlayerController>().

        //押してなければ処理しない
        if (!StayHide) { return; }

        //隠れる処理
        //押している間は移動できないようにしてほしいです。
        other.transform.position = hidePos;
        DebugLog("StayHide");
    }

    private void ExitHide(Collider other) 
    {
        if (other.tag == "Player")
        {
            //プレイヤーがボタンを離したか取得
            bool ExitHide = upHide;
            //other.GetComponent<PlayerController>().

            //ボタンを押している間は処理しない
            if (!ExitHide) { return; }

            //その場所に戻る
            other.transform.position = playerPrePos;
            DebugLog("ExitHide");
        }
    }

    private void DebugLog(string code) { Debug.Log(code); }
}
