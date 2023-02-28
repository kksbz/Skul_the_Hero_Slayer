using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDead : IMonsterState
{
    private MonsterController mController;
    private GameObject deadEffect;
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.DEAD;
        mController.monster.monsterAudio.clip = mController.monster.deadSound;
        mController.monster.monsterAudio.Play();
        deadEffect = GameObject.Instantiate(Resources.Load("Prefabs/Effect/EnemyDead")) as GameObject;
        deadEffect.transform.position = mController.monster.transform.position;
        deadEffect.SetActive(true);
        mController.monster.gameObject.SetActive(false);
    }
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }
    public void StateUpdate()
    {
        /*Do Nothing*/
    }
    public void StateExit()
    {
        /*Do Nothing*/
    }
}
