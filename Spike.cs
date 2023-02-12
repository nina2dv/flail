using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spike : MonoBehaviour
{
    public Text scoreText;
 
    public static int score;
    void Start()
    {
        score = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        UpdateScoreText();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            score++;
            UpdateScoreText();
        }
    }
    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
