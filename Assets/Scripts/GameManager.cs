using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
 public static GameManager Instance;

    public GameObject pausePage;
    public GameObject gameOverPage;
    public Text scoreText;

    private void Start()
    {
        Time.timeScale = 1; // starts time on game start
    }
    enum PageState // page states
    {
        None,
        Pause,
        GameOver
    }

    int score = 0; // score starts at 0
    bool gameOver = false; // by default game is not over

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

    void OnPlayerDied() // when player dies even brings up gameover menu
    {
        gameOver = true;
        SetPageState(PageState.GameOver);
        Time.timeScale = 0; // freezes time when player loses
    }
    void OnPlayerScored()
    {
        score++; // score increases
        scoreText.text = score.ToString(); //takes players score and converts it to text
    }

    void Update()
    {
        // pauses time when escape is pressed and brings up pause menu
        if (Input.GetKey(KeyCode.Escape))
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
        SetPageState(PageState.GameOver); // tells player they suck and that they need to git gud
    }
}
