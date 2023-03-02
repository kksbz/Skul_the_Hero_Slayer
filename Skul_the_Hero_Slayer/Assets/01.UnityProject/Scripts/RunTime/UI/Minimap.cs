using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Camera miniMapCamera;
    private GameObject tagetPos;
    private string thisSceneName;
    // Start is called before the first frame update
    void Start()
    {
        miniMapCamera = gameObject.GetComponentMust<Camera>();
        tagetPos = GFunc.GetRootObj("Player");
        thisSceneName = SceneMgr.Instance.GetThisSceneName();
    }

    // Update is called once per frame
    void Update()
    {
        //씬이 바뀌면 플레이어를 다시 찾음
        if (thisSceneName != SceneMgr.Instance.GetThisSceneName())
        {
            tagetPos = GFunc.GetRootObj("Player");
            thisSceneName = SceneMgr.Instance.GetThisSceneName();
        }
        if (SceneMgr.Instance.GetThisSceneName() != GData.TITLE_SCENE_NAME)
        {
            miniMapCamera.transform.position = new Vector3(tagetPos.transform.position.x, tagetPos.transform.position.y, miniMapCamera.transform.position.z);
        }
    }
}
