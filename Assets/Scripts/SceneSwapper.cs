using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1); // loads scene 1 (game scene)
    }
    public void Quit()
    {
        Application.Quit(); // closes application and returns user to windows
    }
}
