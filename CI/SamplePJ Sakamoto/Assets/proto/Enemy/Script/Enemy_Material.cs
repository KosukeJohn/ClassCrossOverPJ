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
        // �v���p�e�B����ID�ɕϊ��i�������j
        targetPropertyId = Shader.PropertyToID(targetPropertyName);

        // ���b�V�������_���[����}�e���A�����擾
        targetMaterial = meshRenderer.material;
    }

    public void ChangeValue()
    {
        // �F�ύX
        targetMaterial.SetColor(targetPropertyId, targetColor);
    }
}
