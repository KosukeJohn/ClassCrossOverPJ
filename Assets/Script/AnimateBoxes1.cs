using UnityEngine;

public class AnimateBoxes : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // "AnimationName" はFBXファイル内のアニメーションクリップ名
        animator.Play("BoxesJump");
    }
}
