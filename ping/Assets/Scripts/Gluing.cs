using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gluing : MonoBehaviour
{
    public GameObject obj;
    public Ball ball;
    public KeyCode code;
    public Vector3 delta;
    public bool down = true;
    public int speed = 13;
    private void Start()
    {
        delta = ball.transform.position - obj.transform.position;
    }
    private void Update()
    {
        if (down)
        {
            
            //_delta = ball.transform.position - obj.transform.position;
            if (Input.GetKeyDown(code))
            {
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                down = false;

            }
            else
                ball.transform.position = delta + obj.transform.position;
        }
    }



}
