using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkulInfo : MonoBehaviour
{
    private GameObject mainSkulFrame;
    private GameObject subSkulFrame;
    private GameObject mainSkillAFrame;
    private GameObject mainSkillBFrame;
    private GameObject subSkillAFrame;
    private GameObject subSkillBFrame;
    private Image swapCoolDown;
    private Image mainSkillACoolDown;
    private Image mainSkillBCoolDown;
    private Image subSkillACoolDown;
    private Image subSkillBCoolDown;
    private Image mainSkul;
    private Image subSkul;
    private Image mainSkillA;
    private Image mainSkillB;
    private Image subSkillA;
    private Image subSkillB;
    // Start is called before the first frame update
    void Start()
    {
        mainSkulFrame = gameObject.FindChildObj("SkulMain");
        subSkulFrame = gameObject.FindChildObj("SkulSub");
        mainSkillAFrame = gameObject.FindChildObj("SkillA");
        mainSkillBFrame = gameObject.FindChildObj("SkillB");
        subSkillAFrame = gameObject.FindChildObj("SubSkillA");
        subSkillBFrame = gameObject.FindChildObj("SubSkillB");
        mainSkul = mainSkulFrame.FindChildObj("MainSkul").GetComponentMust<Image>();
        subSkul = subSkulFrame.FindChildObj("SubSkul").GetComponentMust<Image>();
        mainSkillA = mainSkillAFrame.FindChildObj("MainSkillA").GetComponentMust<Image>();
        mainSkillB = mainSkillBFrame.FindChildObj("MainSkillB").GetComponentMust<Image>();
        subSkillA = subSkillAFrame.FindChildObj("SubSkillA").GetComponentMust<Image>();
        subSkillB = subSkillBFrame.FindChildObj("SubSkillB").GetComponentMust<Image>();

        swapCoolDown = gameObject.FindChildObj("SwapCoolDown").GetComponentMust<Image>();
        mainSkillACoolDown = gameObject.FindChildObj("MainSkillACool").GetComponentMust<Image>();
        mainSkillBCoolDown = gameObject.FindChildObj("MainSkillBCool").GetComponentMust<Image>();
        subSkillACoolDown = gameObject.FindChildObj("SubSkillACool").GetComponentMust<Image>();
        subSkillBCoolDown = gameObject.FindChildObj("SubSkillBCool").GetComponentMust<Image>();
    } //Start

    // Update is called once per frame
    void Update()
    {
        ShowCoolDown();
        ShowSkulImage();
    } //Update

    private void ShowSkulImage()
    {
        mainSkul.sprite = UIManager.Instance.mainSkul;
        subSkul.sprite = UIManager.Instance.subSkul;
        mainSkillA.sprite = UIManager.Instance.mainSkillA;
        mainSkillB.sprite = UIManager.Instance.mainSkillB;
        subSkillA.sprite = UIManager.Instance.subSkillA;
        subSkillB.sprite = UIManager.Instance.subSkillB;
    } //ShowSkulImage

    private void ShowCoolDown()
    {
        //스왑쿨다운
        if (swapCoolDown.fillAmount == 1)
        {
            swapCoolDown.gameObject.SetActive(false);
        }
        else
        {
            swapCoolDown.gameObject.SetActive(true);
        }
        swapCoolDown.fillAmount = UIManager.Instance.swapCoolDown / 6f;

        //스킬A쿨다운
        if (mainSkillACoolDown.fillAmount == 1)
        {
            mainSkillACoolDown.gameObject.SetActive(false);
        }
        else
        {
            mainSkillACoolDown.gameObject.SetActive(true);
        }

        mainSkillACoolDown.fillAmount = UIManager.Instance.skillACoolDown / 10f;

        //스킬B쿨다운
        if (mainSkillBCoolDown.fillAmount == 1)
        {
            mainSkillBCoolDown.gameObject.SetActive(false);
        }
        else
        {
            mainSkillBCoolDown.gameObject.SetActive(true);
        }
        mainSkillBCoolDown.fillAmount = UIManager.Instance.skillBCoolDown / 3f;
    } //ShowCoolDown
}
