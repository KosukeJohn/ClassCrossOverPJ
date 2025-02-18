using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlackOut : MonoBehaviour
{
    [SerializeField]private PostProcessVolume postProcessVolume;
    private void Update()
    {
        if (transform.position.x <= -40f)
        {
            float weight = transform.position.x + 40;

            postProcessVolume.weight = 0.115f + Mathf.Abs(weight) / 50.0f;
        }
    }
}
