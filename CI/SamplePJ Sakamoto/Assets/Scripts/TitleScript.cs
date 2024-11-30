using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    GameObject targetObject;
    GameObject blackSheet; // �Ώۂ̃I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        // �V�[�����Ŗ��O�� "Wooden_Box_Top" �̃I�u�W�F�N�g��T��
        targetObject = GameObject.Find("Wooden_Box_Top");

        // �V�[�����Ŗ��O�� "BlackSheet" �̃I�u�W�F�N�g��T��
        blackSheet = GameObject.Find("BlackSheet");

        // �C���[�W�R���|�[�l���g��L�����i�`�F�b�N������j
        Image image = blackSheet.GetComponent<Image>();
        if (image != null)
        {
            image.enabled = true; // Image�R���|�[�l���g��L���ɂ���
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0); // ������Ԃ͓���
        }
        else
        {
            Debug.LogError("BlackSheet��Image�R���|�[�l���g���A�^�b�`����Ă��܂���B");
        }
    }

    public void OnEnter()
    {
        Debug.Log("A�{�^����������܂����I");

        // �I�u�W�F�N�g�����������ꍇ
        if (targetObject != null)
        {
            // 2�b�����ĉ�]��ύX����R���[�`�����J�n
            StartCoroutine(RotateAndFadeIn(targetObject, 150, 2f)); // 2�b�ŉ�]���Ă���t�F�[�h�C��
        }
        else
        {
            Debug.LogError("Wooden_Box_Top �I�u�W�F�N�g��������܂���ł����B");
        }
    }

    IEnumerator RotateAndFadeIn(GameObject obj, float targetXRotation, float duration)
    {
        // ���݂̉�]�p�x
        Vector3 startRotation = obj.transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // �o�ߎ��Ԃ̊������v�Z
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // ��Ԃ��ĉ�]���v�Z
            float newXRotation = Mathf.Lerp(startRotation.x, targetXRotation, t);

            // ���݂̉�]���X�V
            obj.transform.eulerAngles = new Vector3(newXRotation, startRotation.y, startRotation.z);

            yield return null; // ���̃t���[���܂őҋ@
        }

        // �ŏI�I�ɐ��m�Ȋp�x��ݒ�
        obj.transform.eulerAngles = new Vector3(targetXRotation, startRotation.y, startRotation.z);

        Debug.Log("��]�������܂����I");

        // ��]������������t�F�[�h�C�����J�n
        yield return StartCoroutine(FadeInCoroutine(blackSheet, 1f));
    }

    IEnumerator FadeInCoroutine(GameObject obj, float duration)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("�w�肳�ꂽ�I�u�W�F�N�g��Image�R���|�[�l���g������܂���B");
            yield break;
        }

        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1); // �A���t�@�l��1��

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // �A���t�@�l����
            image.color = Color.Lerp(startColor, targetColor, t);

            yield return null; // ���̃t���[���܂őҋ@
        }

        // �ŏI�I�Ɋ��S�ȕs�����ɐݒ�
        image.color = targetColor;

        Debug.Log("�t�F�[�h�C�������I");
    }
}
