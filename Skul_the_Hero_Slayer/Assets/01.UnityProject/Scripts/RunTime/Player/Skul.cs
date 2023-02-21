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

    // Start is called before the first frame update
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
    void Start()
    {

    }

    public override void AttackA()
    {
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
        playerAni.SetBool("isSkillA", true);
        skillAObj = Instantiate(Resources.Load("Prefabs/SkulSkillAEffect") as GameObject);
        skillAObj.GetComponentMust<SkulSkillA>().Init(this);
        playerAni.runtimeAnimatorController = SkulHeadless;
    } //SkillA

    public override void SkillB()
    {
        playerController.player.transform.position = skillAObj.transform.position;
        playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
        if (skillAObj != null)
        {
            Destroy(skillAObj);
        }
    } //SkillB
}
