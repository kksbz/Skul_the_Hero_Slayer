using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public Transform tagetObj;
    private Vector2 offset;
    private RectTransform objRect = default;
    [SerializeField]
    private float offsetX = 0f;
    [SerializeField]
    private float offsetY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        objRect = gameObject.GetComponentMust<RectTransform>();
        offset = new Vector2(tagetObj.position.x + offsetX, tagetObj.position.y + offsetY);
        objRect.anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(4f, 1.7f, 0f));
    }
}
