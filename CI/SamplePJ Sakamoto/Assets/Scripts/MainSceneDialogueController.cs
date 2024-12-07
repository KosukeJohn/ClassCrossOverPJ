using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainSceneDialogueController : MonoBehaviour
{
    GameObject dialogueObject; // ダイアログ用のUIオブジェクト
    Text dialogueText; // ダイアログのTextコンポーネントをキャッシュ

    // 表示する文章のリスト
    private string[] dialogues = {
        "ようこそ、冒険者！",
        "この地には数多くの試練が待っています。",
        "準備はできていますか？",
        "さあ、冒険を始めましょう！"
    };

    private int currentDialogueIndex = 0; // 現在の文章のインデックス

    // Start is called before the first frame update
    void Start()
    {
        // シーン内で名前が "Dialogue" のオブジェクトを探す
        dialogueObject = GameObject.Find("Dialogue");

        // ダイアログのTextコンポーネントを取得して初期設定
        dialogueText = dialogueObject.GetComponent<Text>();
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void onEnter()
    {
        Debug.Log("Aボタンが押されました！");


        AdvanceDialogue();
    }

    // 次のダイアログに進む
    void AdvanceDialogue()
    {
        if (dialogueText != null && currentDialogueIndex < dialogues.Length - 1)
        {
            currentDialogueIndex++; // インデックスを進める
            dialogueText.text = dialogues[currentDialogueIndex]; // 次の文章を表示
        }
        else
        {
            // ダイアログが終了したら非表示にする
            dialogueText.enabled = false;
            Debug.Log("すべてのダイアログが終了しました。ダイアログを非表示にしました。");
        }
    }
}
