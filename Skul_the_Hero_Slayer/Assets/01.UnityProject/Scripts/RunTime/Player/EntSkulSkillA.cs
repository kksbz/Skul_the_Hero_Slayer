using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntSkulSkillA : MonoBehaviour
{
    private Animator entSkillAni;
    private bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        entSkillAni = gameObject.GetComponentMust<Animator>();
    } //Start

    // Update is called once per frame
    void Update()
    {
        if (isHit == true)
        {
            gameObject.GetComponentMust<BoxCollider2D>().enabled = false;
        }
        if (entSkillAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    } //Update

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == GData.ENEMY_LAYER_MASK)
        {
            BossHead boss = collider.gameObject?.GetComponentMust<BossHead>();
            if (boss != null)
            {
                boss.hp -= Random.RandomRange(25, 30);
                Debug.Log($"Ent스킬A공격 = {boss.hp}/{boss.maxHp}");
                isHit = true;
            }
            MonsterController target = collider.gameObject?.GetComponentMust<MonsterController>();
            if (target != null)
            {
                target.monster.hp -= Random.RandomRange(25, 30);
                Debug.Log($"Ent스킬A공격 = {target.monster.hp}/{target.monster.maxHp}");
                isHit = true;
            }
        }
    } //OnTriggerEnter2D
}
