using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainSceneDialogueController : MonoBehaviour
{
    GameObject dialogueObject; // �_�C�A���O�p��UI�I�u�W�F�N�g
    Text dialogueText; // �_�C�A���O��Text�R���|�[�l���g���L���b�V��

    // �\�����镶�͂̃��X�g
    private string[] dialogues = {
        "�悤�����A�`���ҁI",
        "���̒n�ɂ͐������̎������҂��Ă��܂��B",
        "�����͂ł��Ă��܂����H",
        "�����A�`�����n�߂܂��傤�I"
    };

    private int currentDialogueIndex = 0; // ���݂̕��͂̃C���f�b�N�X

    // Start is called before the first frame update
    void Start()
    {
        // �V�[�����Ŗ��O�� "Dialogue" �̃I�u�W�F�N�g��T��
        dialogueObject = GameObject.Find("Dialogue");

        // �_�C�A���O��Text�R���|�[�l���g���擾���ď����ݒ�
        dialogueText = dialogueObject.GetComponent<Text>();
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void onEnter()
    {
        Debug.Log("A�{�^����������܂����I");


        AdvanceDialogue();
    }

    // ���̃_�C�A���O�ɐi��
    void AdvanceDialogue()
    {
        if (dialogueText != null && currentDialogueIndex < dialogues.Length - 1)
        {
            currentDialogueIndex++; // �C���f�b�N�X��i�߂�
            dialogueText.text = dialogues[currentDialogueIndex]; // ���̕��͂�\��
        }
        else
        {
            // �_�C�A���O���I���������\���ɂ���
            dialogueText.enabled = false;
            Debug.Log("���ׂẴ_�C�A���O���I�����܂����B�_�C�A���O���\���ɂ��܂����B");
        }
    }
}
