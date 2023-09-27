using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 30;
    public string axis = "Vertical";
    public bool isInverce = false;
    public bool isSlow = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        if (isInverce)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -v * speed);
        }
        else
         GetComponent<Rigidbody2D>().velocity = new Vector2(0, v * speed);
    }
}
