using UnityEngine;
using System.Collections;

public class PlatformShake_copy : MonoBehaviour
{
    public float shakeAmount = 2.0f;
    public float shakeDuration = 1.0f;
    private Rigidbody rb;
    private Light redLight;
    private Vector3 originalPosition;
    private bool isShaking = false;
    private bool hasShaken = false;  // 揺れたかどうかを記録する変数を追加
    bool AtkSEPlayed = false;
    private GameObject fall;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        fall = transform.parent.gameObject;
        redLight = transform.GetChild(0).GetComponent<Light>();
        redLight.enabled = false;
    }

    void Update()
    {
        if (fall.GetComponent<fallFlag>().GetFallFlag())
        {
            redLight.enabled = true;
            StartShake();
        }

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
            source.clip = clip;
            source.Play();
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
        if (collision.gameObject.name == "Gameover Area") 
        {
            Destroy(gameObject);
        }
    }
}