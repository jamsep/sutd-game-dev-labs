using UnityEngine;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI highscoreText;
    public IntVariable gameScore;
    public GameConstants gameConstants;

    void Start()
    {
        UpdateLoadingScreenUI();
    }

    public void UpdateLoadingScreenUI()
    {
        if (endScoreText != null && gameScore != null)
        {
            endScoreText.text = "SCORE: " + gameScore.Value.ToString("D6");
        }

        if (livesText != null && gameConstants != null)
        {
            livesText.text = "x " + gameConstants.currentLives.ToString();
        }
    }
}