using UnityEngine;

public class AnimateBoxes1 : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // "AnimationName" ��FBX�t�@�C�����̃A�j���[�V�����N���b�v��
        animator.Play("BoxesMove");
    }
}