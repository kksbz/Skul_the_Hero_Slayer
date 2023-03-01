using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.bgAudio.clip = AudioManager.Instance.stageSound;
        AudioManager.Instance.bgAudio.Play();
        UIManager.Instance.ShowStageName(GData.STAGE_1_SCENE_NAME, GData.STAGE_1_SCENE_SUB_NAME);
    }
}
