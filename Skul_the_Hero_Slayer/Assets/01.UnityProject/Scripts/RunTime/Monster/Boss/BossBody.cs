using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : BossMonster
{
    private Animator bossBodyAni;
    // Start is called before the first frame update
    void Start()
    {
        bossBodyAni = gameObject.GetComponentMust<Animator>();
    } //Start

    public void ExitAttack()
    {
        bossBodyAni.SetBool("isRightAttack", false);
        bossBodyAni.SetBool("isLeftAttack", false);
        bossBodyAni.SetBool("isAttackC", false);
        bossBodyAni.SetBool("isChangeP", false);
    } //ExitAttack
}
