using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    public bool down = false;
    public bool hit = false;
    public bool isUsed = false;
    //public bool isGluing = false;
    public Ball ball;
    public int time = 6;
    public GameObject sprite;
    public CoroutineTimer timer;
    void FixedUpdate()
    {
        if (down && hit /*&& !isGluing*/)
        {
            ball.GetComponent<Rigidbody2D>().velocity *= 2;
            down = false;
            hit = false;
            isUsed = true;
            //isGluing = false;
        }
    }
}
