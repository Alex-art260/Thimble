using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    public int score;
    private int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("Score");
        highScoreText.text = " " + highScore;
    }

    public void SetScore()
    {
        score += 5;
        scoreText.text = " " + score;

        PlayerPrefs.SetInt("Score", highScore);
        if (PlayerPrefs.GetInt("Score") < score)
        {
            highScore = score;
            PlayerPrefs.Save();
        }
    }
}
