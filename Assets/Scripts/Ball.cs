using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Speed")]
    public float baseSpeed = 8f;
    public float speedIncrease = 1f;
    public float maxSpeed = 18f;

    [Header("Bounce")]
    public float maxBounceAngle = 60f;

    [Header("References")]
    public ScoreManager scoreManager;

    [Header("Audio")]
    public AudioClip paddleHitClip;
    public AudioClip wallHitClip;
    public AudioClip scoreClip;

    Rigidbody2D rb;
    SpriteRenderer sr;
    AudioSource audioSource;
    float currentSpeed;
    int rallyHits;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        Launch();
    }

    void Launch()
    {
        currentSpeed = baseSpeed;
        rallyHits = 0;
        sr.color = Color.white;

        float xDir = Random.value < 0.5f ? -1f : 1f;
        float yDir = Random.Range(-0.5f, 0.5f);
        rb.linearVelocity = new Vector2(xDir, yDir).normalized * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlaySound(wallHitClip, Random.Range(0.95f, 1.05f));
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

        rallyHits++;
        float t = (currentSpeed - baseSpeed) / (maxSpeed - baseSpeed);
        sr.color = Color.Lerp(Color.white, Color.red, t);

        PlaySound(paddleHitClip, Mathf.Lerp(1f, 1.3f, t));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreLeft"))
        {
            scoreManager.AIScored();
            PlaySound(scoreClip, 0.85f);   // lower = bad news
        }
        else if (other.CompareTag("ScoreRight"))
        {
            scoreManager.PlayerScored();
            PlaySound(scoreClip, 1.15f);   // higher = good news
        }
        else
            return;

        transform.position = Vector3.zero;

        if (scoreManager.GameOver)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Launch();
    }

    void PlaySound(AudioClip clip, float pitch = 1f)
    {
        if (clip == null) return;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip);
    }
}