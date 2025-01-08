using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーとUIの座標を取得
        Vector3 Plpos=Player.transform.position; 
        Vector3 UIPos=UI.transform.position;
        //プレイヤーとUIの距離を取得
        float dis=Vector3.Distance(Plpos, UIPos);
        //プレイヤーとUIの距離が一定以下ならUIを非表示
        if(dis<10.5f)
        {
            UI.SetActive(true);
        }
        //プレイヤーとUIの距離が一定上ならUIを表示
        if (dis>20.0f)
        {
            UI.SetActive(false);
        }
    }
}
