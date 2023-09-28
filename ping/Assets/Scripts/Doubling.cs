using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;
using System;

public class Doubling : MonoBehaviour
{
    public User user;
    public Ball ball;
    public Ball newBall;
    public bool down = false;
    public bool hit = false;
    public bool isRun = false;
    public bool isBallCreated = false;
    public int speed = 13;
    public int time = 15;
    public Sprite sprite;
    void Update()
    {
        if (down && hit && !isRun)
        {
            StartCoroutine(BallIndicator(ball.transform.position)) ;
            isRun = true;
            isBallCreated = true;
        }

    }
    private IEnumerator BallIndicator(Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        newBall = Instantiate(ball, pos, Quaternion.identity);
        ball.wallLeft.secondBall = newBall;
        ball.wallRight.secondBall = newBall;
        newBall.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

}
