using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.DebugUI;

public class ScoreUI : MonoBehaviour
{
    public Text textScore;
    public int score;
    public int maxScore = 25;
    public Ball ball;
    public Ball secondBall;
    public int difference = 3;
    public AudioSource mySource;
    public AudioSource theEnd;
    public Text user;
    public Text winner;
    public Pause pause;

    private void Update()
    {
        textScore.text = "" + score;
        if (score >= maxScore)
        {
            Destroy(ball.spawn.zone);
            winner.text = user.text + " is WIN. Press ESC to exit to the menu";
            pause.paused = true;
            pause.panel.GetComponent<Text>().enabled = false;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ball.GameObject() /*|| collision.gameObject == secondBall.GameObject()*/)
        {
            int.TryParse(textScore.text, out score);
            score += difference;
            if(score >= maxScore)
                theEnd.Play();
            else
                mySource.Play();
        }
    }

}
