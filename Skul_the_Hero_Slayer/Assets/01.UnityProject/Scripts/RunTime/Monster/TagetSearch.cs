using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagetSearch : IMonsterState
{
    private MonsterController mController;
    private Vector2 tagetPos;
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.monster.monsterAni.SetBool("isWalk", true);
    }
    public void StateFixedUpdate()
    {

    }
    public void StateUpdate()
    {
        mController.monster.transform.Translate(tagetPos * mController.monster.moveSpeed * Time.deltaTime);
    }
    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isWalk", false);
    }
}
