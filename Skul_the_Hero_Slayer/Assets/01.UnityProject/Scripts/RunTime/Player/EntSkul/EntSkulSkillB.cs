using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntSkulSkillB : MonoBehaviour
{
    private Animator entSkillAni;
    private int minDamage = 30;
    private int maxDamage = 40;
    // Start is called before the first frame update
    void Start()
    {
        entSkillAni = gameObject.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entSkillAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    }

    //EntSkillB공격 판정 함수
    private void EntSkillBAttack()
    {
        Vector2 attackArea = new Vector2(5f, 3f);
        Vector2 direction = new Vector2(transform.localScale.x, 0).normalized;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, attackArea / 2f, 0f, direction, 3f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
            {
                int damage = Random.Range(minDamage, maxDamage + 1);
                BossHead boss = hit.collider.gameObject?.GetComponentMust<BossHead>();
                if (boss != null)
                {
                    boss.hp -= damage;
                    // Debug.Log($"엔트스킬B 공격:{boss.name}={boss.hp}/{boss.maxHp}");
                    GameManager.Instance.totalDamage += damage;
                }

                Monster monster = hit.collider.gameObject?.GetComponentMust<Monster>();
                if (monster != null)
                {
                    monster.hp -= damage;
                    // Debug.Log($"엔트스킬B 공격:{monster._name}={monster.hp}/{monster.maxHp}");
                    GameManager.Instance.totalDamage += damage;
                }
                GameObject hitEffect = Instantiate(Resources.Load("Prefabs/Effect/HitEffect") as GameObject);
                hitEffect.transform.position = hit.transform.position - new Vector3(0f, 0.5f, 0f);
                hitEffect.transform.localScale = new Vector2(transform.localScale.x,
                hitEffect.transform.localScale.y);
            }
        }
    }

    // //AttackB 공격범위 기즈모
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireCube(transform.position + new Vector3(3f * transform.localScale.x, 0, 0), new Vector2(5f, 3f));
    // } //OnDrawGizmos
}
