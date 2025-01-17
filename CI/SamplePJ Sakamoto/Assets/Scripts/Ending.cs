using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    [SerializeField]private Image prepareImage;
    [SerializeField]private VideoPlayer videoPlayer;
    private void Start()
    {
        // 読み込みまで仮で表示しておく白背景
        prepareImage = GameObject.Find("PrepareImage").GetComponent<Image>();

        // 動画準備
        videoPlayer = GameObject.Find("videoPlayer").GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "ToyBox_Endimg.mp4");
        videoPlayer.Prepare();

        // 動画の準備完了時のイベントを登録
        videoPlayer.prepareCompleted += OnPrepareCompleted;
        // 動画の再生終了時のイベントを登録
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    private void Update()
    {
        // pass
    }

    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        // 準備完了したらフェードインと動画再生を行う
        StartCoroutine(FadeIn());
        videoPlayer.Play();
    }
    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        // 動画が完了したらシーンを遷移
        SceneManager.LoadScene("Title Scene");
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        Color color = prepareImage.color;
        float fadeDuration = 0.3f;  // フェードインの秒数

        // フェードイン処理
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = 1 - (timer / fadeDuration);
            prepareImage.color = color;
            yield return null;
        }

        color.a = 0;
        prepareImage.color = color;
    }
}