using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
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
    public GameObject timer;
    public Canvas canvas;
    //public Vector3 posX;
    void Start()
    {
        //posX = transform.position + new Vector3(transform.GetComponent<BoxCollider2D>().size.x / 2, 0 , 0);
        objects[current].GetComponent<Outline>().enabled = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(code))
        {
            switch (current)
            {
                case 0:
                    if (gluing.time != 0 && doubling.down == false && swipe.down == false)
                    {
                        gluing.down = true;
                        StartCoroutine(TimerGluing());
                    }
                    break;
                case 1:
                    if (doubling.time != 0 && gluing.down == false && swipe.down == false)
                    {
                        doubling.down = true;
                        doubling.hit = false;
                        doubling.isRun = false;
                        doubling.isBallCreated = false;
                        StartCoroutine(TimerDoubling());
                    }
                    
                    break;
                case 2:
                    if (swipe.time != 0 && gluing.down == false && doubling.down == false)
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
        gluing.sprite.SetActive(false);
        gluing.timer.time = t;
        GameObject obj = Instantiate(timer, gluing.transform.position, Quaternion.identity);
        obj.transform.SetParent(canvas.transform);
        yield return new WaitForSeconds(t);
        Destroy(obj);
        gluing.sprite.SetActive(true);
        gluing.time = t;
    }
    private IEnumerator TimerSwipe()
    {
        int t = swipe.time;
        swipe.time = 0;
        swipe.sprite.SetActive(false);
        swipe.timer.time = t;
        GameObject obj = Instantiate(timer, swipe.transform.position, Quaternion.identity);
        obj.transform.SetParent(canvas.transform);
        yield return new WaitForSeconds(t);
        Destroy(obj);
        swipe.sprite.SetActive(true);
        swipe.time = t;
    }
    private IEnumerator TimerDoubling()
    {
        int t = doubling.time;
        doubling.time = 0;
        doubling.sprite.SetActive(false);
        doubling.timer.time = t;
        GameObject obj = Instantiate(timer, doubling.transform.position, Quaternion.identity);
        obj.transform.SetParent(canvas.transform);
        yield return new WaitForSeconds(t);
        Destroy(obj);
        doubling.sprite.SetActive(true);
        doubling.time = t;
    }
}
