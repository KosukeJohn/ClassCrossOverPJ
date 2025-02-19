using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Stage1EnemyContllore : MonoBehaviour
{
    private GameObject enemy;
    private Collider coll;
    private GameObject Player;
    private bool changeSceneFlag;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float PlayerSetPosition;
    [SerializeField] private float ChangeScenePos;
    [SerializeField] private float EnemyInstancePos;
    [SerializeField] private float EndStage1Pos;

    private void Start()
    {
        Player = GameObject.Find("Player");
        coll = GetComponent<Collider>();
        changeSceneFlag = false;
        coll.enabled = false;
    }

    private void Update()
    {
        if (Player.transform.position.x < ChangeScenePos) 
        {
            if (!changeSceneFlag) changeSceneFlag = true;
        }

        if (Player.transform.position.x >= ChangeScenePos)
        {
            if (changeSceneFlag) 
            {
                Vector3 pos = new Vector3(PlayerSetPosition, 0, 4.36000013f);
                Player.GetComponent<PlayerFirstPos>().SetFirstPos(pos);

                changeSceneFlag = false;

                SceneManager.LoadScene("BooksScene");
                return;
            }
        }

        if (Player.transform.position.x >= PlayerSetPosition)
        {
            coll.enabled = true;

            if (!enemy)
            {
                //アタッチした敵を生成
                enemy = Instantiate(enemyPrefab);

                //箱の中心かつ地面の上に生成
                Vector3 pos = new Vector3(EnemyInstancePos,0, 4.36000013f);
                enemy.transform.position = new Vector3(pos.x, 0, pos.z);
            }
        }

        if (Player.transform.position.x >= EndStage1Pos)
        {
            if (enemy)
            {
                Destroy(enemy);
            }
        }
    }
}
