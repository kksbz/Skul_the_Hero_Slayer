using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStartKey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneMgr.Instance.LoadAsyncScene(GData.CASTLELOBBY_SCENE_NAME, GData.CASTLELOBBY_SCENE_SUB_NAME);
        }
    }
}
