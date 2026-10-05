using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 8f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    void Launch()
    {
        float xDir = Random.value < 0.5f ? -1f : 1f;
        float yDir = Random.Range(-0.5f, 0.5f);
        rb.linearVelocity = new Vector2(xDir, yDir).normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreLeft") || other.CompareTag("ScoreRight"))
        {
            // TODO: call your ScoreManager here
            transform.position = Vector3.zero;
            Launch();
        }
    }
}