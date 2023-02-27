using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Camera miniMapCamera;
    private GameObject tagetPos;
    // Start is called before the first frame update
    void Start()
    {
        miniMapCamera = gameObject.GetComponentMust<Camera>();
        tagetPos = GFunc.GetRootObj("Player");
    }

    // Update is called once per frame
    void Update()
    {
        miniMapCamera.transform.position = new Vector3(tagetPos.transform.position.x, tagetPos.transform.position.y, miniMapCamera.transform.position.z);
    }
}
