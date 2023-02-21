using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;
    private Animator skulAni;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponentMust<PlayerController>();
        playerData = Resources.Load("SkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
        skulAni = gameObject.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void AttackA()
    {
        Debug.Log("스컬공격A들옴?");
        Vector2 attackArea = new Vector2(1.5f, 1.5f);
        //BoxcastAll로 Hit처리
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, attackArea, 0f, Vector2.zero, 0f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
            {
                Monster monster = hit.collider.gameObject.GetComponentMust<Monster>();
                monster.hp -= Random.Range(playerData.MinDamage, playerData.MaxDamage);
                Debug.Log($"{hit.collider.name}={monster.hp}/{monster.maxHp}");
            }
        }
    } //AttackA

    public override void AttackB()
    {

    } //AttackB

    public override void SkillA()
    {
        Debug.Log("스컬 스킬A사용?");
        GameObject skillAObj = Instantiate(Resources.Load("Prefabs/SkulSkillAEffect") as GameObject);
        skillAObj.GetComponentMust<SkulSkillA>().Init(this);
    } //SkillA

    public override void SkillB()
    {

    } //SkillB
}
