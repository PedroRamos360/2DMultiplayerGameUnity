using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    Text scoreText;
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void IncreaseScore()
    {
        Score.score++;
        scoreText.text = score.ToString();
    }
}
