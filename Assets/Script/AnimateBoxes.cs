using UnityEngine;

public class AnimateBoxes1 : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // "AnimationName" はFBXファイル内のアニメーションクリップ名
        animator.Play("BoxesMove");
    }
}
