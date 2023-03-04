using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public string thisSceneName;
    private static SceneMgr instance = null;
    public static SceneMgr Instance
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

    //싱글톤 패턴 적용
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    public string GetThisSceneName()
    {
        thisSceneName = SceneManager.GetActiveScene().name;
        return thisSceneName;
    } //GetThisSceneName

    public void LoadAsyncScene(string sceneName)
    {
        StartCoroutine(GetLoading(sceneName));
    } //LoadYourAsyncScene

    //로딩창 출력 후 해당씬으로 이동 코루틴함수
    private IEnumerator GetLoading(string sceneName)
    {
        //메인UI끄고 로딩창 출력
        UIManager.Instance.mainUiObj.SetActive(false);
        UIManager.Instance.ShowLoading(true);
        yield return new WaitForSeconds(1.5f);
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        //현재씬이 타이틀씬이 아닐 경우 메인UI 출력
        if (GetThisSceneName() != GData.TITLE_SCENE_NAME)
        {
            UIManager.Instance.mainUiObj.SetActive(true);
            UIManager.Instance.skillACoolDown = UIManager.Instance.maxSkillACool;
            UIManager.Instance.skillBCoolDown = UIManager.Instance.maxSkillBCool;
        }
        UIManager.Instance.ShowLoading(false);
    } //GetLoading
}
