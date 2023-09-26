using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text textScore;
    private int score;
    public int maxScore = 25;
    public Ball ball;
    public Ball secondBall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ball.GameObject() || collision.gameObject == secondBall.GameObject())
        {
            Debug.Log(collision.gameObject);
            int.TryParse(textScore.text, out score);
            score += 3;
            textScore.text = "" + score;
            if (score >= maxScore)
            {
                Debug.Log("END");
                Application.Quit();
            }
        }
    }

}
