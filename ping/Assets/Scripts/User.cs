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
                    if (gluing.time != 0)
                    {
                        gluing.down = true;
                        StartCoroutine(TimerGluing());
                    }
                    break;
                case 1:
                    if (doubling.time != 0)
                    {
                        doubling.down = true;
                        doubling.hit = false;
                        doubling.isRun = false;
                        doubling.isBallCreated = false;
                        StartCoroutine(TimerDoubling());
                    }
                    
                    break;
                case 2:
                    if (swipe.time != 0)
                    {
                        swipe.down = true;
                        StartCoroutine(TimerSwipe());
                    }
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
    private IEnumerator TimerGluing()
    {
        int t = gluing.time;
        gluing.time = 0;
        yield return new WaitForSeconds(t);
        gluing.time = t;
    }
    private IEnumerator TimerSwipe()
    {
        int t = swipe.time;
        swipe.time = 0;
        yield return new WaitForSeconds(t);
        swipe.time = t;
    }
    private IEnumerator TimerDoubling()
    {
        int t = doubling.time;
        doubling.time = 0;
        yield return new WaitForSeconds(t);
        doubling.time = t;
    }
}
