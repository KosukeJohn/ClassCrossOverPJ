using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstPos : MonoBehaviour
{
    private static Vector3 firstPos = new(0, 0, 0);

    public void SetFirstPos(Vector3 pos)
    {
        firstPos = pos;
    }
    public Vector3 GetFirstPos()
    {
        return firstPos;
    }
}
