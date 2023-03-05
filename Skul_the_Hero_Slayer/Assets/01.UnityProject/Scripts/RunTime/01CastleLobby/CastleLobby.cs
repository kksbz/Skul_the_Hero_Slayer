using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.bgAudio.clip = AudioManager.Instance.castleSound;
        if (AudioManager.Instance.isPlayAudio == true)
        {
            AudioManager.Instance.bgAudio.Play();
        }
        UIManager.Instance.ShowStageName(GData.CASTLELOBBY_SCENE_NAME, GData.CASTLELOBBY_SCENE_SUB_NAME);
        UIManager.Instance.minimap.SetActive(false);
    }
}
