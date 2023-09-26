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
    // Update is called once per frame
    void Update()
    {
        if (down && hit && !isRun)
        {
            //Ball bal = new Ball();
            //bal.Copy(ball);
            StartCoroutine(BallIndicator(ball.transform.position)) ;
            isRun = true;
            isBallCreated = true;
            //hit = false;
        }

    }
    private IEnumerator BallIndicator(Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        //preball.gl = ball.gl;
        //preball.gl2 = ball.gl2;
        //preball.doubling1 = ball.doubling1;
        //preball.doubling2 = ball.doubling2;
        //b.Copy(preball);
        //b.gl.down = false;
        //b.gl.hit = false;
        //b.gl2.down = false;
        //b.gl2.hit = false;
        
        newBall = Instantiate(ball, pos, Quaternion.identity);
        //ball.gl.ball = newBall;
        //ball.gl2.ball = newBall;
        //ball.doubling1.ball = newBall;
        //ball.doubling2.ball = newBall;
        ball.wallLeft.secondBall = newBall;
        ball.wallRight.secondBall = newBall;
        newBall.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
       
    }

}
