using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI winText;
    public int winningScore = 7;

    int leftScore;
    int rightScore;
    PongControls controls;

    public bool GameOver { get; private set; }

    void Awake()
    {
        controls = new PongControls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Restart.performed += OnRestart;
    }

    void OnDisable()
    {
        controls.Player.Restart.performed -= OnRestart;
        controls.Player.Disable();
    }

    void Start()
    {
        winText.gameObject.SetActive(false);
        UpdateUI();
    }

    public void PlayerScored()
    {
        leftScore++;
        UpdateUI();
        CheckWin();
    }

    public void AIScored()
    {
        rightScore++;
        UpdateUI();
        CheckWin();
    }

    void CheckWin()
    {
        if (rightScore >= winningScore)
            EndGame("YOU WIN");
        else if (leftScore >= winningScore)
            EndGame("YOU LOSE");
    }

    void EndGame(string message)
    {
        GameOver = true;
        winText.text = message + "\nPress R to restart";
        winText.gameObject.SetActive(true);
    }

    void OnRestart(InputAction.CallbackContext ctx)
    {
        if (GameOver)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI()
    {
        leftText.text = leftScore.ToString();
        rightText.text = rightScore.ToString();
    }
}
