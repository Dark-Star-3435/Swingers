using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject pausePage;
    public GameObject gameOverPage;
    public Text scoreText;

    private void Start()
    {
        Time.timeScale = 1;
    }
    enum PageState
    {
        None,
        Pause,
        GameOver
    }

    int score = 0;
    bool gameOver = false;

    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }
    void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDied += OnPlayerDied;
        PlayerController.OnPlayerScored += OnPlayerScored;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDied -= OnPlayerDied;
        PlayerController.OnPlayerScored -= OnPlayerScored;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("HighScore"); //saves players highscore
        if (score > savedScore) //if current score is greater than highest score
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageState(PageState.GameOver);
        Time.timeScale = 0;
    }
    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString(); //takes players score and converts it to text
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SetPageState(PageState.Pause);

            
        }
    }

    // Update is called once per frame
    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None: //initial game state before player starts
                pausePage.SetActive(false);
                gameOverPage.SetActive(false);
                break;

            case PageState.Pause:
                pausePage.SetActive(true); //activates game start state
                gameOverPage.SetActive(false);
                break;

            case PageState.GameOver:
                pausePage.SetActive(false);
                gameOverPage.SetActive(true); //activates game over state
                break;
        }
    }

    public void ConfirmGameOver()
    {
        //OnGameOverConfirmed(); //event
        scoreText.text = "0";
        SetPageState(PageState.GameOver);
    }
}
