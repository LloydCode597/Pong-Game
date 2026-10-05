using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public Transform ball;
    public float speed = 6f; // keep this LOWER than player speed so it's fair
    public float yLimit = 4f;
    public float reactionDeadzone = 0.3f; // prevents jittery micro-movements

    void Update()
    {
        float diff = ball.position.y - transform.position.y;
        if (Mathf.Abs(diff) > reactionDeadzone)
        {
            float dir = Mathf.Sign(diff);
            Vector3 pos = transform.position;
            pos.y += dir * speed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
            transform.position = pos;
        }
    }
}