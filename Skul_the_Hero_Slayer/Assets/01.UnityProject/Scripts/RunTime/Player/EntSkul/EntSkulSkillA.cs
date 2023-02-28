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
        //Hit상태면 콜라이더 비활성화
        if (isHit == true)
        {
            gameObject.GetComponentMust<BoxCollider2D>().enabled = false;
        }
        //애니메이션이 종료되면 파괴
        if (entSkillAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    } //Update

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"스킬A트리거 발동? {collider.name}, {collider.tag}");
        if (collider.tag == GData.ENEMY_LAYER_MASK)
        {
            BossHead boss = collider.gameObject?.GetComponentMust<BossHead>();
            //보스몬스터 데미지 판정처리
            int damage = Random.RandomRange(25, 30);
            if (boss != null)
            {
                boss.hp -= damage;
                Debug.Log($"Ent스킬A공격 = {boss.hp}/{boss.maxHp}");
                isHit = true;
                GameManager.Instance.totalDamage += damage;
            }
            MonsterController target = collider.gameObject?.GetComponentMust<MonsterController>();
            //몬스터 데미지 판정처리
            if (target != null)
            {
                target.monster.hp -= damage;
                Debug.Log($"Ent스킬A공격 = {target.monster.hp}/{target.monster.maxHp}");
                isHit = true;
                GameManager.Instance.totalDamage += damage;
            }
        }
    }
}
