using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI leftText;    // shows the AI's score (left side)
    public TextMeshProUGUI rightText;   // shows the player's score (right side)
    public TextMeshProUGUI winText;
    public int winningScore = 5;

    int playerScore;
    int aiScore;
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
        playerScore++;
        UpdateUI();
        CheckWin();
    }

    public void AIScored()
    {
        aiScore++;
        UpdateUI();
        CheckWin();
    }

    void CheckWin()
    {
        if (playerScore >= winningScore)
            EndGame("YOU WIN");
        else if (aiScore >= winningScore)
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
        leftText.text = aiScore.ToString();
        rightText.text = playerScore.ToString();
    }
}