using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagetSearch : IMonsterState
{
    private MonsterController mController;
    private Vector3 targetPos;
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        Debug.Log($"{mController.monster.name}서치타겟 시작");
        mController.enumState = MonsterController.MonsterState.SEARCH;
        mController.monster.monsterAni.SetBool("isWalk", true);
    }
    public void StateFixedUpdate()
    {

    }
    public void StateUpdate()
    {
        LookAndFollowTaget();
    }

    //타겟을 쫓아가는 함수
    private void LookAndFollowTaget()
    {
        targetPos = mController.monster.tagetSearchRay.hit.transform.position;
        Vector3 targetDirection = (targetPos - mController.monster.transform.position).normalized;
        //타겟과 자신의 거리의 x값 위치를 비교해 바라보는방향 및 그라운드체크레이어 방향 전환
        if (targetDirection.x != 0)
        {
            //targetDirection.x가 0보다 작으면 타겟은 왼쪽, 0보다 크면 타겟은 오른쪽에 있음
            if (targetDirection.x < 0)
            {
                mController.monster.groundCheckRay._isRight = false;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
            else if (targetDirection.x > 0)
            {
                mController.monster.groundCheckRay._isRight = true;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
        }
        mController.monster.transform.Translate(new Vector3(targetDirection.x, 0, targetDirection.z) * mController.monster.moveSpeed * Time.deltaTime);
    }
    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isWalk", false);
    }
}
