using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("マウスカーソル")]
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
            UnityEditor.EditorApplication.isPlaying = false;//ゲーム終了
#else
    Application.Quit();//ゲーム終了
#endif
        }
    }

}
