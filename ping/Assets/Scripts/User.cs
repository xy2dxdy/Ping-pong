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
    public Gluing gl;
    void Start()
    {
        objects[current].transform.position += new Vector3(deltaX, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(code))
        {
           // gl.down = false;
            //if (objects[current].GetType() == typeof(Gluing))
            //{
            //    gl =
            //}

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
