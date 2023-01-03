using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;

    void Start()
    {
        highscoreText.text = "Highscore:\n" + Score.GetHighscore();
    }
}
