using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject cam;
    GameObject camPosition;
    Text camTextX;
    Camera camComponent;
    public float camPositionX;
    public float camPositionY;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        camComponent = cam.GetComponent<Camera>();
        camPosition = GameObject.Find("CameraPosition");
        camTextX = camPosition.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

        camTextX.text = "x:" + cam.transform.position.x.ToString() + "\n" + "y:" + cam.transform.position.y.ToString();

    }

    public void CameraYPlus()
    {
        Vector3 newPosition = cam.transform.position;
        newPosition.y += 1;
        cam.transform.position = newPosition;
    }

    public void CameraYMinus()
    {
        Vector3 newPosition = cam.transform.position;
        newPosition.y -= 1;
        cam.transform.position = newPosition;
    }
}


