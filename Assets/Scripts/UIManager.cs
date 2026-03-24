using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI menuHighScoreText;
    public GameObject playButton;

    bool isMenuScene;

    void Start()
    {
        isMenuScene = SceneManager.GetActiveScene().name == "MainMenu";

        if (isMenuScene && menuHighScoreText != null)
            menuHighScoreText.text = "Best: " + GameManager.HighScore;
    }

    void Update()
    {
        if (isMenuScene) return;

        if (scoreText != null)
            scoreText.text = "Score: " + GameManager.Score.ToString();
        if (highScoreText != null)
            highScoreText.text = "Best: " + GameManager.HighScore;
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }
}