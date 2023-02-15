using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitThisScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            SceneMgr.Instance.LoadAsyncScene(GData.DUNGEONLOBBY_SCENE_NAME);
        }
    } //OnTriggerEnter2D
}
