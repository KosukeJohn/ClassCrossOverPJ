using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Material : MonoBehaviour
{
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    string targetPropertyName = "_Color";
    [SerializeField]
    Color targetColor_Red = Color.red;
    [SerializeField]
    Color targetColor_White = Color.white;

    int targetPropertyId;
    Material targetMaterial;

    void Start()
    {
        // プロパティ名→IDに変換（高速化）
        targetPropertyId = Shader.PropertyToID(targetPropertyName);

        // メッシュレンダラーからマテリアルを取得
        targetMaterial = meshRenderer.material;
    }

    public void ChangeValueRed()
    {
        // 色変更
        targetMaterial.SetColor(targetPropertyId, targetColor_Red);
    }
    public void ChangeValueWhite()
    {
        // 色変更
        targetMaterial.SetColor(targetPropertyId, targetColor_White);
    }
}
