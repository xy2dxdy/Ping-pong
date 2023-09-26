using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    private GameObject obj;//кто из игроков последним отбил
    public Gluing gl;
    public Gluing gl2;
    public Doubling doubling1;
    public Doubling doubling2;
    public ScoreUI wallRight;
    public ScoreUI wallLeft;
    public void Copy(Ball ball)
    {
        gl = ball.gl;
        gl2 = ball.gl2;
        doubling1 = ball.doubling1;
        doubling2 = ball.doubling2;
    }
    private void Start()
    {

        // Vector3 delta = transform.position - objectToFollow.transform.position;
        //Debug.Log("dfghj");
        //GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        //gl.delta = transform.position - gl.obj.transform.position;
        //gl.down = true;
        //gl.hit = true;
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
            if (gl.down == true)
            {
                gl.delta = transform.position - gl.obj.transform.position;
                gl.hit = true;
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
        else
        {
            if (collision.gameObject.name == "RacketRight")
            {
                if (gl2.down == true)
                {
                    gl2.delta = transform.position - gl2.obj.transform.position;
                    gl2.hit = true;
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
                        transform.position = gameObject.transform.position;
                        transform.position += new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                        gl.delta = transform.position - gl.obj.transform.position;
                        gl.down = true;
                        gl2.down = false;
                        //GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
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

                        GameObject gameObject = GameObject.Find("RacketRight");
                        transform.position = gameObject.transform.position;
                        transform.position -= new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                        gl2.down = true;
                        gl2.delta = transform.position - gl2.obj.transform.position;
                        gl.down = false;
                        //GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;

                    }
                }
            }
        }
    }
}
            
            