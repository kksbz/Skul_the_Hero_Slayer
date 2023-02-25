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
                Monster monster = hit.collider.gameObject.GetComponentMust<Monster>();
                monster.hp -= Random.Range(playerData.MinDamage, playerData.MaxDamage);
                Debug.Log($"엔트스컬 민뎀={playerData.MinDamage},맥뎀={playerData.MaxDamage}");
                Debug.Log($"{hit.collider.name}={monster.hp}/{monster.maxHp}");
            }
        }
    } //AttackAandB
    public override void SkillA()
    {

    } //SkillA

    public void EntSkillA()
    {
        Vector2 direction = new Vector2(transform.localScale.x, 0f).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 10f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        int number = 0;
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

    public override void SkillB()
    {

    } //SkillB
}
