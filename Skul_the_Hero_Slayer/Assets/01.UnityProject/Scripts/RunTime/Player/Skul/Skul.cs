using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;
    private RuntimeAnimatorController SkulHeadless;
    public System.Action onHeadBack;
    private GameObject skillAObj;

    void OnEnable()
    {
        //Action에 SkillA를 써서 런타임컨트롤러가 SkulHeadless로 바뀌고 해골을 줍지 못했을 경우 처리하기 위한 내용 저장
        onHeadBack += () => { playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC; };
        //SkillA를 썼을 경우 자신의 해골을 날려 SkulHeadless로 런타임애니컨트롤러를 변경하기 위한 초기화
        SkulHeadless = Resources.Load("Animation/PlayerAni/SkulHeadless/SkulHeadless") as RuntimeAnimatorController;
        playerController = gameObject.GetComponentMust<PlayerController>();
        playerData = Resources.Load("SkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
        Debug.Log("Skul");
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
        Vector2 attackArea = new Vector2(1.5f, 1.5f);
        //BoxcastAll로 Hit처리
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, attackArea, 0f, Vector2.zero, 0f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
            {
                BossHead boss = hit.collider.gameObject?.GetComponentMust<BossHead>();
                if (boss != null)
                {
                    boss.hp -= Random.RandomRange(20, 25);
                    Debug.Log($"스킬A공격 = {boss.hp}/{boss.maxHp}");
                }

                Monster monster = hit.collider.gameObject.GetComponentMust<Monster>();
                if (monster != null)
                {
                    monster.hp -= Random.Range(playerData.MinDamage, playerData.MaxDamage);
                    Debug.Log($"{hit.collider.name}={monster.hp}/{monster.maxHp}");
                }
            }
            GameObject hitEffect = Instantiate(Resources.Load("Prefabs/Effect/HitEffect") as GameObject);
            hitEffect.transform.position = hit.transform.position - new Vector3(0f, 0.5f, 0f);
            hitEffect.transform.localScale = new Vector2(playerController.player.transform.localScale.x,
             hitEffect.transform.localScale.y);
        }
    } //AttackAandB

    public override void SkillA()
    {

    } //SkillA

    public override void SkillB()
    {

    } //SkillB

    public void SkulSkillA()
    {
        skillAObj = Instantiate(Resources.Load("Prefabs/SkulSkillAEffect") as GameObject);
        skillAObj.GetComponentMust<SkulSkillA>().Init(this);
        playerAni.runtimeAnimatorController = SkulHeadless;
    } //SkulSkillA

    public void SkulSkillB()
    {
        Debug.Log("스킬B 들어옴?");
        playerController.player.transform.position = skillAObj.transform.position;
        playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
        if (skillAObj != null)
        {
            Destroy(skillAObj);
        }
    } //SkulSkillB
}
