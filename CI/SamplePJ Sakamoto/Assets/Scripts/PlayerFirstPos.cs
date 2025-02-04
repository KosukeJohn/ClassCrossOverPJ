using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstPos : MonoBehaviour
{
    private static Vector3 firstPos = new(-10, 0, 0);

    public bool debag;//true‚É‚·‚é‚Æ‚»‚ÌˆÊ’u‚ÅŽn‚Ü‚é

    public void SetFirstPos(Vector3 pos)
    {
        firstPos = pos;
    }
    public Vector3 GetFirstPos()
    {
        if (debag)
        { 
            return this.transform.position;
        }

        return firstPos;
    }
}
