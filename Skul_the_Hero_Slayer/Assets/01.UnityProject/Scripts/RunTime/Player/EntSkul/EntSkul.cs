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
        AttackAandB();
    } //AttackA

    public override void AttackB()
    {
        AttackAandB();
    } //AttackB

    //공격A,B의 히트 판정 처리하는 함수
    private void AttackAandB()
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
                    Debug.Log($"엔트스컬 민뎀={playerData.MinDamage},맥뎀={playerData.MaxDamage}");
                    Debug.Log($"{boss.name}={boss.hp}/{boss.maxHp}");
                }

                Monster monster = hit.collider.gameObject?.GetComponentMust<Monster>();
                if (monster != null)
                {
                    monster.hp -= Random.RandomRange(playerData.MinDamage, playerData.MaxDamage);
                    Debug.Log($"엔트스컬 민뎀={playerData.MinDamage},맥뎀={playerData.MaxDamage}");
                    Debug.Log($"{monster._name}={monster.hp}/{monster.maxHp}");
                }
            }
        }
    } //AttackAandB
    public override void SkillA()
    {

    } //SkillA
    public override void SkillB()
    {

    } //SkillB

    //EntSkillA 함수
    public void EntSkillA()
    {
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
        GameObject entSkillB = Instantiate(Resources.Load("Prefabs/EntSkillB") as GameObject);
        float directionX = playerController.player.transform.localScale.x;
        //SkillB 오브젝트의 위치와 방향 설정
        entSkillB.transform.position = playerController.player.transform.position;
        entSkillB.transform.localScale = new Vector3(directionX, 1, 1);
    } //EntSkillB
}
