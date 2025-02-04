using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    private bool onHide;
    private bool isHide;
    private bool upHide;
    private bool PlayerisHide;
    private Vector3 HideObjPos;
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

    public bool GetOnHide() { return onHide; }
    public bool GetIsHIde() { return isHide; }
    public bool GetUpHide() { return upHide; }

    public bool GetPlayerisHide() { return PlayerisHide; }
    public void SetPlayerIsHide(bool isHide) { PlayerisHide = isHide; }
    public void SetHideobjPos(Vector3 pos) { HideObjPos = pos; }
    public Vector3 GetHideObjPos() { return HideObjPos; }
}
