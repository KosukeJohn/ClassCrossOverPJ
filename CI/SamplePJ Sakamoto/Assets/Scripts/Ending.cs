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
        // �ǂݍ��݂܂ŉ��ŕ\�����Ă������w�i
        prepareImage = GameObject.Find("PrepareImage").GetComponent<Image>();

        // ���揀��
        videoPlayer = GameObject.Find("videoPlayer").GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "ToyBox_Endimg.mp4");
        videoPlayer.Prepare();

        // ����̏����������̃C�x���g��o�^
        videoPlayer.prepareCompleted += OnPrepareCompleted;
        // ����̍Đ��I�����̃C�x���g��o�^
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    private void Update()
    {
        // pass
    }

    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        // ��������������t�F�[�h�C���Ɠ���Đ����s��
        StartCoroutine(FadeIn());
        videoPlayer.Play();
    }
    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        // ���悪����������V�[����J��
        SceneManager.LoadScene("Title Scene");
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        Color color = prepareImage.color;
        float fadeDuration = 0.3f;  // �t�F�[�h�C���̕b��

        // �t�F�[�h�C������
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