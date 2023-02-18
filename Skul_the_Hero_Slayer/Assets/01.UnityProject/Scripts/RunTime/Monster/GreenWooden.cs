using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    private Animator greenWoodenAni;

    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("GreenWooden") as MonsterData;
        InitMonsterData(monsterData);
        monsterController.monster = (Monster)(this as Monster);
        greenWoodenAni = gameObject.GetComponentMust<Animator>();
    }

    //공격하는 함수(공격방식을 애니메이션 이벤트로 처리함)
    public override void AttackA()
    {
        //탐색레이어가 타겟을 감지했을 때
        if (monsterController.monster.tagetSearchRay.hit != null)
        {
            //원거리 공격 => 투사체오브젝트를 생성
            GameObject thornAttack = Instantiate(Resources.Load("Prefabs/GreenWooden_Thorn") as GameObject);
            //투사체의 위치를 타겟위치로 설정
            thornAttack.gameObject.transform.position = monsterController.monster.tagetSearchRay.hit.transform.position;
            //투사체의 부모가 자신임을 알려주기위한 처리
            thornAttack.GetComponent<ThornAttack>().Init(this);
        }
    } //AttackA

    //공격시 투사체가 사라질 때 까지 특정모션에서 대기하는 함수
    public void PauseAni()
    {
        greenWoodenAni.StartPlayback();
    } //PauseAni

    //투사체가 사라지면 멈췄던 모션을 다시 재생시키는 함수
    public void NotifyFinish()
    {
        greenWoodenAni.StopPlayback();
        StartCoroutine(AttackDelay());
    } //NotifyFinish

    //공격딜레이 정하는 코루틴 함수
    private IEnumerator AttackDelay()
    {
        //공격딜레이 중에는 idel모션 처리
        greenWoodenAni.SetBool("isAttackA", false);
        greenWoodenAni.SetBool("isIdle", true);
        yield return new WaitForSeconds(2f);
        greenWoodenAni.SetBool("isIdle", false);
        greenWoodenAni.SetBool("isAttackA", true);
    } //AttackDelay
}
