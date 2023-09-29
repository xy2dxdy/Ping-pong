using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursoreLock : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
