using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    public float speedValue;
    private GameObject obj;//ÍÚÓ ËÁ Ë„ÓÍÓ‚ ÔÓÒÎÂ‰ÌËÏ ÓÚ·ËÎ
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
    public ParticleSystem particle;
    public AudioSource audioSource;
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
    private void FixedUpdate()
    {
        if(GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
            speed += Time.deltaTime * speed * 0.01f;
    }

    float hitFactor(Vector2 racketPos, Vector2 ballPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "RacketLeft")
        {
            if(gl.down == false)
                audioSource.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Vector3 pos = transform.position;
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            psmain.startColor = new Color(1, 0.09803922f, 0, 1);
            scoreUI = wallRight;
            numberOfCollisions++;
            if (gl.down == true)
            {
                if (doubling2.isBallCreated)
                    gl.down = false;
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    GameObject gameObject = GameObject.Find("RacketLeft");
                    transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, 0);
                    transform.position += new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 4 + transform.GetComponent<BoxCollider2D>().size.x / 4, 0, 0);
                    gl.delta = transform.position - gl.obj.transform.position;

                    gl.hit = true;
                }
            }
            else
            {
                float y = hitFactor(collision.transform.position, transform.position, collision.collider.bounds.size.y);
                Vector2 dir = new Vector2(1, y).normalized;
                Instantiate(particle, transform.position, Quaternion.identity);
                audioSource.Play();
                GetComponent<Rigidbody2D>().velocity = dir * speed;
            }

            if (doubling2.down == true && doubling2.hit == true)
            {
                if (swipe1.down == false)
                {
                    if (transform != doubling2.newBall)
                    {
                        gl.ball = doubling2.newBall;
                        gl2.ball = doubling2.newBall;
                        doubling1.ball = doubling2.newBall;
                        doubling2.ball = doubling2.newBall;
                        doubling2.isRun = false;
                        scoreUI.ball = doubling2.newBall;
                        wallRight.ball = wallRight.secondBall;
                        wallRight.secondBall = null;
                        wallLeft.ball = wallLeft.secondBall;
                        wallLeft.secondBall = null;
                        swipe1.ball = doubling2.newBall;
                        swipe2.ball = doubling2.newBall;
                    }

                    doubling2.down = false;
                    doubling2.isBallCreated = false;
                    Destroy(transform.GameObject());
                }
                else 
                {
                    if (transform == doubling2.newBall)
                    {
                        Destroy(gl.ball.GameObject());
                        gl.ball = doubling2.newBall;
                        gl2.ball = doubling2.newBall;
                        doubling1.ball = doubling2.newBall;
                        doubling2.ball = doubling2.newBall;
                        doubling2.isRun = false;
                        scoreUI.ball = doubling2.newBall;
                        swipe1.ball = doubling2.newBall;
                        swipe2.ball = doubling2.newBall;
                        wallRight.ball = wallRight.secondBall;
                        wallRight.secondBall = null;
                        wallLeft.ball = wallLeft.secondBall;
                        wallLeft.secondBall = null;
                        swipe1.ball = doubling2.newBall;
                        swipe2.ball = doubling2.newBall;
                    }
                    else
                    {
                        Destroy(doubling2.newBall.GameObject());
                    }
                    doubling2.down = false;
                    doubling2.isBallCreated = false;
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
                if(gl.down == false)
                    swipe1.hit = true;
        }
        else
        {
            if (collision.gameObject.name == "RacketRight")
            {
                if(gl2.down == false)
                    audioSource.Play();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Vector3 pos = transform.position;
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule psmain = ps.main;
                psmain.startColor = new Color(0, 1, 1, 1);
                
                scoreUI = wallLeft;
                numberOfCollisions++;
                if (gl2.down == true)
                {
                    if (doubling1.isBallCreated)
                        gl2.down = false;
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        GameObject gameObject = GameObject.Find("RacketRight");
                        transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, 0);
                        transform.position -= new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 4 + transform.GetComponent<BoxCollider2D>().size.x / 4, 0);
                        gl2.delta = transform.position - gl2.obj.transform.position;
                        gl2.hit = true;
                    }
                }
                else
                {
                    float y = hitFactor(collision.transform.position, transform.position, collision.collider.bounds.size.y);
                    Vector2 dir = new Vector2(-1, y).normalized;
                    Instantiate(particle, transform.position, Quaternion.identity);
                    audioSource.Play();
                    GetComponent<Rigidbody2D>().velocity = dir * speed;
                }
                if (doubling1.down == true && doubling1.hit == true)
                {

                    if (swipe2.down == false)
                    {
                        if (transform != doubling1.newBall)
                        {
                            gl.ball = doubling1.newBall;
                            gl2.ball = doubling1.newBall;
                            doubling1.ball = doubling1.newBall;
                            doubling2.ball = doubling1.newBall;
                            doubling1.isRun = false;
                            scoreUI.ball = doubling1.newBall;
                            wallLeft.ball = wallLeft.secondBall;
                            wallLeft.secondBall = null;
                            wallRight.ball = wallRight.secondBall;
                            wallRight.secondBall = null;
                            swipe1.ball = doubling1.newBall;
                            swipe2.ball = doubling1.newBall;
                        }

                        doubling1.down = false;
                        doubling1.isBallCreated = false;
                        Destroy(transform.GameObject());
                    }
                    else 
                    {
                        if (transform == doubling1.newBall)
                        {
                            Destroy(doubling1.ball.GameObject());
                            gl.ball = doubling1.newBall;
                            gl2.ball = doubling1.newBall;
                            doubling1.ball = doubling1.newBall;
                            doubling2.ball = doubling1.newBall;
                            doubling1.isRun = false;
                            scoreUI.ball = doubling1.newBall;
                            swipe1.ball = doubling1.newBall;
                            swipe2.ball = doubling1.newBall;
                            wallLeft.ball = wallLeft.secondBall;
                            wallLeft.secondBall = null;
                            wallRight.ball = wallRight.secondBall;
                            wallRight.secondBall = null;
                        }
                        else
                        {
                            Destroy(doubling1.newBall.GameObject());
                        }
                        doubling1.down = false;
                        doubling1.isBallCreated = false;
                        
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
                {
                    if(gl2.down == false)
                        swipe2.hit = true;
                }
            }
            else
            {
                if (collision.gameObject == wallLeft.GameObject())
                {
                    
                    speed = speedValue;
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
                            wallRight.ball = wallLeft.secondBall;
                            wallRight.secondBall = null;
                            wallLeft.secondBall = null;
                            swipe1.ball = doubling2.newBall;
                            swipe2.ball = doubling2.newBall;
                        }
                        doubling2.isBallCreated = false;
                        doubling2.down = false;
                        wallLeft.score += 3;
                        Destroy(transform.GameObject());
                    }
                    else
                    {
                        GameObject gameObject = GameObject.Find("RacketLeft");
                        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        transform.position = gameObject.transform.position;
                        transform.position += new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                        gl.delta = transform.position - gl.obj.transform.position;
                        gl.isEnd = true;
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
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    if (swipe1.isUsed == true)
                    {
                        GetComponent<Rigidbody2D>().velocity /= 2;
                        swipe1.isUsed = false;
                    }
                    if (swipe2.isUsed == true)
                    {
                        GetComponent<Rigidbody2D>().velocity /= 2;
                        swipe2.isUsed = false;
                    }
                }
                else
                {
                    if (collision.gameObject == wallRight.GameObject())
                    {
                       
                        speed = speedValue;

                        if (doubling1.isBallCreated)
                        {
                            if (transform != doubling1.newBall)
                            {
                                gl.ball = doubling1.newBall;
                                gl2.ball = doubling1.newBall;
                                doubling1.ball = doubling1.newBall;
                                doubling2.ball = doubling1.newBall;
                                doubling1.isRun = false;
                                wallLeft.ball = wallRight.secondBall;
                                wallLeft.secondBall = null;
                                wallRight.ball = wallRight.secondBall;
                                wallRight.secondBall = null;
                                swipe1.ball = doubling1.newBall;
                                swipe2.ball = doubling1.newBall;
                            }

                            doubling1.isBallCreated = false;
                            doubling1.down = false;
                            wallRight.score += 3;
                            Destroy(transform.GameObject());

                        }
                        else
                        {
                            GameObject gameObject = GameObject.Find("RacketRight");
                            transform.position = gameObject.transform.position;
                            transform.position -= new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                            gl2.isEnd = true;
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
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        if (swipe1.isUsed == true)
                        {
                            GetComponent<Rigidbody2D>().velocity /= 2;
                            swipe1.isUsed = false;
                        }
                        if (swipe2.isUsed == true)
                        {
                            GetComponent<Rigidbody2D>().velocity /= 2;
                            swipe2.isUsed = false;
                        }
                    }
                    if (collision.gameObject.name == "WallTop")
                    {
                        audioSource.Play();
                        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                        ParticleSystem.MainModule psmain = ps.main;
                        psmain.startColor = new Color(1, 0, 1, 1);
                        Instantiate(particle, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        if (collision.transform.name == "WallBottom")
                        {
                            audioSource.Play();
                            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                            ParticleSystem.MainModule psmain = ps.main;
                            psmain.startColor = new Color(1, 0, 1, 1);
                            Instantiate(particle, transform.position, Quaternion.identity);
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
            /*œŒ ¿ “ŒÀ‹ Œ Õ¿ ≈ƒ»Õ»÷” ”¬≈À»◊»¬¿ﬁ*/
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
            
            