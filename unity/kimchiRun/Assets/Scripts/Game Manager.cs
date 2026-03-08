using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State = GameState.Intro;
    public float PlayStartTime;
    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;

    public Player PlayerScript;
    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        IntroUI.SetActive(true);
    }

    float CalculateScore()
    {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    public float CalculateGameSpeed()
    {
        if (State != GameState.Playing)
            return 30f;
        float speed = 30f + 0.5f * Mathf.Floor(CalculateScore() / 10f);

        return Mathf.Min(speed, 20f);
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    void Update()
    {
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            ScoreText.gameObject.SetActive(true);
            HighScoreText.gameObject.SetActive(true);
            HighScoreText.text = "Best: " + GetHighScore();
            PlayStartTime = Time.time;
        }
        if (State == GameState.Playing)
        {
            ScoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        
            if (Lives == 0)
            {
                PlayerScript.KillPlayer();
                EnemySpawner.SetActive(false);
                FoodSpawner.SetActive(false);
                GoldenSpawner.SetActive(false);
                State = GameState.Dead;
                SaveHighScore();
                HighScoreText.text = "Best: " + GetHighScore();
            }
        }
        if (State == GameState.Dead)
        {
            DeadUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene("main");
        }
    }
}