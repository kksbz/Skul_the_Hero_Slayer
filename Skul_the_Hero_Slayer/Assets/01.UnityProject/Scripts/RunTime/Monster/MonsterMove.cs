using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMonsterState
{
    private int offsetX;
    private float offsetY;
    private bool exitState;
    private bool isRight = false;
    private float timeCheck = 2f;
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        mController = _mController;
        exitState = false;
        // MonoBehaviour.StartCoroutine(randomPosX());
    }
    public void StateFixedUpdate()
    {
        if (isRight && offsetX < 0f || !isRight && offsetX > 0f)
        {
            if(mController.monster._name == "BigWooden")
            {
                return;
            }
            Vector3 localScale = mController.transform.localScale;
            isRight = !isRight;
            localScale.x *= -1f;
            mController.transform.localScale = localScale;
        }
        mController.monster.monsterRb.AddForce(new Vector2(offsetX, offsetY) * mController.monster.moveSpeed);
    }
    public void StateUpdate()
    {
        timeCheck += Time.deltaTime;
        if(timeCheck >= 2f)
        {
            timeCheck = 0f;
            offsetX = Random.RandomRange(-1, 2);
            offsetY = mController.monster.transform.position.y;
            Debug.Log(offsetX);
        }
    }
    public void StateExit()
    {
        exitState = true;
    }

    // private IEnumerator randomPosX()
    // {
    //     while (exitState == false)
    //     {
    //         offsetX = Random.RandomRange(-1, 2);
    //         offsetY = mController.monster.transform.position.y;
    //         Debug.Log(offsetX);
    //         yield return new WaitForSeconds(2f);
    //     }
    // }
}
