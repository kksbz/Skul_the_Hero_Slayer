using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMonsterState
{
    private int offsetX; //이동방향 변수
    private bool exitState; //코루틴 while문 탈출조건
    private bool isRight = true; //바라보는 방향 변수
    private Vector3 localScale;
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        mController = _mController;
        mController.monster.monsterAni.SetBool("isWalk", true);
        exitState = false;
        localScale = mController.transform.localScale;
        mController.CoroutineDeligate(randomPosX());
    } //StateEnter
    public void StateFixedUpdate()
    {

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
        if (mController.monster._name == "BigWooden")
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
        if(offsetX == 0)
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
        if (isRight && offsetX < 0 || !isRight && offsetX > 0)
        {
            isRight = !isRight;
            //raycast방향도 같이 전환
            mController.monster.groundCheckRay._isRight = !mController.monster.groundCheckRay._isRight;
            localScale.x *= -1;
            mController.transform.localScale = localScale;
        }
    } //ChangLookDirection

    //타일맵 끝에 닿았을 때 반대방향으로 전환하는 함수
    private void GroundCheck()
    {
        if (mController.monster.groundCheckRay.hit.collider == null)
        {
            offsetX *= -1;
            isRight = !isRight;
            mController.monster.groundCheckRay._isRight = !mController.monster.groundCheckRay._isRight;
            localScale.x *= -1;
            mController.transform.localScale = localScale;
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
            offsetX = Random.RandomRange(-1, 2);
            yield return new WaitForSeconds(3f);
        }
    } //randomPosX
}
