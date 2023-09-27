using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public bool down = false;
    public bool hit = false;
    public Ball ball;
    public int time = 6;
    void FixedUpdate()
    {
        if (down && hit)
        {
            ball.GetComponent<Rigidbody2D>().velocity *= 2;
            down = false;
            hit = false;
        }
    }
}
