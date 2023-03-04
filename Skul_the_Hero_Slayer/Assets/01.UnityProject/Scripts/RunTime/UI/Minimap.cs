using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Camera miniMapCamera;
    private GameObject targetPos;
    private string thisSceneName;
    void OnEnable()
    {
        miniMapCamera = gameObject.GetComponentMust<Camera>();
        targetPos = GFunc.GetRootObj(GData.PLAYER_LAYER_MASK);
    }

    // Update is called once per frame
    void Update()
    {
        miniMapCamera.transform.position = new Vector3(targetPos.transform.position.x, targetPos.transform.position.y, -13f);
    }
}
