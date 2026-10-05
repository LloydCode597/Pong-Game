using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    public float speed = 10f;
    public float yLimit = 4f; // adjust to your camera bounds

    void Update()
    {
        float input = Input.GetAxisRaw("Vertical"); // W/S or arrow keys
        Vector3 pos = transform.position;
        pos.y += input * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        transform.position = pos;
    }
}
