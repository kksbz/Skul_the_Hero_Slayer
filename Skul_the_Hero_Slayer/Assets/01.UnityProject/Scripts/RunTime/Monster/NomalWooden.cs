using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    private Animator nomalWoodenAni;
    private RaycastHit2D hit;
    private float direction;
    private Vector3 attackdirection;

    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("NomalWooden") as MonsterData;
        InitMonsterData(monsterData);
        //몬스터컨트롤러에서 몬스터타입 변수로 접근가능하게 하기 위한 처리
        //몬스터컨트롤러의 몬스터타입 변수에 자신을 몬스터로 캐스팅하여 저장
        monsterController.monster = (Monster)(this as Monster);
        nomalWoodenAni = gameObject.GetComponent<Animator>();
    } //Awake

    //공격하는 함수(공격을 애니메이션 이벤트로 처리함)
    public override void AttackA()
    {
        Vector2 attackArea = new Vector2(0.5f, 1.5f);
        direction = transform.localScale.x;
        attackdirection = new Vector3(direction, 0f).normalized;
        //Boxcast로 피격처리
        hit = Physics2D.BoxCast(transform.position, attackArea, 0f, attackdirection, 1f, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
        if (hit.collider != null)
        {
            PlayerController target = hit.collider.gameObject.GetComponentMust<PlayerController>();
            target.playerHp -= Random.RandomRange(monsterData.MinDamage, monsterData.MaxDamage);
            Debug.Log($"노말우든 공격! 플레이어 hp = {target.playerHp}/{target.playerMaxHp}");
        }
    } //AttackA

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (attackdirection * 1f), new Vector2(0.5f, 1.5f));
    } //OnDrawGizmos

    //공격애니메이션이 종료되면 코루틴 실행
    public void ExitAttack()
    {
        StartCoroutine(AttackDelay());
    } //ExitAttack

    //공격딜레이 정하는 코루틴 함수
    private IEnumerator AttackDelay()
    {
        //공격딜레이 중에는 idel모션 처리
        nomalWoodenAni.SetBool("isAttackA", false);
        nomalWoodenAni.SetBool("isIdle", true);
        yield return new WaitForSeconds(2f);
        nomalWoodenAni.SetBool("isIdle", false);
        //2초후 현재 상태가 공격이 아니라면 코루틴 종료 => 코루틴들어오고 상태가 변했을 경우 밑에 공격모션을 취소하기 위한 예외처리
        if (monsterController.enumState != MonsterController.MonsterState.ATTACK)
        {
            Debug.Log($"2초후 상태{monsterController.enumState}");
            yield break;
        }
        nomalWoodenAni.SetBool("isAttackA", true);
    } //AttackDelay
}
