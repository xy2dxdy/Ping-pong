using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text textScore;
    private int score;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            int.TryParse(textScore.text, out score);
            score += 3;
            textScore.text = "" + score;
            if (score == 25)
            {
                Debug.Log("END");
                Application.Quit();
            }
        }
    }

}
