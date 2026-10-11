using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    int leftScore;
    int rightScore;

    void Start()
    {
        UpdateUI();
    }

    public void PlayerScored()
    {
        leftScore++;
        UpdateUI();
    }

    public void AIScored()
    {
        rightScore++;
        UpdateUI();
    }

    void UpdateUI()
    {
        leftText.text = leftScore.ToString();
        rightText.text = rightScore.ToString();
    }
}
