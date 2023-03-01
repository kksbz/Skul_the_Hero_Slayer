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
    }

    // Update is called once per frame
    void Update()
    {
        BgInPhase();
    }

    private void BgInPhase()
    {
        if (boss.isDead == true)
        {
            if (AudioManager.Instance.bgAudio.clip.name == AudioManager.Instance.bossBgSound.name)
            {
                AudioManager.Instance.bgAudio.clip = AudioManager.Instance.stageSound;
                AudioManager.Instance.bgAudio.Play();
            }
            skySprite.sprite = bossDeadSky;
            bgSprite.sprite = bossDeadBg;
            return;
        }
        if (boss.isDead == false)
        {
            AudioManager.Instance.bgAudio.clip = AudioManager.Instance.bossBgSound;
            if (!AudioManager.Instance.bgAudio.isPlaying)
            {
                AudioManager.Instance.bgAudio.Play();
            }
            if (boss.isChangePhase == false)
            {
                skySprite.sprite = bossP1Sky;
                bgSprite.sprite = bossP1Bg;
            }
            else
            {
                skySprite.sprite = bossP2Sky;
                bgSprite.sprite = bossP2Bg;
            }
        }
    }
}
