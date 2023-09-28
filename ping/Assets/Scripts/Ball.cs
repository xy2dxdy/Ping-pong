using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    private GameObject obj;//��� �� ������� ��������� �����
    public Gluing gl;
    public Gluing gl2;
    public Doubling doubling1;
    public Doubling doubling2;
    public ScoreUI wallRight;
    public ScoreUI wallLeft;
    public int numberOfCollisions = 0;
    public RandomSpawn spawn;
    public ScoreUI scoreUI;
    public MoveRacket racketLeft;
    public MoveRacket racketRight;
    public Swipe swipe1;
    public Swipe swipe2;
    public void Copy(Ball ball)
    {
        gl = ball.gl;
        gl2 = ball.gl2;
        doubling1 = ball.doubling1;
        doubling2 = ball.doubling2;
    }
    private void Start()
    {

        gl2.down = false;

    }

    float hitFactor(Vector2 racketPos, Vector2 ballPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "RacketLeft")
        {
            scoreUI = wallRight;
            numberOfCollisions++;
            if (gl.down == true)
            {
                if (doubling2.isBallCreated)
                    gl.down = false;
                else
                {
                    gl.delta = transform.position - gl.obj.transform.position;
                    gl.hit = true;
                }
            }
            else
            {
                float y = hitFactor(collision.transform.position, transform.position, collision.collider.bounds.size.y);
                Vector2 dir = new Vector2(1, y).normalized;
                GetComponent<Rigidbody2D>().velocity = dir * speed;
            }

            if (doubling2.down == true && doubling2.hit == true)
            {

                if (transform != doubling2.newBall)
                {
                    gl.ball = doubling2.newBall;
                    gl2.ball = doubling2.newBall;
                    doubling1.ball = doubling2.newBall;
                    doubling2.ball = doubling2.newBall;
                    doubling2.isRun = false;
                }

                doubling2.down = false;
                doubling2.isBallCreated = false;
                Destroy(transform.GameObject());
                if (wallRight.secondBall != null)
                {
                    wallRight.ball = wallRight.secondBall;
                    wallRight.secondBall = null;
                    doubling2.ball = doubling2.newBall;
                    doubling2.newBall = null;
                }

            }
            else
            {
                if (doubling1.down == true)
                {
                    doubling1.hit = true;
                    if (this == doubling1.newBall)
                    {
                        doubling1.ball = doubling1.newBall;
                        doubling1.newBall = this;
                    }

                }
            }
            if (swipe1.down == true)
                swipe1.hit = true;
        }
        else
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "RacketRight")
            {
                scoreUI = wallLeft;
                numberOfCollisions++;
                if (gl2.down == true)
                {
                    if (doubling1.isBallCreated)
                        gl2.down = false;
                    else
                    {
                        gl2.delta = transform.position - gl2.obj.transform.position;
                        gl2.hit = true;
                    }
                }
                else
                {
                    float y = hitFactor(collision.transform.position, transform.position, collision.collider.bounds.size.y);
                    Vector2 dir = new Vector2(-1, y).normalized;
                    GetComponent<Rigidbody2D>().velocity = dir * speed;
                }
                if (doubling1.down == true && doubling1.hit == true)
                {


                    if (transform != doubling1.newBall)
                    {
                        gl.ball = doubling1.newBall;
                        gl2.ball = doubling1.newBall;
                        doubling1.ball = doubling1.newBall;
                        doubling2.ball = doubling1.newBall;
                        doubling1.isRun = false;
                    }

                    doubling1.down = false;
                    doubling1.isBallCreated = false;
                    Destroy(transform.GameObject());
                    if (wallLeft.secondBall != null)
                    {
                        wallLeft.ball = wallLeft.secondBall;
                        wallLeft.secondBall = null;
                        doubling1.ball = doubling1.newBall;
                        doubling1.newBall = null;
                    }

                }
                else
                {
                    if (doubling2.down == true)
                    {

                        doubling2.hit = true;
                        if (this == doubling2.newBall)
                        {
                            doubling2.ball = doubling2.newBall;
                            doubling2.newBall = this;
                        }
                    }
                }
                if (swipe2.down == true)
                    swipe2.hit = true;
            }
            else
            {
                if (collision.gameObject == wallLeft.GameObject())
                {
                    if (doubling2.isBallCreated)
                    {
                        if (transform != doubling2.newBall)
                        {
                            gl.ball = doubling2.newBall;
                            gl2.ball = doubling2.newBall;
                            doubling1.ball = doubling2.newBall;
                            doubling2.ball = doubling2.newBall;
                            doubling2.isRun = false;
                            wallLeft.ball = wallLeft.secondBall;
                            wallLeft.secondBall = null;
                        }
                        doubling2.isBallCreated = false;
                        doubling2.down = false;
                        Destroy(transform.GameObject());
                    }
                    else
                    {
                        GameObject gameObject = GameObject.Find("RacketLeft");
                        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        transform.position = gameObject.transform.position;
                        transform.position += new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                        gl.delta = transform.position - gl.obj.transform.position;
                        gl.down = true;
                        gl2.down = false;
                        doubling1.down = false;
                        doubling2.down = false;
                        swipe1.down = false;
                        swipe2.down = false;
                    }
                    if (racketLeft.isInverce == true)
                        racketLeft.isInverce = false;
                    if (racketRight.isInverce == true)
                        racketRight.isInverce = false;
                    if (racketRight.isSlow == true)
                    {
                        racketRight.isSlow = false;
                        racketRight.speed *= 2;
                    }
                    if (racketLeft.isSlow == true)
                    {
                        racketLeft.isSlow = false;
                        racketLeft.speed *= 2;
                    }
                }
                else
                {
                    if (collision.gameObject == wallRight.GameObject())
                    {
                        if (doubling1.isBallCreated)
                        {
                            if (transform != doubling1.newBall)
                            {
                                gl.ball = doubling1.newBall;
                                gl2.ball = doubling1.newBall;
                                doubling1.ball = doubling1.newBall;
                                doubling2.ball = doubling1.newBall;
                                doubling1.isRun = false;
                                wallRight.ball = wallRight.secondBall;
                                wallRight.secondBall = null;
                            }

                            doubling1.isBallCreated = false;
                            doubling1.down = false;
                            Destroy(transform.GameObject());

                        }
                        else
                        {
                            GameObject gameObject = GameObject.Find("RacketRight");
                            transform.position = gameObject.transform.position;
                            transform.position -= new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                            gl2.down = true;
                            gl2.delta = transform.position - gl2.obj.transform.position;
                            gl.down = false;
                            doubling1.down = false;
                            doubling2.down = false;
                            swipe1.down = false;
                            swipe2.down = false;
                        }
                        if (racketLeft.isInverce == true)
                            racketLeft.isInverce = false;
                        if (racketRight.isInverce == true)
                            racketRight.isInverce = false;
                        if (racketRight.isSlow == true)
                        {
                            racketRight.isSlow = false;
                            racketRight.speed *= 2;
                        }
                        if (racketLeft.isSlow == true)
                        {
                            racketLeft.isSlow = false;
                            racketLeft.speed *= 2;
                        }

                    }
                    
                }
            }
        }
        if (numberOfCollisions >= 3)
        {
            spawn.Spawn();
            numberOfCollisions = 0;
            if (racketLeft.isInverce == true)
                racketLeft.isInverce = false;
            if (racketRight.isInverce == true)
                racketRight.isInverce = false;
            if (racketRight.isSlow == true)
            {
                racketRight.isSlow = false;
                racketRight.speed *= 2;
            }
            if (racketLeft.isSlow == true)
            {
                racketLeft.isSlow = false;
                racketLeft.speed *= 2;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Gold(Clone)")
        {
            /*���� ������ �� ������� ����������*/
            this.scoreUI.score++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "Purple(Clone)")
        {
            if (scoreUI == wallRight)
            {
                racketLeft.isInverce = true;
                Destroy(collision.gameObject);
            }
            if (scoreUI == wallLeft)
            {
                racketRight.isInverce = true;
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.name == "Blue(Clone)")
        {
            if (scoreUI == wallRight)
            {
                racketLeft.speed /= 2;
                racketLeft.isSlow = true;
                Destroy(collision.gameObject);
            }
            if (scoreUI == wallLeft)
            {
                racketRight.speed /= 2;
                racketRight.isSlow = true;
                Destroy(collision.gameObject);
            }
        }
    }
}
            
            