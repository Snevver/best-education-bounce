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

    void Awake()
    {
        if (Instance != null) { 
            Destroy(gameObject); 
            return; 
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetHighScore();
    }

    public void AddScore(int amount)
    {
        Score += amount;
        if (Score > HighScore) {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }

    public void GameOver()
    {
        LastScore = Score;
        DiedLastGame = true;

        if (Score > HighScore)
            HighScore = Score;

        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }

    public void ResetHighScore()
    {
        HighScore = 0;
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
    }
}