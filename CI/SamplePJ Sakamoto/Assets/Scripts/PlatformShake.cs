using UnityEngine;
using System.Collections;

public class PlatformShake : MonoBehaviour
{
    public float shakeAmount = 2.0f;
    public float shakeDuration = 1.0f;
    private Rigidbody rb;
    private Vector3 originalPosition;
    private bool isShaking = false;
    private bool hasShaken = false;  // óhÇÍÇΩÇ©Ç«Ç§Ç©ÇãLò^Ç∑ÇÈïœêîÇí«â¡

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
        // óhÇÍÇΩÇ±Ç∆Ç™Ç»Ç≠ÅAåªç›óhÇÍÇƒÇ¢Ç»Ç¢èÍçáÇÃÇ›é¿çs
        if (!hasShaken && !isShaking)
        {
            isShaking = true;
            hasShaken = true;  // óhÇÍÇΩÇ±Ç∆ÇãLò^
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