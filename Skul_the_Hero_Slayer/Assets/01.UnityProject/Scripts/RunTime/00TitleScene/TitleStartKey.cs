using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStartKey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            GFunc.LoadScene(GData.PLAY_SCENE_NAME);
        }
    }
}
