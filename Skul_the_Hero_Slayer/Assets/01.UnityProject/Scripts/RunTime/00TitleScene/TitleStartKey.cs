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
            UIManager.Instance.ShowLoading(GData.CASTLELOBBY_SCENE_NAME);
        }
    }
}
