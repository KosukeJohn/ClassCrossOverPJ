using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneDialogueController : MonoBehaviour
{
    [Header("ダイアログ設定")]
    public GameObject dialogueObject; // ダイアログ用のUIオブジェクト
    private Text dialogueText; // ダイアログのTextコンポーネントをキャッシュ

    [Header("表示する文章")]
    private string[] dialogues = {
        "主人公「僕人形になってる！」",
        "主人公「蝶ネクタイがない・・・」",
        "マリオネット「僕らを大切にしなかったから君は人形になったんだ！」",
        "マリオネット「人間に戻りたかったら、取りにおいで！」",
        "主人公「とりあえず進もう」"
    };

    private int currentDialogueIndex = 0; // 現在の文章のインデックス
    private Coroutine dialogueCoroutine; // ダイアログ進行用のコルーチン

    // Start is called before the first frame update
    void Start()
    {
        // シーン内で名前が "Dialogue" のオブジェクトを探す
        dialogueObject = GameObject.Find("Dialogue");

        // ダイアログのTextコンポーネントを取得
        if (dialogueObject != null)
        {
            dialogueText = dialogueObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogError("Dialogueオブジェクトが見つかりません。");
            return;
        }

        // ダイアログの初期文章を設定して表示
        if (dialogueText != null)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }

        // 1秒ごとにテキストを進めるコルーチンを開始
        dialogueCoroutine = StartCoroutine(AdvanceDialogueAutomatically());
    }

    // ダイアログを自動的に進めるコルーチン
    IEnumerator AdvanceDialogueAutomatically()
    {
        while (currentDialogueIndex < dialogues.Length - 1)
        {
            yield return new WaitForSeconds(2f); // 1秒待機
            currentDialogueIndex++; // インデックスを進める
            dialogueText.text = dialogues[currentDialogueIndex]; // 次の文章を表示
        }

        // 最後のダイアログが終了したら非表示にする
        yield return new WaitForSeconds(1f);
        dialogueText.enabled = false;
        Debug.Log("すべてのダイアログが終了しました。ダイアログを非表示にしました。");
    }

    // 手動でダイアログを進める場合（ボタンイベント等で使用）
    public void OnEnter()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine); // 自動進行を停止
            dialogueCoroutine = null;
        }

        AdvanceDialogueManually();
    }

    // 手動で次のダイアログに進む
    void AdvanceDialogueManually()
    {
        if (currentDialogueIndex < dialogues.Length - 1)
        {
            currentDialogueIndex++; // インデックスを進める
            dialogueText.text = dialogues[currentDialogueIndex]; // 次の文章を表示
        }
        else
        {
            dialogueText.enabled = false; // ダイアログを非表示にする
            Debug.Log("すべてのダイアログが終了しました。");
        }
    }
}
