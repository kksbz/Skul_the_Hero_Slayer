using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    private Animator bigWoodenAni;
    private RaycastHit2D hit; //AttackA 공격 처리 변수
    private GameObject meleeEffectObj; //AttackA 공격이펙트 처리 변수

    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("BigWooden") as MonsterData;
        InitMonsterData(monsterData);
        //몬스터컨트롤러에서 몬스터타입 변수로 접근가능하게 하기 위한 처리
        //몬스터컨트롤러의 몬스터타입 변수에 자신을 몬스터로 캐스팅하여 저장
        monsterController.monster = (Monster)(this as Monster);
        bigWoodenAni = gameObject.GetComponent<Animator>();
        meleeEffectObj = gameObject.FindChildObj("MeleeEffect");
        meleeEffectObj.transform.position = gameObject.transform.position;
        meleeEffectObj.SetActive(false);
    }

    //공격A하는 함수(공격을 애니메이션 이벤트로 처리함)
    public override void AttackA()
    {
        monsterController.monster.monsterAudio.clip = monsterController.monster.attackASound;
        monsterController.monster.monsterAudio.Play();
        StartCoroutine(MeleeEffectOnOff());
        Vector2 attackArea = new Vector2(8f, 1.5f);
        //Boxcast로 피격처리
        hit = Physics2D.BoxCast(transform.position, attackArea, 0f, Vector3.down, 1f, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
        if (hit.collider != null)
        {
            PlayerController target = hit.collider.gameObject.GetComponentMust<PlayerController>();
            if (target.isHit == false)
            {
                target.playerHp -= Random.Range(monsterData.MinDamage, monsterData.MaxDamage + 1);
                // Debug.Log($"빅우든 근접공격! 플레이어 hp = {target.playerHp}/{target.playerMaxHp}");
                int direction = target.transform.position.x - transform.position.x > 0 ? 1 : -1;
                //타겟을 direction방향으로 밀어냄
                target.player.playerRb.AddForce(new Vector2(direction, 2f), ForceMode2D.Impulse);
            }
        }
    } //AttackA

    //AttackA 공격이펙트 처리하는 코루틴함수
    private IEnumerator MeleeEffectOnOff()
    {
        meleeEffectObj.SetActive(true);
        float meleeEffectLength = meleeEffectObj.GetComponentMust<Animator>().GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(meleeEffectLength);
        //공격이펙트 애니메이션이 끝나면 비활성화
        meleeEffectObj.SetActive(false);
    } //MeleeEffectOnOff

    //공격B하는 함수(공격을 애니메이션 이벤트로 처리함)
    public override void AttackB()
    {
        GameObject childObj = gameObject.FindChildObj("BigWoodenAttackB");
        BigWoodenAttackB child = childObj.GetComponentMust<BigWoodenAttackB>();
        monsterController.monster.monsterAudio.clip = monsterController.monster.attackBSound;
        monsterController.monster.monsterAudio.Play();
        child.ShootBullet();
    } //AttackB

    // //AttackA 범위 기즈모
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireCube(transform.position + (Vector3.down * 1f), new Vector2(8f, 1.5f));
    // } //OnDrawGizmos


    //공격애니메이션이 종료되면 코루틴 실행
    public void ExitAttack()
    {
        //현재 진행중인 애니메이션의 이름이 AttackA면 AttackADelay 코루틴 실행 아니면 AttackBDelay실행
        if (bigWoodenAni.GetCurrentAnimatorStateInfo(0).IsName("AttackA"))
        {
            StartCoroutine(AttackADelay());
        }
        else if (bigWoodenAni.GetCurrentAnimatorStateInfo(0).IsName("AttackB"))
        {
            StartCoroutine(AttackBDelay());
        }
    } //ExitAttack

    //타겟과의 거리로 공격타입을 변경하는 함수
    private void ChangeAttackType()
    {
        Vector3 targetPos = monsterController.monster.tagetSearchRay.hit.transform.position;
        float distance = Vector2.Distance(targetPos, monsterController.monster.transform.position);
        //타겟과 자신의 거리가 근접공격거리 보다 작거나같으면 AttackA, 크면 AttackB 실행
        if (monsterController.enumState != MonsterController.MonsterState.ATTACK)
        {
            return;
        }
        if (distance <= monsterController.monster.meleeAttackRange)
        {
            bigWoodenAni.SetBool("isAttackA", true);
        }
        else if (distance > monsterController.monster.meleeAttackRange)
        {
            bigWoodenAni.SetBool("isAttackB", true);
        }
    } //ChageAttackType

    //AttackA 공격딜레이 정하는 코루틴 함수
    private IEnumerator AttackADelay()
    {
        //공격딜레이 중에는 idel모션 처리
        bigWoodenAni.SetBool("isAttackA", false);
        bigWoodenAni.SetBool("isIdle", true);
        yield return new WaitForSeconds(2.5f);
        bigWoodenAni.SetBool("isIdle", false);
        //2.5초후 현재 상태가 공격이 아니라면 코루틴 종료
        //=> 코루틴들어오고 상태가 변했을 경우 밑에 공격모션을 취소하기 위한 예외처리
        if (monsterController.enumState != MonsterController.MonsterState.ATTACK)
        {
            yield break;
        }
        ChangeAttackType();
    } //AttackDelay

    //AttackB 공격딜레이 정하는 코루틴 함수
    private IEnumerator AttackBDelay()
    {
        bigWoodenAni.SetBool("isAttackB", false);
        bigWoodenAni.SetBool("isIdle", true);
        yield return new WaitForSeconds(4f);
        bigWoodenAni.SetBool("isIdle", false);
        //4초후 현재 상태가 공격이 아니라면 코루틴 종료
        //=> 코루틴들어오고 상태가 변했을 경우 밑에 공격모션을 취소하기 위한 예외처리
        if (monsterController.enumState != MonsterController.MonsterState.ATTACK)
        {
            yield break;
        }
        ChangeAttackType();
    } //AttackBDelay
}
