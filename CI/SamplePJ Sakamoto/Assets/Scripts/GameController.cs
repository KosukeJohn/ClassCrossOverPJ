using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("�}�E�X�J�[�\��")]
    [SerializeField] bool cursor;
    void Start()
    {
        if(!cursor)
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
    }

    void EndGame()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���I��
#else
    Application.Quit();//�Q�[���I��
#endif
        }
    }

}
