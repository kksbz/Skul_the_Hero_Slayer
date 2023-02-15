using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitThisScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            UIManager.Instance.ShowLoading(GData.DUNGEONLOBBY_SCENE_NAME);
        }
    }
}
