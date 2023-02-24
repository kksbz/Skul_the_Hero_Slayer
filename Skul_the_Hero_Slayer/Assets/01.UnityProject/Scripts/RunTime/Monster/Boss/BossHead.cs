using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : BossMonster
{
    private BossMonster bossObj;
    private Animator bossHeadAni;
    // Start is called before the first frame update
    void Start()
    {
        bossHeadAni = gameObject.GetComponentMust<Animator>();
        hp = maxHp;
        Debug.Log($"{hp}/{maxHp}");
    }

    void Update()
    {
        if (hp <= 0f)
        {
            Debug.Log(isChangePhase);
            Debug.Log("들어옴?");
            isChangePhase = true;
            ChangePhase(true);
        }
    }

    public override void ChangePhase(bool ChangePhase)
    {
        base.ChangePhase(ChangePhase);
    }
    public void P1ExitAttack()
    {
        bossHeadAni.SetBool("isRightAttack", false);
        bossHeadAni.SetBool("isLeftAttack", false);
        bossHeadAni.SetBool("isAttackC", false);
        bossHeadAni.SetBool("isChangeP", false);
    } //P1ExitAttack

    public void P2ExitAttack()
    {
        bossHeadAni.SetBool("isP2RightAttack", false);
        bossHeadAni.SetBool("isP2LeftAttack", false);
        bossHeadAni.SetBool("isP2AttackC", false);
        bossHeadAni.SetBool("isP2Idle", true);
    } //P2ExitAttack
}
