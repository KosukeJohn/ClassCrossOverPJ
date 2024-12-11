using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player; // プレイヤーのTransformを格納する変数
    public float speed = 2.0f; // ボスが追跡する速度

    void Start()
    {
        // プレイヤーを"Player"タグで検索し、Transformを取得
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Playerオブジェクトが見つかりませんでした。");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // プレイヤーの方向を向く
            Vector3 direction = (player.position - transform.position).normalized;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            // プレイヤーに向かって移動
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
