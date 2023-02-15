using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private int setWidth = 1920;
    private int setHeight = 1080;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(setWidth, setHeight, false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
