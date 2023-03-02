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
            //Debug.Log($"{mController.monster._name}공격시작B");
            mController.monster.monsterAni.SetBool("isAttackB", true);
        }
        else
        {
            //Debug.Log($"{mController.monster._name}공격시작A");
            mController.monster.monsterAni.SetBool("isAttackA", true);
        }
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }
    public void StateUpdate()
    {
        /*Do Nothing*/
    } //StateUpdate
    public void StateExit()
    {
        if (mController.monster.hasAdditionalAttack == false)
        {
            mController.monster.monsterAni.SetBool("isAttackA", false);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isAttackA", false);
            mController.monster.monsterAni.SetBool("isAttackB", false);
        }
    } //StateExit
}
