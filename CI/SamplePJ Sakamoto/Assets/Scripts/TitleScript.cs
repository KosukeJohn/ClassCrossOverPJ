using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TitleScript : MonoBehaviour
{
    GameObject targetObject; // ��]����ΏۃI�u�W�F�N�g
    GameObject blackSheet;   // �t�F�[�h�C���Ɏg�p���鍕���摜
    GameObject dialogueObject; // �_�C�A���O�p��UI�I�u�W�F�N�g
    Text dialogueText; // �_�C�A���O��Text�R���|�[�l���g���L���b�V��

    private GameObject player;//�v���C���[�̏����ʒu�����Z�b�g������

    [SerializeField] private AudioSource source;//�I�[�f�B�I
    [SerializeField] private AudioClip clip;//����

    // �\�����镶�́i���Ԃɐݒ�j
    string[] dialogues = { "���ꂳ��u�����ЂÂ��Ă����Ȃ�����[�v", "��l���u�́[���v", "��l���u���[��A�߂�ǂ������ȁ[�v", "�u�|�C���v" };
    int currentDialogueIndex = 0; // ���݂̕��͂̃C���f�b�N�X

    // Start is called before the first frame update
    void Start()
    {
        // �V�[�����Ŗ��O�� "Wooden_Box_Top" �̃I�u�W�F�N�g��T��
        targetObject = GameObject.Find("Wooden_Box_Top");

        // �V�[�����Ŗ��O�� "BlackSheet" �̃I�u�W�F�N�g��T��
        blackSheet = GameObject.Find("BlackSheet");

        // �V�[�����Ŗ��O�� "Dialogue" �̃I�u�W�F�N�g��T��
        dialogueObject = GameObject.Find("Dialogue");

        // �u���b�N�V�[�g�̏����ݒ�
        Image blacksheetimage = blackSheet.GetComponent<Image>();
        if (blacksheetimage != null)
        {
            blacksheetimage.enabled = true; // Image�R���|�[�l���g��L���ɂ���
            blacksheetimage.color = new Color(0, 0, 0, 0); // �����͓���
        }
        else
        {
            Debug.LogError("BlackSheet��Image�R���|�[�l���g���A�^�b�`����Ă��܂���B");
        }

        // �_�C�A���O��Text�R���|�[�l���g���擾���ď����ݒ�
        dialogueText = dialogueObject.GetComponent<Text>();
        if (dialogueText != null)
        {
            dialogueText.enabled = false; // �ŏ��͔�\��
        }
        else
        {
            Debug.LogError("Dialogue��Text�R���|�[�l���g���A�^�b�`����Ă��܂���B");
        }

        //1/7�ǉ�
        player = GameObject.Find("Player");
        Vector3 pos = new(0, 0, 0);
        player.GetComponent<PlayerFirstPos>().SetFirstPos(pos);
    }

    // �{�^���������ꂽ�ۂ̏���
    public void OnJump()/*1/7��Enter->Jump�ɕύX */
    {
        Debug.Log("A�{�^����������܂����I");

        source.clip = clip;
        source.Play();

        // �_�C�A���O�����ɗL���Ȃ玟�̕��͂�\��
        if (dialogueText != null && dialogueText.enabled)
        {
            ShowNextDialogue();
        }
        else
        {
            // �t�F�[�h�C���̌�Ƀ_�C�A���O��\��
            StartCoroutine(StartSequence());

        }
    }

    // ���̕��͂�\������
    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            // �z�񂩂玟�̕��͂��擾
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            Debug.Log("�S�Ă̕��͂��\������܂����B");
            // �t�F�[�h�C��������ɃV�[�������[�h
            SceneManager.LoadScene("mainScene");
        }
    }

    // ��]�ƃt�F�[�h�C�������Ԃɏ�������R���[�`��
    IEnumerator StartSequence()
    {
        if (targetObject != null)
        {
            // ��]����
            yield return RotateAndFadeIn(targetObject, -150, 2f);
        }
        // �t�F�[�h�C������
        yield return FadeInCoroutine(blackSheet, 1f);


        // �t�F�[�h�C��������ɃV�[�������[�h
        SceneManager.LoadScene("mainScene");

        //// �_�C�A���O�̏������͂�\��
        //if (dialogueText != null)
        //{
        //    dialogueText.enabled = true;
        //    ShowNextDialogue();
        //}
    }

    // �ΏۃI�u�W�F�N�g���w��̊p�x�܂ŉ�]������R���[�`��
    IEnumerator RotateAndFadeIn(GameObject obj, float targetXRotation, float duration)
    {
        // ���݂̉�]�p�x���擾
        Vector3 startRotation = obj.transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // �o�ߎ��Ԃ��v�Z
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // ��]���Ԃ��Đݒ�
            obj.transform.eulerAngles = new Vector3(Mathf.Lerp(startRotation.x, targetXRotation, t), startRotation.y, startRotation.z);
            yield return null;
        }

        // ��]�̍ŏI�p�x��ݒ�
        obj.transform.eulerAngles = new Vector3(targetXRotation, startRotation.y, startRotation.z);
    }

    // �����V�[�g���t�F�[�h�C��������R���[�`��
    IEnumerator FadeInCoroutine(GameObject obj, float duration)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("�w�肳�ꂽ�I�u�W�F�N�g��Image�R���|�[�l���g������܂���B");
            yield break;
        }

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // �o�ߎ��Ԃ��v�Z
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // �A���t�@�l����
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        // ���S�ȕs�����ɐݒ�
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }
}

