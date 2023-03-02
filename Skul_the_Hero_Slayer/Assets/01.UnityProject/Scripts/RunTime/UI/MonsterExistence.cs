using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterExistence : MonoBehaviour
{
    TMP_Text monsterExistence;
    // Start is called before the first frame update
    void Start()
    {
        monsterExistence = gameObject.FindChildObj("MonsterExistence").GetComponentMust<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.monsterRemainingNumber <= 0)
        {
            monsterExistence.gameObject.SetActive(false);
        }
        else
        {
            monsterExistence.gameObject.SetActive(true);
            monsterExistence.text = $"몬스터:{GameManager.Instance.monsterRemainingNumber}";
        }
    }
}
