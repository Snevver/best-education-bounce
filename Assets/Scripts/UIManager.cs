using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI menuHighScoreText;
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI congratsText;
    public GameObject playButton;

    bool isMenuScene;

    public GameObject deathPanel;

    void Start() {
        isMenuScene = SceneManager.GetActiveScene().name == "MainMenu";

        if (isMenuScene) {
            if (menuHighScoreText != null)  menuHighScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0);
            if (deathPanel != null) deathPanel.SetActive(GameManager.DiedLastGame);

            if (lastScoreText != null) {
                if (GameManager.LastScore > 0) {
                    lastScoreText.gameObject.SetActive(true);
                    lastScoreText.text = "Score: " + GameManager.LastScore;
                } else {
                    lastScoreText.gameObject.SetActive(false);
                }
            }

            if (congratsText != null) {
                bool isNewHighScore = GameManager.LastScore > 0 && GameManager.LastScore >= GameManager.HighScore;
                congratsText.gameObject.SetActive(isNewHighScore);
                if (isNewHighScore) congratsText.text = "New highscore!";
            }
        }
    }

    void Update() {
        if (isMenuScene) return;
        if (scoreText != null) scoreText.text = "Score: " + GameManager.Score.ToString();
        if (highScoreText != null) highScoreText.text = "Best: " + GameManager.HighScore;
    }

    // Method to handle play button click. This will load the main game scene
    public void OnPlayButton() {
        SceneManager.LoadScene("Game");
    }
}