using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    //インスペクターから設定するか、初期化時にGetComponentして、TextMeshProUGUIへの参照を取得しておく。
    [SerializeField]
    Image img;

    [Header("1ループの長さ(秒単位)")]
    [SerializeField]
    [Range(0.1f, 10.0f)]
    float duration = 1.0f;

    //開始時の色。
    [Header("ループ開始時の色")]
    [SerializeField]
    Color32 startColor = new Color32(255, 255, 255, 255);

    //終了(折り返し)時の色。
    [Header("ループ終了時の色")]
    [SerializeField]
    Color32 endColor = new Color32(255, 255, 255, 64);



    //インスペクターから設定した場合は、GetComponentする必要がなくなる為、Awakeを削除しても良い。
    void Awake()
    {
        if (img == null)
            img = GetComponent<Image>();
    }

    void Update()
    {
        img.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time / duration, 1.0f));
    }
}

