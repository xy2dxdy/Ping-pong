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
    private int current = 0;
    public Gluing gluing;
    public Doubling doubling;
    public Swipe swipe;
    void Start()
    {
        objects[current].GetComponent<Outline>().enabled = true;
    }
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
                    swipe.down = true;
                    break;
                default:
                    break;
            }

        }
        if (Input.GetKeyDown(right))
        {
            objects[current].GetComponent<Outline>().enabled = false;     
            current++;
            if(current > objects.Length - 1)
                current = 0;
            objects[current].GetComponent<Outline>().enabled = true;
        }
    }
}
