using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour
{
    public float speed = 10f;
    public float yLimit = 4f;

    PongControls controls;
    float moveInput;

    void Awake()
    {
        controls = new PongControls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<float>();
        controls.Player.Move.canceled += ctx => moveInput = 0f;
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += moveInput * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        transform.position = pos;
    }
}