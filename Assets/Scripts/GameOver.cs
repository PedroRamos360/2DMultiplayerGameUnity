using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public void Start()
    {
        Text scoreText = GameObject.Find("GameOverScore").GetComponent<Text>();
        scoreText.text = "SCORE: " + Score.score.ToString();
    }
    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
