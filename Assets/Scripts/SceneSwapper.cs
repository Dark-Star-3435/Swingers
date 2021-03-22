using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1); // loads scene 1 (game scene)
    }
    public void Back()
    {
        SceneManager.LoadScene(0);   // takes player back to splash screen
    }
}
