using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
public class Scene : MonoBehaviour
{
    public GameObject hitCheck;
    private bool hitFlag;
    private float firstWight;

    [SerializeField] private PostProcessVolume postProcessVolume;
    [SerializeField] private Image[] featherCnt = new Image[3];

    private void Start()
    {
        hitFlag = false;
        firstWight = postProcessVolume.weight;
    }

    private void Update()
    {
        hitFlag = hitCheck.GetComponent<PlayerHitCheck>().GetPlayerHitCheck();

        if (hitFlag)
        {
            for (int i = 0; i < featherCnt.Length; i++)
            {
                featherCnt[i].color = new Color(0, 0, 0, 0);
            }
            StartCoroutine(Fadeenumerator(1f));         
        }
    }

    private IEnumerator Fadeenumerator(float maxTimeCnt)
    {
        float weight = 1 - firstWight;
        postProcessVolume.weight += weight * Time.deltaTime / maxTimeCnt;

        if (postProcessVolume.weight >= 1)
        {
            postProcessVolume.weight = 1;

            Image image = GameObject.Find("BlackSheet").GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);            
        }

        yield return new WaitForSeconds(maxTimeCnt);

        SceneManager.LoadScene("GameOverScene");
    }
}
