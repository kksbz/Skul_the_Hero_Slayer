using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : IMonsterState
{
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        //요기서 들어가고
    }
    public void StateFixedUpdate()
    {

    }
    public void StateUpdate()
    {
        // string test = mController.test;
        // Debug.Log(test);
        // mController.monster.AddForce(Vector2.right * 1f);
    }
    public void StateExit()
    {
        Debug.Log("Idle 나간다");
        //요기서 나가는거
    }
}
