using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    GameObject targetObject;
    GameObject blackSheet; // 対象のオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        // シーン内で名前が "Wooden_Box_Top" のオブジェクトを探す
        targetObject = GameObject.Find("Wooden_Box_Top");

        // シーン内で名前が "BlackSheet" のオブジェクトを探す
        blackSheet = GameObject.Find("BlackSheet");

        // イメージコンポーネントを有効化（チェックを入れる）
        Image image = blackSheet.GetComponent<Image>();
        if (image != null)
        {
            image.enabled = true; // Imageコンポーネントを有効にする
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0); // 初期状態は透明
        }
        else
        {
            Debug.LogError("BlackSheetにImageコンポーネントがアタッチされていません。");
        }
    }

    public void OnEnter()
    {
        Debug.Log("Aボタンが押されました！");

        // オブジェクトが見つかった場合
        if (targetObject != null)
        {
            // 2秒かけて回転を変更するコルーチンを開始
            StartCoroutine(RotateAndFadeIn(targetObject, 150, 2f)); // 2秒で回転してからフェードイン
        }
        else
        {
            Debug.LogError("Wooden_Box_Top オブジェクトが見つかりませんでした。");
        }
    }

    IEnumerator RotateAndFadeIn(GameObject obj, float targetXRotation, float duration)
    {
        // 現在の回転角度
        Vector3 startRotation = obj.transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 経過時間の割合を計算
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 補間して回転を計算
            float newXRotation = Mathf.Lerp(startRotation.x, targetXRotation, t);

            // 現在の回転を更新
            obj.transform.eulerAngles = new Vector3(newXRotation, startRotation.y, startRotation.z);

            yield return null; // 次のフレームまで待機
        }

        // 最終的に正確な角度を設定
        obj.transform.eulerAngles = new Vector3(targetXRotation, startRotation.y, startRotation.z);

        Debug.Log("回転完了しました！");

        // 回転が完了したらフェードインを開始
        yield return StartCoroutine(FadeInCoroutine(blackSheet, 1f));
    }

    IEnumerator FadeInCoroutine(GameObject obj, float duration)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("指定されたオブジェクトにImageコンポーネントがありません。");
            yield break;
        }

        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1); // アルファ値を1に

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // アルファ値を補間
            image.color = Color.Lerp(startColor, targetColor, t);

            yield return null; // 次のフレームまで待機
        }

        // 最終的に完全な不透明に設定
        image.color = targetColor;

        Debug.Log("フェードイン完了！");
    }
}
