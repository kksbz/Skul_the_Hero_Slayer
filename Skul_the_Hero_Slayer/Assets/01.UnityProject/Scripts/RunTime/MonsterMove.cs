using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMonsterState
{
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        mController = _mController;
        
        Debug.Log($"몬스터상태클래스 : move 들어왔음 이름: {mController.monster._name}");
    }
    public void StateFixedUpdate()
    {
        
    }
    public void StateUpdate()
    {
        mController.monster.monsterRb.AddForce(Vector2.left * 1f);
    }
    public void StateExit()
    {
        if(mController.monster.transform.position.x <= 5f)
        {
            Debug.Log("move상태 탈출");
        }
    }
}
