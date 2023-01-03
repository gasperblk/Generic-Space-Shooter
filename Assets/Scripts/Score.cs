using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static int score;
    public static int highScore;

    public static int GetScore()
    {
        return score;
    }

    public static int SetScore(int newScore)
    {
        score = newScore;
        return score;
    }

    public static int AddScore(int toAdd)
    {
        score = score + toAdd;
        return score;
    }

    public static int GetHighscore()
    {
        return highScore;
    }

    public static int SetHighscore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
        }
        return highScore;
    }
}
