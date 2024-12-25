using UnityEngine;
using System.Collections;

public class PlatformShake : MonoBehaviour
{
    public float shakeAmount = 0.05f;  // —h‚ê‚Ì‹­‚³
    public float shakeDuration = 2.0f;  // —h‚ê‚éŽžŠÔ
    Rigidbody rb;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // ‰ñ“]‚ð–h‚®
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

            // Rigidbody‚ðŽg‚Á‚Ä•¨—“I‚É—h‚ç‚·
            Vector3 newPosition = originalPosition + new Vector3(x, y, 0);
            rb.MovePosition(newPosition);  // •¨—“I‚ÉˆÊ’u‚ð“®‚©‚·

            elapsed += Time.deltaTime;
            yield return null;
        }

        // —h‚ê‚ªI‚í‚Á‚½‚çd—Í‚ð—LŒø‰»
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
