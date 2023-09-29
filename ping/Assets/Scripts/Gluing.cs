using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Gluing : MonoBehaviour
{
    public GameObject obj;
    public Ball ball;
    public KeyCode code;
    public Vector3 delta;
    public bool down = true;
    public bool hit = true;
    public int speed = 13;
    public int time = 10;
    public GameObject sprite;
    public Swipe swipe2;
    public CoroutineTimer timer;
    private void Start()
    {
        delta = ball.transform.position - obj.transform.position;
    }
    private void Update()
    {
        if (down && hit)
        {

            if (Input.GetKeyDown(code))
            {
                ParticleSystem ps = ball.particle.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule psmain = ps.main;
                
                Vector2 vector = new Vector2(UnityEngine.Random.Range(10, 20), UnityEngine.Random.Range(-10, 10)).normalized;
                if (this == ball.gl)
                {
                    psmain.startColor = new Color(1, 0.09803922f, 0, 1);
                    Instantiate(ball.particle, ball.transform.position, Quaternion.identity);
                }
                if (this == ball.gl2)
                {
                    psmain.startColor = new Color(0, 1, 1, 1);
                    Instantiate(ball.particle, ball.transform.position, Quaternion.identity);
                }
                if (swipe2.isUsed)
                {
                    //swipe2.isGluing = true;
                    ball.GetComponent<Rigidbody2D>().velocity = vector * speed * 2;
                    swipe2.isUsed = false;
                    //swipe2.isGluing = false;
                }
                else
                    ball.GetComponent<Rigidbody2D>().velocity = vector * speed;
                down = false;
                hit = false;

            }
            else
            {
                //ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                ball.transform.position = delta + obj.transform.position;
                
            }
        }
    }
}
