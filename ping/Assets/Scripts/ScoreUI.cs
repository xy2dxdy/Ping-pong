using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text textScore;
    public int score;
    public int maxScore = 25;
    public Ball ball;
    public Ball secondBall;
    public int difference = 3;

    private void Update()
    {
        textScore.text = "" + score;
        if (score >= maxScore)
        {
            Debug.Log("END");
            Application.Quit();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ball.GameObject() || collision.gameObject == secondBall.GameObject())
        {
            int.TryParse(textScore.text, out score);
            score += difference;
        }
    }

}
