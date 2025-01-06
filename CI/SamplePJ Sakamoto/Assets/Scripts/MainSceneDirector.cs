using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneDirector : MonoBehaviour
{
    GameObject blackSheet;

    // Start is called before the first frame update
    void Start()
    {
        // シーン内で名前が "BlackSheet" のオブジェクトを探す
        blackSheet = GameObject.Find("BlackSheet");

        if (blackSheet != null)
        {
            // イメージコンポーネントを取得して有効化
            Image image = blackSheet.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = true; // イメージを有効化
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1); // 初期状態を完全不透明
                StartCoroutine(FadeOutCoroutine(image, 2f)); // 2秒かけてフェードアウト
            }
            else
            {
                Debug.LogError("BlackSheet に Image コンポーネントがアタッチされていません。");
            }
        }
        else
        {
            Debug.LogError("BlackSheet オブジェクトが見つかりませんでした。");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeOutCoroutine(Image image, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0); // アルファ値を0に

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // アルファ値を補間
            image.color = Color.Lerp(startColor, targetColor, t);

            yield return null; // 次のフレームまで待機
        }

        // 最終的に完全に透明に設定
        image.color = targetColor;
        Debug.Log("フェードアウト完了！");
    }
}
