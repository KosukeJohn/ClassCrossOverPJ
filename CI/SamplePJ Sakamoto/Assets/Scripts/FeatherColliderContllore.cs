using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherColliderContllore : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 3.0f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
