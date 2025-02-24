using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleLogoController : MonoBehaviour
{ 
    GameObject logo;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        logo = GameObject.Find("title");
        logo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 6.5)
        {
            logo.SetActive(true);
        }
    }
}
