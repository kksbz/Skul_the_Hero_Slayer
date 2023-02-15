using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject mainUiObj = default;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //UIManager 초기화
    private void InitUIManager()
    {
        mainUiObj = gameObject.FindChildObj(GData.MAIN_UI_OBJ_NAME);
        loadingObj = gameObject.FindChildObj(GData.LOADDING_OBJ_NAME);
    } //InitUIManager

    //로딩창 출력 후 해당씬으로 이동
    public void ShowLoading(bool activeImage)
    {
        loadingObj.SetActive(activeImage);
    } //ShowLoading
}
