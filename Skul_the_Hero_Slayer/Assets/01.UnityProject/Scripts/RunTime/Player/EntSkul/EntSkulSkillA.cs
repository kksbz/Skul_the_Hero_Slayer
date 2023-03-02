using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntSkulSkillA : MonoBehaviour
{
    private Animator entSkillAni;
    private int minDamage = 30;
    private int maxDamage = 35;
    // Start is called before the first frame update
    void Start()
    {
        entSkillAni = gameObject.GetComponentMust<Animator>();
    } //Start

    // Update is called once per frame
    void Update()
    {
        //애니메이션이 종료되면 파괴
        if (entSkillAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    } //Update

    //EntSkillA공격 판정 함수
    private void EntSkillAAttack()
    {
        Vector2 attackArea = new Vector2(2f, 2.5f);
        Vector2 direction = new Vector2(transform.localScale.x, 0).normalized;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, attackArea, 0f, Vector2.zero, 0f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
        {
            int damage = Random.RandomRange(minDamage, maxDamage);
            BossHead boss = hit.collider.gameObject?.GetComponentMust<BossHead>();
            if (boss != null)
            {
                boss.hp -= damage;
                Debug.Log($"엔트스킬A 공격:{boss.name}={boss.hp}/{boss.maxHp}");
                GameManager.Instance.totalDamage += damage;
            }

            Monster monster = hit.collider.gameObject?.GetComponentMust<Monster>();
            if (monster != null)
            {
                monster.hp -= damage;
                Debug.Log($"엔트스킬A 공격:{monster._name}={monster.hp}/{monster.maxHp}");
                GameManager.Instance.totalDamage += damage;
            }
            GameObject hitEffect = Instantiate(Resources.Load("Prefabs/Effect/HitEffect") as GameObject);
            hitEffect.transform.position = hit.transform.position - new Vector3(0f, 0.5f, 0f);
            hitEffect.transform.localScale = new Vector2(transform.localScale.x,
            hitEffect.transform.localScale.y);
        }
    } //EntSkillAAttack

    //AttackB 공격범위 기즈모
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(2f, 2.5f));
    } //OnDrawGizmos
}
