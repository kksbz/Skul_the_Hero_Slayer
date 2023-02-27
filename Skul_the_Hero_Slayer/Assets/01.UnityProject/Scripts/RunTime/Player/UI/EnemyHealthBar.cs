using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject hpBarPrefab;
    public GameObject canvas;
    RectTransform hpBar;
    public float offset = -1f;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = Instantiate(Resources.Load("Prefabs/Monster/MonsterHealthBar") as GameObject, canvas.transform).GetComponentMust<RectTransform>();
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + offset, 0));
        hpBar.position = _hpBarPos;
    }
}
