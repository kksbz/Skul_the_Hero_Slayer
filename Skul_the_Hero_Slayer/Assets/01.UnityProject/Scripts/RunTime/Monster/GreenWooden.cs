using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    private Animator greenWoodenAni;
    private Vector3 localScale;

    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("GreenWooden") as MonsterData;
        InitMonsterData(monsterData);
        //몬스터컨트롤러에서 몬스터타입 변수로 접근가능하게 하기 위한 처리
        //몬스터컨트롤러의 몬스터타입 변수에 자신을 몬스터로 캐스팅하여 저장
        monsterController.monster = (Monster)(this as Monster);
        greenWoodenAni = gameObject.GetComponentMust<Animator>();
    }

    //공격하는 함수(공격을 애니메이션 이벤트로 처리함)
    public override void AttackA()
    {
        //탐색레이어가 타겟을 감지했을 때
        if (monsterController.monster.tagetSearchRay.hit != null)
        {
            monsterController.monster.monsterAudio.clip = monsterController.monster.attackASound;
            monsterController.monster.monsterAudio.Play();
            //원거리 공격 => 투사체오브젝트를 생성
            GameObject thornAttack = Instantiate(Resources.Load("Prefabs/Monster/GreenWooden_Thorn") as GameObject);
            //투사체의 위치를 타겟위치로 설정
            thornAttack.gameObject.transform.position = monsterController.monster.tagetSearchRay.hit.transform.position;
            //투사체의 부모가 자신임을 알려주기위한 처리
            thornAttack.GetComponent<GreenWoodenAttackA>().Init(this);
        }
    } //AttackA

    //타겟을 바라보는 방향전환 함수
    private void lookTarget()
    {
        Vector3 targetPos = monsterController.monster.tagetSearchRay.hit.transform.position;
        //타겟과 나의 거리를 노멀라이즈하여 타겟의 방향구함
        Vector3 targetDirection = (targetPos - monsterController.monster.transform.position).normalized;
        localScale = monsterController.monster.transform.localScale;
        //targetDirection.x의 값이 0보다 작으면 왼쪽, 크면 오른쪽
        if (targetDirection.x > 0)
        {
            localScale = new Vector3(1, localScale.y, localScale.z);
            monsterController.monster.groundCheckRay._isRight = true;
            monsterController.monster.transform.localScale = localScale;
        }
        else if (targetDirection.x < 0)
        {
            localScale = new Vector3(-1, localScale.y, localScale.z);
            monsterController.monster.groundCheckRay._isRight = false;
            monsterController.monster.transform.localScale = localScale;
        }
    } //lookTarget

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
        if (gameObject == null)
        {
            yield break;
        }
        //공격딜레이 중에는 idel모션 처리
        greenWoodenAni.SetBool("isAttackA", false);
        greenWoodenAni.SetBool("isIdle", true);
        yield return new WaitForSeconds(2f);
        //2초후 현재 상태가 공격이 아니라면 코루틴 종료 => 코루틴들어오고 상태가 변했을 경우 밑에 공격모션을 취소하기 위한 예외처리
        greenWoodenAni.SetBool("isIdle", false);
        if (monsterController.enumState != MonsterController.MonsterState.ATTACK)
        {
            Debug.Log($"2초후 상태{monsterController.enumState}");
            yield break;
        }
        lookTarget();
        greenWoodenAni.SetBool("isAttackA", true);
    } //AttackDelay
}
