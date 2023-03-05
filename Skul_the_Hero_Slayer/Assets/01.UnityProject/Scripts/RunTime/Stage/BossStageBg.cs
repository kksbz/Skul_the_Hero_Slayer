using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageBg : MonoBehaviour
{
    private BossMonster boss;
    public SpriteRenderer skySprite;
    public SpriteRenderer bgSprite;
    public Sprite bossP1Sky;
    public Sprite bossP2Sky;
    public Sprite bossDeadSky;
    public Sprite bossP1Bg;
    public Sprite bossP2Bg;
    public Sprite bossDeadBg;
    // Start is called before the first frame update
    void Start()
    {
        boss = GFunc.GetRootObj("Boss").GetComponentMust<BossMonster>();
        skySprite = gameObject.FindChildObj("Sky").GetComponentMust<SpriteRenderer>();
        bgSprite = gameObject.FindChildObj("BgImage").GetComponentMust<SpriteRenderer>();
        UIManager.Instance.ShowStageName(GData.BOSS_SCENE_NAME, GData.BOSS_SCENE_SUB_NAME);
        UIManager.Instance.minimap.SetActive(false);
    } //Start

    // Update is called once per frame
    void Update()
    {
        BgInPhase();
    } //Update

    private void BgInPhase()
    {
        //Boss가 죽으면 실행
        if (boss.isDead == true)
        {
            if (AudioManager.Instance.bgAudio.clip.name == AudioManager.Instance.bossBgSound.name)
            {
                AudioManager.Instance.bgAudio.clip = AudioManager.Instance.stageSound;
                if (AudioManager.Instance.isPlayAudio == true)
                {
                    AudioManager.Instance.bgAudio.Play();
                }
            }
            skySprite.sprite = bossDeadSky;
            bgSprite.sprite = bossDeadBg;
            return;
        }
        //Boss가 살아있으면 실행
        if (boss.isDead == false)
        {
            AudioManager.Instance.bgAudio.clip = AudioManager.Instance.bossBgSound;
            //BgSound가 실행중이지 않으면 실행
            if (!AudioManager.Instance.bgAudio.isPlaying)
            {
                if (AudioManager.Instance.isPlayAudio == true)
                {
                    AudioManager.Instance.bgAudio.Play();
                }
            }
            //1페이즈 배경
            if (boss.isChangePhase == false)
            {
                skySprite.sprite = bossP1Sky;
                bgSprite.sprite = bossP1Bg;
            }
            //2페이즈 배경
            else
            {
                skySprite.sprite = bossP2Sky;
                bgSprite.sprite = bossP2Bg;
            }
        }
    } //BgInPhase
}
