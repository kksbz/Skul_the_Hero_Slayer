using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null || instance == default)
            {
                return null;
            }
            return instance;
        }
    } //Instance 프로퍼티

    private GameObject loadingObj = default;
    private GameObject stageUiObj = default;
    public GameObject mainUiObj = default;
    public GameObject resultObj = default;
    public GameObject MinimapCamera = default;
    public Sprite daedScreenShot;
    public Sprite mainSkul;
    public Sprite subSkul;
    public Sprite mainSkillA;
    public Sprite mainSkillB;
    public Sprite subSkillA;
    public Sprite subSkillB;
    public int playerHp;
    public int playerMaxHp;
    public float swapCoolDown;
    public float skillACoolDown;
    public float skillBCoolDown;
    public float maxSkillACool;
    public float maxSkillBCool;

    //싱글톤 패턴 적용
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitUIManager();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    } //Awake

    //UIManager 초기화
    private void InitUIManager()
    {
        mainUiObj = gameObject.FindChildObj(GData.MAIN_UI_OBJ_NAME);
        loadingObj = gameObject.FindChildObj(GData.LOADDING_OBJ_NAME);
        stageUiObj = gameObject.FindChildObj(GData.STAGE_UI_OBJ_NAME);
        resultObj = gameObject.FindChildObj(GData.RESULT_UI_OBJ_NAME);
        MinimapCamera = gameObject.FindChildObj("MinimapCamera");
        daedScreenShot = default;
    } //InitUIManager

    //Stage입장시 정보UI 보여주는 함수
    public void ShowStageName(string mainName, string subName)
    {
        TMP_Text mainStageName = stageUiObj.FindChildObj("MainName").GetComponentMust<TMP_Text>();
        TMP_Text subStageName = stageUiObj.FindChildObj("SubName").GetComponentMust<TMP_Text>();
        mainStageName.text = mainName;
        subStageName.text = subName;
        StartCoroutine(StageName());
    } //ShowStageName

    private IEnumerator StageName()
    {
        stageUiObj.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        stageUiObj.SetActive(false);
    } //StageName

    //로딩창 출력 후 해당씬으로 이동
    public void ShowLoading(bool activeImage)
    {
        loadingObj.SetActive(activeImage);
    } //ShowLoading
}
