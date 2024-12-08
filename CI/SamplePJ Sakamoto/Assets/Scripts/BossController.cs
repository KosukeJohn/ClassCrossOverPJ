using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform���i�[����ϐ�
    public float speed = 2.0f; // �{�X���ǐՂ��鑬�x

    void Start()
    {
        // �v���C���[��"Player"�^�O�Ō������ATransform���擾
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player�I�u�W�F�N�g��������܂���ł����B");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // �v���C���[�̕���������
            Vector3 direction = (player.position - transform.position).normalized;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            // �v���C���[�Ɍ������Ĉړ�
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
