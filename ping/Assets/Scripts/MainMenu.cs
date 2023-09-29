using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image rules;
    [SerializeField] private KeyCode keycode;
    public void PlayGame()
    {
        rules.enabled = true;

    }
    private void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            rules.enabled = false;
        }
    }
    public void ExitGame()
    {
        Debug.Log("Closed game");
        Application.Quit();
    }
}
