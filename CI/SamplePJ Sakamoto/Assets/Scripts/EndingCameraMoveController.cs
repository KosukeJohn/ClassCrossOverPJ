using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCameraMoveController : MonoBehaviour
{
    public float speed = 5f; // ƒJƒƒ‰‚ÌˆÚ“®‘¬“x

    // Update is called once per frame
    void Update()
    {
        // X²•ûŒü‚É speed ‚Ì‘¬‚³‚ÅˆÚ“®‚·‚é
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}