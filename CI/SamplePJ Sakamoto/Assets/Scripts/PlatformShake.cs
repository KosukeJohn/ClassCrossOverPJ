using UnityEngine;
using System.Collections;

public class PlatformShake : MonoBehaviour
{
    public float shakeAmount = 0.05f;  // �h��̋���
    public float shakeDuration = 2.0f;  // �h��鎞��
    Rigidbody rb;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // ��]��h��
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

            // Rigidbody���g���ĕ����I�ɗh�炷
            Vector3 newPosition = originalPosition + new Vector3(x, y, 0);
            rb.MovePosition(newPosition);  // �����I�Ɉʒu�𓮂���

            elapsed += Time.deltaTime;
            yield return null;
        }

        // �h�ꂪ�I�������d�͂�L����
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
