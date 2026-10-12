using UnityEngine;

public class Ball : MonoBehaviour
{
    int rallyHits;

    [Header("Speed")]
    public float baseSpeed = 8f;
    public float speedIncrease = 0.5f;   // added per paddle hit
    public float maxSpeed = 18f;

    [Header("Bounce")]
    public float maxBounceAngle = 60f;   // degrees from horizontal at paddle edge

    public ScoreManager scoreManager;

    Rigidbody2D rb;
    float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    void Launch()
    {
        currentSpeed = baseSpeed;
        float xDir = Random.value < 0.5f ? -1f : 1f;
        float yDir = Random.Range(-0.5f, 0.5f);
        rb.linearVelocity = new Vector2(xDir, yDir).normalized * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Paddle")) return;

        Transform paddle = collision.transform;

        // -1 (bottom edge) to +1 (top edge)
        float halfHeight = collision.collider.bounds.size.y / 2f;
        float offset = Mathf.Clamp((transform.position.y - paddle.position.y) / halfHeight, -1f, 1f);

        // Always send the ball away from the paddle it just hit
        float xDir = Mathf.Sign(transform.position.x - paddle.position.x);

        float angle = (offset * maxBounceAngle + Random.Range(-5f, 5f)) * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(xDir * Mathf.Cos(angle), Mathf.Sin(angle));

        currentSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);
        rb.linearVelocity = dir * currentSpeed;

        rallyHits++;
        Debug.Log($"Hit {rallyHits} | speed {currentSpeed:F1}");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreLeft"))
            scoreManager.AIScored();
        else if (other.CompareTag("ScoreRight"))
            scoreManager.PlayerScored();
        else
            return;

        transform.position = Vector3.zero;
        rallyHits = 0; // reset rally hits
        Launch(); // also resets currentSpeed
    }
}