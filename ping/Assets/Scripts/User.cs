using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class User : MonoBehaviour
{
    public GameObject[] objects = new GameObject[3];
    public KeyCode code;
    public KeyCode right;
    //public int deltaX = 1;
    private int current = 0;
    public Gluing gluing;
    public Doubling doubling;
    void Start()
    {
        //Outline line = objects[current].GetComponent<Outline>();
        objects[current].GetComponent<Outline>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(code))
        {
            switch (current)
            {
                case 0:
                    gluing.down = true;
                    break;
                case 1:
                    doubling.down = true;
                    doubling.hit = false;
                    doubling.isRun = false;
                    doubling.isBallCreated = false;

                    break;
                case 2:
                    gluing.ball.GetComponent<Rigidbody2D>().velocity *= 2;
                    break;
                default:
                    break;
            }

        }
        //if(Input.GetKeyDown(right) && current > 0)
        //{
           
        //    objects[current].GetComponent<Outline>().enabled = false;
        //    //objects[current].transform.position -= new Vector3(deltaX, 0, 0);
        //    current--;
        //    //objects[current].transform.position += new Vector3(deltaX, 0, 0);
        //    objects[current].GetComponent<Outline>().enabled = true;

        //}
        if (Input.GetKeyDown(right))
        {
            objects[current].GetComponent<Outline>().enabled = false;
           // objects[current].transform.position -= new Vector3(deltaX, 0, 0);
            current++;
            if(current > objects.Length - 1)
            {
                current = 0;
            }
            //objects[current].transform.position += new Vector3(deltaX, 0, 0);
            objects[current].GetComponent<Outline>().enabled = true;
            
        }
    }
}
