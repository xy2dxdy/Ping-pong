using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public GameObject[] objects = new GameObject[3];
    public KeyCode code;
    public KeyCode up;
    public KeyCode down;
    public int deltaX = 1;
    private int current = 0;
    public Gluing gluing;
    public Doubling doubling;
    public Swipe swipe;
    void Start()
    {
        objects[current].transform.position += new Vector3(deltaX, 0, 0);
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
                    break;
                default:
                    break;
            }

        }
        if(Input.GetKeyDown(up) && current > 0)
        {
            objects[current].transform.position -= new Vector3(deltaX, 0, 0);
            current--;
            objects[current].transform.position += new Vector3(deltaX, 0, 0);

        }
        if (Input.GetKeyDown(down) && current < objects.Length - 1)
        {
            objects[current].transform.position -= new Vector3(deltaX, 0, 0);
            current++;
            objects[current].transform.position += new Vector3(deltaX, 0, 0);
        }
    }
}
