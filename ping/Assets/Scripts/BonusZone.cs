using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZone : MonoBehaviour
{
    public Ball ball;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == ball)
        {
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       // Destroy(this);
    }
}
