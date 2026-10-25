using System.Collections;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [Header("Speed")]
    public float baseSpeed = 8f;
    public float speedIncrease = 1f;
    public float maxSpeed = 18f;

    [Header("Bounce")]
    public float maxBounceAngle = 60f;

    [Header("Serve")]
    public TextMeshProUGUI countdownText;
    public int countdownFrom = 3;
    public float countdownStep = 0.5f;

    [Header("References")]
    public ScoreManager scoreManager;

    [Header("Audio Clips")]
    public AudioClip paddleHitClip;
    public AudioClip wallHitClip;

    [Header("Volumes")]
    [Range(0f, 1f)] public float paddleVolume = 0.6f;
    [Range(0f, 1f)] public float wallVolume = 0.5f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    TrailRenderer trail;
    AudioSource audioSource;
    float currentSpeed;
    bool serving;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ServeRoutine());
    }

    IEnumerator ServeRoutine()
    {
        serving = true;
        ResetBall();

        for (int i = countdownFrom; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(countdownStep);
        }

        countdownText.text = "";
        Launch();
        serving = false;
    }

    void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = Vector2.zero;
        transform.position = Vector3.zero;

        currentSpeed = baseSpeed;
        sr.color = Color.white;

        if (trail != null) trail.Clear();   // removes the streak back to center
    }

    void Launch()
    {
        float xDir = Random.value < 0.5f ? -1f : 1f;
        float yDir = Random.Range(-0.5f, 0.5f);
        rb.linearVelocity = new Vector2(xDir, yDir).normalized * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (serving) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            PlaySound(wallHitClip, wallVolume, Random.Range(0.95f, 1.05f));
            return;
        }

        if (!collision.gameObject.CompareTag("Paddle")) return;

        Transform paddle = collision.transform;
        float halfHeight = collision.collider.bounds.size.y / 2f;
        float offset = Mathf.Clamp((transform.position.y - paddle.position.y) / halfHeight, -1f, 1f);
        float xDir = Mathf.Sign(transform.position.x - paddle.position.x);

        float angle = offset * maxBounceAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(xDir * Mathf.Cos(angle), Mathf.Sin(angle));

        currentSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);
        rb.linearVelocity = dir * currentSpeed;

        float t = (currentSpeed - baseSpeed) / (maxSpeed - baseSpeed);
        sr.color = Color.Lerp(Color.white, Color.red, t);

        PlaySound(paddleHitClip, paddleVolume, Mathf.Lerp(1f, 1.3f, t));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (serving) return;

        if (other.CompareTag("ScoreLeft"))
            scoreManager.AIScored();
        else if (other.CompareTag("ScoreRight"))
            scoreManager.PlayerScored();
        else
            return;

        if (scoreManager.GameOver)
        {
            ResetBall();
            return;
        }

        StartCoroutine(ServeRoutine());
    }

    void PlaySound(AudioClip clip, float volume, float pitch = 1f)
    {
        if (clip == null) return;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip, volume);
    }
}