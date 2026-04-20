using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int Score;
    public static int HighScore;
    public static int LastScore;
    public static bool DiedLastGame = false;

    void Awake() {
        // Ensure only one instance of GameManager exists
        if (Instance != null) { 
            Destroy(gameObject); 
            return; 
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update() {
        // Check for a reset high score input
        if (Input.GetKeyDown(KeyCode.R)) ResetHighScore();
    }

    // Method to add score and update high score if necessary
    public void AddScore(int amount) {
        Score += amount;
        
        if (Score > HighScore) {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }

    // Method to handle game over logic. This includes saving the last score and updating the high score
    public void GameOver() {
        LastScore = Score;
        DiedLastGame = true;

        if (Score > HighScore) HighScore = Score;

        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }

    // Helper method to reset the high score. This is mainly for testing purposes
    private void ResetHighScore() {
        HighScore = 0;
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
    }
}