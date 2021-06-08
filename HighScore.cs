using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static public int score = 2;
    public Text hScoreText;

    private void Awake()
    {
        hScoreText = this.GetComponent<Text>();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        PlayerPrefs.SetInt("HighScore", score);
    }

    // Update is called once per frame
    void Update()
    {
        hScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        if (GameController.G.score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", GameController.G.score);
    }
}
