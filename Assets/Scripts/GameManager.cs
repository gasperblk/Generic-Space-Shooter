using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Score.SetScore(0);
        scoreText.text = "Score: " + Score.GetScore();
    }

    public void UpdateScore(int toAdd)
    {
        int newScore = Score.AddScore(toAdd);
        Score.SetHighscore(newScore);
        scoreText.text = "Score: " + newScore;
    }
}
