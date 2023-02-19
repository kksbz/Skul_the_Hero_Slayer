using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : IMonsterState
{
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.ATTACK;

        //공격타입에 따른 시작공격 지정
        if (mController.monster.hasAdditionalAttack == true)
        {
            Debug.Log($"{mController.monster._name}공격시작B");
            mController.monster.monsterAni.SetBool("isAttackB", true);
        }
        else
        {
            Debug.Log($"{mController.monster._name}공격시작A");
            mController.monster.monsterAni.SetBool("isAttackA", true);
        }
    }
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }
    public void StateUpdate()
    {
        //현재 진행중인 애니메이션이 끝나지 않으면 리턴
        if (mController.monster.monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            return;
        }
        //공격타입이 1개면 리턴
        if (mController.monster.hasAdditionalAttack == false)
        {
            return;
        }
        ChageAttackType();
    }
    public void StateExit()
    {
        if(mController.monster.hasAdditionalAttack == false)
        {
            mController.monster.monsterAni.SetBool("isAttackA", false);
        }
        mController.monster.monsterAni.SetBool("isAttackA", false);
        mController.monster.monsterAni.SetBool("isAttackB", false);
    }

    //타겟의 거리를 측정하여 공격타입을 바꾸는 함수
    private void ChageAttackType()
    {
        Vector3 targetPos = mController.monster.tagetSearchRay.hit.transform.position;
        float distance = Vector2.Distance(targetPos, mController.monster.transform.position);
        //타겟과 자신의 거리가 근접공격거리 보다 작거나같으면 AttackA, 크면 AttackB 실행
        if (distance <= mController.monster.meleeAttackRange)
        {
            mController.monster.monsterAni.SetBool("isAttackB", false);
            mController.monster.monsterAni.SetBool("isAttackA", true);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isAttackA", false);
            mController.monster.monsterAni.SetBool("isAttackB", true);
        }
    } //ChageAttackType
}
