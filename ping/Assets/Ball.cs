using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

    }

    float hitFactor(Vector2 racketPos, Vector2 ballPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "RacketLeft")
        {
            float y = hitFactor(collision.transform.position, transform.position, collision.collider.bounds.size.y);
            Vector2 dir = new Vector2(1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        else
        {
            if (collision.gameObject.name == "RacketRight")
            {
                float y = hitFactor(collision.transform.position, transform.position, collision.collider.bounds.size.y);
                Vector2 dir = new Vector2(-1, y).normalized;
                GetComponent<Rigidbody2D>().velocity = dir * speed;
            }
            else
            {
                if (collision.gameObject.name == "WallLeft")
                {
                    GameObject gameObject = GameObject.Find("RacketLeft");
                    transform.position = gameObject.transform.position;
                    transform.position += new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                    GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                }
                else
                {
                    if (collision.gameObject.name == "WallRight")
                    {
                        GameObject gameObject = GameObject.Find("RacketRight");
                        transform.position = gameObject.transform.position;
                        transform.position -= new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x / 2, 0, 0);
                        GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
                    }
                }
            }
        }
    }
}
