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
    Color targetColor = Color.red;

    int targetPropertyId;
    Material targetMaterial;

    void Start()
    {
        // プロパティ名→IDに変換（高速化）
        targetPropertyId = Shader.PropertyToID(targetPropertyName);

        // メッシュレンダラーからマテリアルを取得
        targetMaterial = meshRenderer.material;
    }

    public void ChangeValue()
    {
        // 色変更
        targetMaterial.SetColor(targetPropertyId, targetColor);
    }
}
