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
        //해상도 설정
        Screen.SetResolution(setWidth, setHeight, true);
    }
}
