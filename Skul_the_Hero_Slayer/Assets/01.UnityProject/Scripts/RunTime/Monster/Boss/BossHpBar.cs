using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    private BossMonster boss;
    private GameObject hpObjP1;
    private GameObject hpObjP2;
    private Image hpBarP1;
    private Image hpBarP2;
    // Start is called before the first frame update
    void Start()
    {
        boss = GFunc.GetRootObj("Boss").GetComponentMust<BossMonster>();
        hpObjP1 = gameObject.FindChildObj("Boss1Phase");
        hpObjP2 = gameObject.FindChildObj("Boss2Phase");
        hpBarP1 = hpObjP1.FindChildObj("HpBar").FindChildObj("Hp").GetComponentMust<Image>();
        hpBarP2 = hpObjP2.FindChildObj("HpBar").FindChildObj("Hp").GetComponentMust<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowHpBar();
    }

    private void ShowHpBar()
    {
        if (boss.isChangeBossState == true)
        {
            hpObjP1.SetActive(false);
            hpObjP2.SetActive(false);
            return;
        }

        if (boss.isChangePhase == false)
        {
            hpObjP1.SetActive(true);
            hpObjP2.SetActive(false);
            hpBarP1.fillAmount = (float)boss.hp / (float)boss.maxHp;
        }

        if (boss.isChangePhase == true)
        {
            hpObjP1.SetActive(false);
            hpObjP2.SetActive(true);
            hpBarP2.fillAmount = (float)boss.hp / (float)boss.maxHp;
        }
    }
}
