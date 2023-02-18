using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : IMonsterState
{
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        Debug.Log($"{mController.monster._name}공격시작");
        mController.enumState = MonsterController.MonsterState.ATTACK;

        //공격타입에 따른 시작공격 지정
        if (mController.monster.hasAdditionalAttack == true)
        {
            mController.monster.monsterAni.SetBool("isAttackB", true);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isAttackA", true);
        }
    }
    public void StateFixedUpdate()
    {

    }
    public void StateUpdate()
    {
        //공격타입이 1개면 리턴
        if (mController.monster.hasAdditionalAttack == false)
        {
            return;
        }
        //현재 진행중인 애니메이션이 끝나지 않으면 리턴
        if (mController.monster.monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            return;
        }
        ChageAttackType();
    }
    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isAttackA", false);
    }

    //타겟의 거리를 측정하여 공격타입을 바꾸는 함수
    private void ChageAttackType()
    {
        Vector3 targetPos = mController.monster.tagetSearchRay.hit.transform.position;
        float distance = Vector2.Distance(targetPos, mController.monster.transform.position);
        //타겟과 자신의 거리에 따른 공격타입 전환
        if (distance <= mController.monster.meleeAttackRange)
        {
            mController.monster.monsterAni.SetBool("isAttackA", true);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isAttackB", true);
        }
    } //ChageAttackType
}
