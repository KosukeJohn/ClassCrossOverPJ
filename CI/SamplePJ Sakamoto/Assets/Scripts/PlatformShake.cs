using UnityEngine;
using System.Collections;

public class PlatformShake : MonoBehaviour
{
    public float shakeAmount = 2.0f;
    public float shakeDuration = 1.0f;
    private Rigidbody rb;
    private Vector3 originalPosition;
    private bool isShaking = false;
    private bool hasShaken = false;  // �h�ꂽ���ǂ������L�^����ϐ���ǉ�

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        DestroyWhenOutOfStage();
    }

    public void EnableGravity()
    {
        if (rb != null)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX |
                           RigidbodyConstraints.FreezePositionZ |
                           RigidbodyConstraints.FreezeRotation;
        }
    }

    public void DestroyWhenOutOfStage()
    {
        if (transform.position.y < -4f)
        {
            Destroy(gameObject);
        }
    }

    public void StartShake()
    {
        // �h�ꂽ���Ƃ��Ȃ��A���ݗh��Ă��Ȃ��ꍇ�̂ݎ��s
        if (!hasShaken && !isShaking)
        {
            isShaking = true;
            hasShaken = true;  // �h�ꂽ���Ƃ��L�^
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;
            rb.velocity = new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        transform.position = originalPosition;
        isShaking = false;
        EnableGravity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartShake();
        }

        if (collision.gameObject.name == "Gameover Area") 
        {
            Destroy(gameObject);
        }
    }
}