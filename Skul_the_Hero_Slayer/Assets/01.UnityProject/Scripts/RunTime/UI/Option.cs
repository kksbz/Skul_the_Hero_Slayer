using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Button goTitle;
    public Button bgSound;
    public Button gameContinue;
    public Button exitGame;
    // Start is called before the first frame update
    void Awake()
    {
        //타이틀화면으로 이동
        goTitle.onClick.AddListener(() =>
        {
            AudioManager.Instance.bgAudio.Stop();
            GameManager.Instance.InitGameManager();
            UIManager.Instance.InitUIManager();
            UIManager.Instance.optionObj.SetActive(false);
            SceneMgr.Instance.LoadAsyncScene(GData.TITLE_SCENE_NAME);
            Time.timeScale = 1;
        });

        //배경음 On/Off
        bgSound.onClick.AddListener(() =>
        {
            if (AudioManager.Instance.bgAudio.isPlaying == true)
            {
                AudioManager.Instance.isPlayAudio = false;
                AudioManager.Instance.bgAudio.Pause();
            }
            else
            {
                AudioManager.Instance.isPlayAudio = true;
                AudioManager.Instance.bgAudio.Play();
            }
        });

        //게임종료
        exitGame.onClick.AddListener(() =>
        {
            GFunc.QuitThisGame();
        });

        //게임 계속 진행
        gameContinue.onClick.AddListener(() =>
        {
            UIManager.Instance.optionObj.SetActive(false);
            Time.timeScale = 1;
        });
    }
}
