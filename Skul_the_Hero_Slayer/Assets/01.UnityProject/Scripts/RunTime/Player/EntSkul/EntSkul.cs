using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntSkul : Player
{

    public PlayerData playerData;
    private PlayerController playerController;
    private GameObject skillAObj;
    void OnEnable()
    {
        playerController = gameObject.GetComponentMust<PlayerController>();
        playerData = Resources.Load("EntSkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
    }

    public override void AttackA()
    {
        /*Do Nothing*/
    } //AttackA

    public override void AttackB()
    {
        /*Do Nothing*/
    } //AttackB

    private void EntAttackA()
    {
        EntAttack();
        playerAudio.clip = atkASound;
        playerAudio.Play();
    } //EntAttackA
    private void EntAttackB()
    {
        EntAttack();
        playerAudio.clip = atkBSound;
        playerAudio.Play();
    } //EntAttackA
    private void EntJumpAttack()
    {
        EntAttack();
        playerAudio.clip = jumpAtkSound;
        playerAudio.Play();
    } //EntJumpAttack

    //공격A,B의 히트 판정 처리하는 함수
    private void EntAttack()
    {
        Vector2 attackArea = new Vector2(2.5f, 1.5f);
        //BoxcastAll로 Hit처리
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, attackArea, 0f, Vector2.zero, 0f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
            {
                BossHead boss = hit.collider.gameObject?.GetComponentMust<BossHead>();
                if (boss != null)
                {
                    boss.hp -= Random.RandomRange(playerData.MinDamage, playerData.MaxDamage);
                    Debug.Log($"{boss.name}={boss.hp}/{boss.maxHp}");
                }

                Monster monster = hit.collider.gameObject?.GetComponentMust<Monster>();
                if (monster != null)
                {
                    monster.hp -= Random.RandomRange(playerData.MinDamage, playerData.MaxDamage);
                    Debug.Log($"{monster._name}={monster.hp}/{monster.maxHp}");
                }
                GameObject hitEffect = Instantiate(Resources.Load("Prefabs/Effect/HitEffect") as GameObject);
                hitEffect.transform.position = hit.transform.position - new Vector3(0f, 0.5f, 0f);
                hitEffect.transform.localScale = new Vector2(playerController.player.transform.localScale.x,
                hitEffect.transform.localScale.y);
            }
        }
    } //AttackAandB
    public override void SkillA()
    {
        /*Do Nothing*/
    } //SkillA
    public override void SkillB()
    {
        /*Do Nothing*/
    } //SkillB

    //EntSkillA 함수
    public void EntSkillA()
    {
        playerAudio.clip = skillASound;
        playerAudio.Play();
        Vector2 direction = new Vector2(transform.localScale.x, 0f).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 10f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        int number = 0;
        //사거리 안에 있는 적 최대 3명에게 공격
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && number < 3)
            {
                GameObject fist = Instantiate(Resources.Load("Prefabs/EntSkillA") as GameObject);
                fist.transform.position = hits[i].collider.transform.position;
                number += 1;
            }
        }
    } //EntSkillA

    //EntSkillB 함수
    public void EntSkillB()
    {
        playerAudio.clip = skillBSound;
        playerAudio.Play();
        GameObject entSkillB = Instantiate(Resources.Load("Prefabs/EntSkillB") as GameObject);
        float directionX = playerController.player.transform.localScale.x;
        //SkillB 오브젝트의 위치와 방향 설정
        entSkillB.transform.position = playerController.player.transform.position;
        entSkillB.transform.localScale = new Vector3(directionX, 1, 1);
    } //EntSkillB
}
