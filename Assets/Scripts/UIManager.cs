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

    public GameObject deathPanel;

    void Start()
    {
        isMenuScene = SceneManager.GetActiveScene().name == "MainMenu";

        if (isMenuScene) {
            if (menuHighScoreText != null) menuHighScoreText.text = "Best: " + GameManager.HighScore;
            if (deathPanel != null) deathPanel.SetActive(GameManager.DiedLastGame);
        }
    }

    void Update()
    {
        if (isMenuScene) return;
        if (scoreText != null) scoreText.text = "Score: " + GameManager.Score.ToString();
        if (highScoreText != null) highScoreText.text = "Best: " + GameManager.HighScore;
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }
}