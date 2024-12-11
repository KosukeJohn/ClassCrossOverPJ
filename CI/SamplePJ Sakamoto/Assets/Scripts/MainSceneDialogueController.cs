using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneDialogueController : MonoBehaviour
{
    [Header("�_�C�A���O�ݒ�")]
    public GameObject dialogueObject; // �_�C�A���O�p��UI�I�u�W�F�N�g
    private Text dialogueText; // �_�C�A���O��Text�R���|�[�l���g���L���b�V��

    [Header("�\�����镶��")]
    private string[] dialogues = {
        "��l���u�l�l�`�ɂȂ��Ă�I�v",
        "��l���u���l�N�^�C���Ȃ��E�E�E�v",
        "�}���I�l�b�g�u�l����؂ɂ��Ȃ���������N�͐l�`�ɂȂ����񂾁I�v",
        "�}���I�l�b�g�u�l�Ԃɖ߂肽��������A���ɂ����ŁI�v",
        "��l���u�Ƃ肠�����i�����v"
    };

    private int currentDialogueIndex = 0; // ���݂̕��͂̃C���f�b�N�X
    private Coroutine dialogueCoroutine; // �_�C�A���O�i�s�p�̃R���[�`��

    // Start is called before the first frame update
    void Start()
    {
        // �V�[�����Ŗ��O�� "Dialogue" �̃I�u�W�F�N�g��T��
        dialogueObject = GameObject.Find("Dialogue");

        // �_�C�A���O��Text�R���|�[�l���g���擾
        if (dialogueObject != null)
        {
            dialogueText = dialogueObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogError("Dialogue�I�u�W�F�N�g��������܂���B");
            return;
        }

        // �_�C�A���O�̏������͂�ݒ肵�ĕ\��
        if (dialogueText != null)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }

        // 1�b���ƂɃe�L�X�g��i�߂�R���[�`�����J�n
        dialogueCoroutine = StartCoroutine(AdvanceDialogueAutomatically());
    }

    // �_�C�A���O�������I�ɐi�߂�R���[�`��
    IEnumerator AdvanceDialogueAutomatically()
    {
        while (currentDialogueIndex < dialogues.Length - 1)
        {
            yield return new WaitForSeconds(2f); // 1�b�ҋ@
            currentDialogueIndex++; // �C���f�b�N�X��i�߂�
            dialogueText.text = dialogues[currentDialogueIndex]; // ���̕��͂�\��
        }

        // �Ō�̃_�C�A���O���I���������\���ɂ���
        yield return new WaitForSeconds(1f);
        dialogueText.enabled = false;
        Debug.Log("���ׂẴ_�C�A���O���I�����܂����B�_�C�A���O���\���ɂ��܂����B");
    }

    // �蓮�Ń_�C�A���O��i�߂�ꍇ�i�{�^���C�x���g���Ŏg�p�j
    public void OnEnter()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine); // �����i�s���~
            dialogueCoroutine = null;
        }

        AdvanceDialogueManually();
    }

    // �蓮�Ŏ��̃_�C�A���O�ɐi��
    void AdvanceDialogueManually()
    {
        if (currentDialogueIndex < dialogues.Length - 1)
        {
            currentDialogueIndex++; // �C���f�b�N�X��i�߂�
            dialogueText.text = dialogues[currentDialogueIndex]; // ���̕��͂�\��
        }
        else
        {
            dialogueText.enabled = false; // �_�C�A���O���\���ɂ���
            Debug.Log("���ׂẴ_�C�A���O���I�����܂����B");
        }
    }
}
