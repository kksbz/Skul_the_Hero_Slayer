using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    private BossMonster bossObj;
    private Animator bossHeadAni;
    private GameObject groggyEffect;
    private GameObject slamEffect;
    public int maxHp { get => bossObj.maxHp; }
    public int hp { get => bossObj.hp; set => bossObj.hp = value; }
    // Start is called before the first frame update
    void Start()
    {
        bossObj = gameObject.transform.parent.GetComponent<BossMonster>();
        bossHeadAni = gameObject.GetComponentMust<Animator>();
        groggyEffect = GFunc.GetRootObj("BossEffect").FindChildObj("GroggyEffect");
        slamEffect = GFunc.GetRootObj("BossEffect").FindChildObj("FistSlamEffect");
    }

    void Update()
    {

    }

    //1페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    public void P1ExitAttack()
    {
        bossHeadAni.SetBool("isAttackA", false);
        bossHeadAni.SetBool("isRightAttack", false);
        bossHeadAni.SetBool("isLeftAttack", false);
        bossHeadAni.SetBool("isAttackC", false);
        bossHeadAni.SetBool("isFistSlam", false);
        bossHeadAni.SetBool("isGroggy", false);
    } //P1ExitAttack

    //2페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    public void P2ExitAttack()
    {
        bossHeadAni.SetBool("isP2AttackA", false);
        bossHeadAni.SetBool("isP2RightAttack", false);
        bossHeadAni.SetBool("isP2LeftAttack", false);
        bossHeadAni.SetBool("isP2AttackC", false);
        bossHeadAni.SetBool("isP2FistSlam", false);
        bossHeadAni.SetBool("isP2Groggy", false);
        bossHeadAni.SetBool("isP2Idle", true);
    } //P2ExitAttack

    public void ChangePhase()
    {
        bossHeadAni.SetBool("isChangePhase", false);
        bossHeadAni.SetBool("isP2Idle", true);
    } //ExitChangePhase

    public void Dead()
    {
        bossHeadAni.SetBool("isP2AttackA", false);
        bossHeadAni.SetBool("isP2RightAttack", false);
        bossHeadAni.SetBool("isP2LeftAttack", false);
        bossHeadAni.SetBool("isP2AttackC", false);
        bossHeadAni.SetBool("isP2FistSlam", false);
        bossHeadAni.SetBool("isP2Groggy", false);
    } //Dead

    //Groggy이펙트 활성화하는 함수
    public void OnOffGroggyEffect()
    {
        if (!groggyEffect.activeInHierarchy)
        {
            groggyEffect.SetActive(true);
        }
        else
        {
            groggyEffect.SetActive(false);
        }
    } //OnOffGroggyEffect

    //FistSlam이펙트 활성화하는 함수
    public void OnOffFistSlamEffect()
    {
        slamEffect.SetActive(true);
    } //OnOffFistSlamEffect
}
