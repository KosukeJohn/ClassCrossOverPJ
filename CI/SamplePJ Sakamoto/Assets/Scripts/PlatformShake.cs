using UnityEngine;
using System.Collections;

public class PlatformShake : MonoBehaviour
{
    public float shakeAmount = 2.0f;
    public float shakeDuration = 1.0f;
    private Rigidbody rb;
    private Vector3 originalPosition;
    private bool isShaking = false;
    private bool hasShaken = false;  // 揺れたかどうかを記録する変数を追加
   [SerializeField] AudioSource shakeSound;
    bool SEplay = false;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        shakeSound=GetComponent<AudioSource>();
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        DestroyWhenOutOfStage();
        if (SEplay)
        {
            shakeSound.Play();
        }
    }

    public void EnableGravity()
    {
        if (rb != null)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX |
                           RigidbodyConstraints.FreezePositionZ |
                           RigidbodyConstraints.FreezeRotation;
            SEplay = true;
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
        // 揺れたことがなく、現在揺れていない場合のみ実行
        if (!hasShaken && !isShaking)
        {
            isShaking = true;
            hasShaken = true;  // 揺れたことを記録
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