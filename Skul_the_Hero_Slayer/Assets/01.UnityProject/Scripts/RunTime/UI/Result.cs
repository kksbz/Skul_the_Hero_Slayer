using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Result : MonoBehaviour
{
    private TMP_Text timeText;
    private TMP_Text killCountText;
    private TMP_Text damageText;
    private Image showDead;
    private void OnEnable()
    {
        showDead = gameObject.FindChildObj("ScreenShot").GetComponentMust<Image>();
        timeText = gameObject.FindChildObj("TimeText").GetComponentMust<TMP_Text>();
        killCountText = gameObject.FindChildObj("KillCount").GetComponentMust<TMP_Text>();
        damageText = gameObject.FindChildObj("Damage").GetComponentMust<TMP_Text>();
        float time = Time.time - GameManager.Instance.totalTime;
        int minute = (int)(time / 60);
        string second = (time % 60).ToString("F0");
        timeText.text = $"{minute}분{second}초";
        killCountText.text = $"{GameManager.Instance.killCount}";
        damageText.text = $"{GameManager.Instance.totalDamage}";
        Scene scene = SceneManager.GetActiveScene();
        showDead.sprite = UIManager.Instance.daedScreenShot;
    } //ShowResult

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.Instance.bgAudio.Stop();
            UIManager.Instance.resultObj.SetActive(false);
            UIManager.Instance.InitUIManager();
            GameManager.Instance.InitGameManager();
            SceneMgr.Instance.LoadAsyncScene(GData.CASTLELOBBY_SCENE_NAME);
        }
    }
}
