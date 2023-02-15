using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
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
    
    public void LoadAsyncScene(string sceneName)
    {
        StartCoroutine(GetLoading(sceneName));
    } //LoadYourAsyncScene

    //로딩창 출력 후 해당씬으로 이동 코루틴
    private IEnumerator GetLoading(string sceneName)
    {
        UIManager.Instance.ShowLoading(true);
        yield return new WaitForSeconds(1.5f);
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(asyncLoad.isDone);
        UIManager.Instance.ShowLoading(false);
    } //GetLoading
}
