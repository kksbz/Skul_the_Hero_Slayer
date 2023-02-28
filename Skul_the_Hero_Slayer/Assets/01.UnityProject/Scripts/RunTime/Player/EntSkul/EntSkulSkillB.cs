using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntSkulSkillB : MonoBehaviour
{
    private Animator entSkillAni;
    private int minDamage = 27;
    private int maxDamage = 35;
    // Start is called before the first frame update
    void Start()
    {
        entSkillAni = gameObject.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entSkillAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {
            gameObject.GetComponentMust<BoxCollider2D>().enabled = false;
        }
        if (entSkillAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"스킬B트리거 발동? {collider.name}, {collider.tag}");
        if (collider.tag == GData.ENEMY_LAYER_MASK)
        {
            BossHead boss = collider.gameObject?.GetComponentMust<BossHead>();
            //보스몬스터 데미지 판정처리
            int damage = Random.RandomRange(minDamage + 3, maxDamage + 5);
            if (boss != null)
            {
                boss.hp -= damage;
                Debug.Log($"Ent스킬B공격 = {boss.hp}/{boss.maxHp}");
                GameManager.Instance.totalDamage += damage;
            }
            MonsterController target = collider.gameObject?.GetComponentMust<MonsterController>();
            //몬스터 데미지 판정처리
            if (target != null)
            {
                target.monster.hp -= damage;
                Debug.Log($"Ent스킬B공격 = {target.monster.hp}/{target.monster.maxHp}");
                GameManager.Instance.totalDamage += damage;
            }
        }
    } //OnTriggerEnter2D
}
