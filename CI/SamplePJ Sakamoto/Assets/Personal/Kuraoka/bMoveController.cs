using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bMoveController : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] float posy;
    [SerializeField] float posx;
    [SerializeField] float posz;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posx = rectTransform.position.x;
        posy = rectTransform.position.y;
        posz = rectTransform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            posy -= 350 * Time.deltaTime;
            rectTransform.position = new Vector3(posx, posy, posz);
            if (posy < 800)
            {
                posy = 800;
                rectTransform.position = new Vector3(posx, posy, posz);
            }
        }


    }
}
