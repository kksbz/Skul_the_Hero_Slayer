using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStartKey : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.bgAudio.clip = AudioManager.Instance.titleSound;
        if (AudioManager.Instance.isPlayAudio == true)
        {
            AudioManager.Instance.bgAudio.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            AudioManager.Instance.bgAudio.Stop();
            UIManager.Instance.InitUIManager();
            GameManager.Instance.InitGameManager();
            SceneMgr.Instance.LoadAsyncScene(GData.CASTLELOBBY_SCENE_NAME);
        }
    }
}
