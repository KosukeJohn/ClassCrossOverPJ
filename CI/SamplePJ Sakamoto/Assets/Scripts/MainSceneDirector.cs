using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneDirector : MonoBehaviour
{
    GameObject blackSheet;

    // Start is called before the first frame update
    void Start()
    {
        // �V�[�����Ŗ��O�� "BlackSheet" �̃I�u�W�F�N�g��T��
        blackSheet = GameObject.Find("BlackSheet");

        if (blackSheet != null)
        {
            // �C���[�W�R���|�[�l���g���擾���ėL����
            Image image = blackSheet.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = true; // �C���[�W��L����
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1); // ������Ԃ����S�s����
                StartCoroutine(FadeOutCoroutine(image, 2f)); // 2�b�����ăt�F�[�h�A�E�g
            }
            else
            {
                Debug.LogError("BlackSheet �� Image �R���|�[�l���g���A�^�b�`����Ă��܂���B");
            }
        }
        else
        {
            Debug.LogError("BlackSheet �I�u�W�F�N�g��������܂���ł����B");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeOutCoroutine(Image image, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0); // �A���t�@�l��0��

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // �A���t�@�l����
            image.color = Color.Lerp(startColor, targetColor, t);

            yield return null; // ���̃t���[���܂őҋ@
        }

        // �ŏI�I�Ɋ��S�ɓ����ɐݒ�
        image.color = targetColor;
        Debug.Log("�t�F�[�h�A�E�g�����I");
    }
}
