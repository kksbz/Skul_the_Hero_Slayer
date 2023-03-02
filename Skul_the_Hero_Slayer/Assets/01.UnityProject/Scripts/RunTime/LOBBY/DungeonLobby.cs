using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.bgAudio.clip = AudioManager.Instance.stageSound;
        AudioManager.Instance.bgAudio.Play();
        UIManager.Instance.ShowStageName(GData.DUNGEONLOBBY_SCENE_NAME, GData.DUNGEONLOBBY_SCENE_SUB_NAME);
        UIManager.Instance.minimap.SetActive(true);
    }
}
