using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    private bool paused = false;
    public Text panel;
    public KeyCode pause;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            if (paused)
                paused = false;
            else
                paused = true;
            if (paused)
            {
                Time.timeScale = 0;
                panel.GetComponent<Text>().enabled = true;
            }
            else 
            {
                Time.timeScale = 1;
                panel.GetComponent<Text>().enabled = false;
            }
        }
    }
}
