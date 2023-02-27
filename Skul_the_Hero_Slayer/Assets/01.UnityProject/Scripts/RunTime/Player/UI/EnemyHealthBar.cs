using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject canvas;
    private MonsterController mController;
    private RectTransform hpBarTransform;
    private Image hpBar;
    private float offset;
    private float widthOffset;
    private int hp;
    private int maxHp;
    // Start is called before the first frame update
    void Start()
    {
        mController = gameObject.GetComponentMust<MonsterController>();
        offset = mController.monster.hpBarPosY;
        widthOffset = mController.monster.hpBarWidth;
        maxHp = mController.monster.maxHp;
        canvas = gameObject.FindChildObj("Canvas");
        hpBarTransform = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Monster/MonsterHealthBar"), canvas.transform).GetComponentMust<RectTransform>();
        hpBarTransform.sizeDelta = new Vector2(hpBarTransform.rect.width * widthOffset, hpBarTransform.rect.height);
        hpBar = canvas.FindChildObj("MonsterHealthBar(Clone)").FindChildObj("MonsterHealth").GetComponentMust<Image>();
        canvas.SetActive(false);
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + offset, 0));
        hpBarTransform.position = _hpBarPos;
        hp = mController.monster.hp;
        hpBar.fillAmount = (float)hp / (float)maxHp;
    }
}
