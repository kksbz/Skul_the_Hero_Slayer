using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm : BossMonster
{
    private BoxCollider2D armCollider;
    private Animator armAni;

    // Start is called before the first frame update
    void Start()
    {
        armCollider = gameObject.GetComponentMust<BoxCollider2D>();
        armAni = gameObject.GetComponentMust<Animator>();
        armCollider.enabled = false;
    } //Start

    //Attack 애니메이션 진행 중 공격판정 시작위치에서 실행되는 함수
    public void Attack()
    {
        //BoxCollider2D를 꺼둔상태에서 애니메이션 트리거가 발동하면 BoxCollider2D를 켜서 트리거엔터발동판정
        armCollider.enabled = true;
    } //Attack

    //1페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    public void P1ExitAttack()
    {
        armAni.SetBool("isAttackA", false);
        armAni.SetBool("isAttackB", false);
        armAni.SetBool("isAttackC", false);
        armAni.SetBool("isWaitAttack", false);
        armCollider.enabled = false;
    } //ExitAttack

    //2페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    public void P2ExitAttack()
    {
        armAni.SetBool("isP2AttackA", false);
        armAni.SetBool("isP2AttackB", false);
        armAni.SetBool("isP2AttackC", false);
        armAni.SetBool("isP2WaitAttack", false);
        armAni.SetBool("isP2Idle", true);
        armCollider.enabled = false;
    } //P2ExitAttack

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == GData.PLAYER_LAYER_MASK)
        {
            PlayerController target = collider.gameObject?.GetComponentMust<PlayerController>();
            target.playerHp -= Random.RandomRange(minDamage, maxDamage);
            Debug.Log($"보스 공격A 플레이어 hp = {target.playerHp}/{target.playerMaxHp}");
            armCollider.enabled = false;
        }
    } //OnTriggerEnter2D

}
