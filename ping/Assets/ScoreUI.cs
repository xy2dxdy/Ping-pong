using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text textScore;
    private int score = 0;

    //private void Start()
    //{
    //    textScore.text = "" + score;
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            score++;
            textScore.text = "" + score;
        }
    }

}
