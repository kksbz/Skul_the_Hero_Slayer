using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.bgAudio.clip = AudioManager.Instance.stageSound;
        AudioManager.Instance.bgAudio.Play();
        UIManager.Instance.ShowStageName(GData.STAGE_2_SCENE_NAME, GData.STAGE_2_SCENE_SUB_NAME);
    }
}
