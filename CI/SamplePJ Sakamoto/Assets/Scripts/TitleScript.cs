using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TitleScript : MonoBehaviour
{
    GameObject targetObject; // 回転する対象オブジェクト
    GameObject blackSheet;   // フェードインに使用する黒い画像
    GameObject dialogueObject; // ダイアログ用のUIオブジェクト
    Text dialogueText; // ダイアログのTextコンポーネントをキャッシュ

    private GameObject player;//プレイヤーの初期位置をリセットさせる

    [SerializeField] private AudioSource source;//オーディオ
    [SerializeField] private AudioClip clip;//音源

    // 表示する文章（順番に設定）
    string[] dialogues = { "お母さん「部屋片づけておきなさいよー」", "主人公「はーい」", "主人公「うーん、めんどくさいなー」", "「ポイっ」" };
    int currentDialogueIndex = 0; // 現在の文章のインデックス

    // Start is called before the first frame update
    void Start()
    {
        // シーン内で名前が "Wooden_Box_Top" のオブジェクトを探す
        targetObject = GameObject.Find("Wooden_Box_Top");

        // シーン内で名前が "BlackSheet" のオブジェクトを探す
        blackSheet = GameObject.Find("BlackSheet");

        // シーン内で名前が "Dialogue" のオブジェクトを探す
        dialogueObject = GameObject.Find("Dialogue");

        // ブラックシートの初期設定
        Image blacksheetimage = blackSheet.GetComponent<Image>();
        if (blacksheetimage != null)
        {
            blacksheetimage.enabled = true; // Imageコンポーネントを有効にする
            blacksheetimage.color = new Color(0, 0, 0, 0); // 初期は透明
        }
        else
        {
            Debug.LogError("BlackSheetにImageコンポーネントがアタッチされていません。");
        }

        // ダイアログのTextコンポーネントを取得して初期設定
        dialogueText = dialogueObject.GetComponent<Text>();
        if (dialogueText != null)
        {
            dialogueText.enabled = false; // 最初は非表示
        }
        else
        {
            Debug.LogError("DialogueにTextコンポーネントがアタッチされていません。");
        }

        //1/7追加
        player = GameObject.Find("Player");
        Vector3 pos = new(0, 0, 0);
        player.GetComponent<PlayerFirstPos>().SetFirstPos(pos);
    }

    // ボタンが押された際の処理
    public void OnJump()/*1/7にEnter->Jumpに変更 */
    {
        Debug.Log("Aボタンが押されました！");

        source.clip = clip;
        source.Play();

        // ダイアログが既に有効なら次の文章を表示
        if (dialogueText != null && dialogueText.enabled)
        {
            ShowNextDialogue();
        }
        else
        {
            // フェードインの後にダイアログを表示
            StartCoroutine(StartSequence());

        }
    }

    // 次の文章を表示する
    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            // 配列から次の文章を取得
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            Debug.Log("全ての文章が表示されました。");
            // フェードイン完了後にシーンをロード
            SceneManager.LoadScene("mainScene");
        }
    }

    // 回転とフェードインを順番に処理するコルーチン
    IEnumerator StartSequence()
    {
        if (targetObject != null)
        {
            // 回転処理
            yield return RotateAndFadeIn(targetObject, -150, 2f);
        }
        // フェードイン処理
        yield return FadeInCoroutine(blackSheet, 1f);


        // フェードイン完了後にシーンをロード
        SceneManager.LoadScene("mainScene");

        //// ダイアログの初期文章を表示
        //if (dialogueText != null)
        //{
        //    dialogueText.enabled = true;
        //    ShowNextDialogue();
        //}
    }

    // 対象オブジェクトを指定の角度まで回転させるコルーチン
    IEnumerator RotateAndFadeIn(GameObject obj, float targetXRotation, float duration)
    {
        // 現在の回転角度を取得
        Vector3 startRotation = obj.transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 経過時間を計算
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 回転を補間して設定
            obj.transform.eulerAngles = new Vector3(Mathf.Lerp(startRotation.x, targetXRotation, t), startRotation.y, startRotation.z);
            yield return null;
        }

        // 回転の最終角度を設定
        obj.transform.eulerAngles = new Vector3(targetXRotation, startRotation.y, startRotation.z);
    }

    // 黒いシートをフェードインさせるコルーチン
    IEnumerator FadeInCoroutine(GameObject obj, float duration)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("指定されたオブジェクトにImageコンポーネントがありません。");
            yield break;
        }

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // 経過時間を計算
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // アルファ値を補間
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        // 完全な不透明に設定
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }
}

