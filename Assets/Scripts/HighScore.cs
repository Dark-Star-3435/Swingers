using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //refrenced unity's UI system

[RequireComponent(typeof(Text))]

public class HighScore : MonoBehaviour
{
    Text highscore;
    private void Start()
    {
        highscore = GetComponent<Text>();
        highscore.text = PlayerPrefs.GetInt("HighScore").ToString(); //calls players score from playerprefs and converts it to readable text (string) overriding highscore.text's data
    }
}
