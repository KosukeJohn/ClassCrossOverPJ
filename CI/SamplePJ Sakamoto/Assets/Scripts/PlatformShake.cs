using UnityEngine;
using System.Collections;

public class PlatformShake : MonoBehaviour
{
    public float shakeAmount = 0.05f;  // 揺れの強さ
    public float shakeDuration = 2.0f;  // 揺れる時間
    Rigidbody rb;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // 回転を防ぐ
    }

    void Update()
    {
        DestroyWhenOutOfStage();
    }

    public void EnableGravity()
    {
        rb.useGravity = true;
    }

    public void DestroyWhenOutOfStage()
    {
        if (transform.position.y < 4f)
        {
            Destroy(gameObject);
        }
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            // Rigidbodyを使って物理的に揺らす
            Vector3 newPosition = originalPosition + new Vector3(x, y, 0);
            rb.MovePosition(newPosition);  // 物理的に位置を動かす

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 揺れが終わったら重力を有効化
        EnableGravity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartShake();
        }

    }
}
