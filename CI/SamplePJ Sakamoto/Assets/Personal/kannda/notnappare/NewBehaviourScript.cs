using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour

{
    float moveCnt = 0;
    float lenge = 12.0f;
    int jumpCnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveJump();
    }
    private void MoveJump(/*Vector3 pos*/)
    {
        Vector3 vector = transform.position;
        float height = 0.0f;

        if (moveCnt >= 1.0f)
        {
            jumpCnt++;
            moveCnt = 0;     
        }

        //â°à⁄ìÆ
        {
            float posx = (lenge / 3 * jumpCnt) + lenge / 3 * moveCnt;
            vector.x = posx;
        }

        //ècà⁄ìÆ
        {
            float X = moveCnt - 0.3f;
            vector.y = -(X * X) + height;
        }

        moveCnt += 1.0f * Time.deltaTime;
        transform.position = vector;
    }
}
