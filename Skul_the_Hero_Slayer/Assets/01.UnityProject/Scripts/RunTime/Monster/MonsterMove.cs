using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMonsterState
{
    private int offsetX; //이동방향 변수
    private bool exitState; //코루틴 while문 탈출조건
    private Vector3 localScale; //바라보는방향 전환 변수
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        mController = _mController;
        mController.enumState = MonsterController.MonsterState.MOVE;
        // Debug.Log($"{mController.monster.name}이동시작");
        mController.monster.monsterAni.SetBool("isWalk", true);
        exitState = false;
        localScale = mController.monster.transform.localScale;
        mController.CoroutineDeligate(randomPosX());
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate

    public void StateUpdate()
    {
        Move();
    } //StateUpdate
    public void StateExit()
    {
        exitState = true;
        mController.monster.monsterAni.SetBool("isWalk", false);
    } //StateExit

    //몬스터 이동시키는 함수
    private void Move()
    {
        if (mController.monster.moveSpeed == 0)
        {
            return;
        }
        ChangeIdleAni();
        ChangLookDirection();
        GroundCheck();

        mController.monster.transform.Translate(new Vector2(offsetX, 0f) * mController.monster.moveSpeed * Time.deltaTime);
    } //Move

    //offsetX값이 0일때 idle애니로 전환하는 함수
    private void ChangeIdleAni()
    {
        if (offsetX == 0)
        {
            mController.monster.monsterAni.SetBool("isWalk", false);
            mController.monster.monsterAni.SetBool("isIdle", true);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isIdle", false);
            mController.monster.monsterAni.SetBool("isWalk", true);
        }
    } //ChangeIdleAni

    //이동할 방향으로 바라보는 방향 전환하는 함수
    private void ChangLookDirection()
    {
        //offsetX값에 따라 바라보는 방향처리
        if (offsetX != 0)
        {
            //offsetX값이 0보다 작으면 왼쪽, 0보다 크면 오른쪽
            if (offsetX < 0)
            {
                //raycast방향도 같이 전환
                mController.monster.groundCheckRay._isRight = false;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
            else if (offsetX > 0)
            {
                mController.monster.groundCheckRay._isRight = true;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
        }
    } //ChangLookDirection

    //타일맵 끝에 닿았을 때 반대방향으로 전환하는 함수
    private void GroundCheck()
    {
        //그라운드체크레이어가 땅을 감지하지 못할 경우 반대방향으로 전환
        if (mController.monster.groundCheckRay.hit.collider == null)
        {
            if (localScale.x < 0)
            {
                mController.monster.groundCheckRay._isRight = true;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
                offsetX *= -1;
            }
            else if (localScale.x > 0)
            {
                mController.monster.groundCheckRay._isRight = false;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
                offsetX *= -1;
            }
        }
    } //GroundCheck

    //2초마다 이동시킬 방향을 정하고 바라보는 방향을 바꾸는 코루틴함수
    private IEnumerator randomPosX()
    {
        while (exitState == false)
        {
            if (exitState)
            {
                yield break;
            }
            offsetX = Random.Range(-1, 1 + 1);
            yield return new WaitForSeconds(3f);
        }
    } //randomPosX
}
