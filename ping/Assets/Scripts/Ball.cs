using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    private GameObject obj;//кто из игроков последним отбил
    public Gluing gl;
    public Gluing gl2;
    private void Start()
    {

        // Vector3 delta = transform.position - objectToFollow.transform.position;
        //Debug.Log("dfghj");
        //GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        gl.delta = transform.position - gl.obj.transform.position;
        gl.down = true;
        gl.hit = true;
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
            }
            else
            {
                if (collision.gameObject.name == "WallLeft")
                {
                    GameObject gameObject = GameObject.Find("RacketLeft");
                    transform.position = gameObject.transform.position;
                    transform.position += new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                    gl.delta = transform.position - gl.obj.transform.position;
                    gl.down = true;
                    gl2.down = false;
                    //GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                }
                else
                {
                    if (collision.gameObject.name == "WallRight")
                    {
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
