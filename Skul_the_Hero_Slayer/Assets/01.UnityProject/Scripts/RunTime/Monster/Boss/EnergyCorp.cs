using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCorp : MonoBehaviour
{
    private GameObject bossObj;
    private BossMonster bossMonster;
    private CircleCollider2D corpCollider;
    private Animator corpAni;
    private Vector3 direction;
    private Vector3 targetPos;
    private float speed = 5f;
    private int minDamage;
    private int maxDamage;
    private bool isMove = false;
    private bool isHit = false;

    // Start is called before the first frame update
    void Awake()
    {
        bossObj = GFunc.GetRootObj("Boss");
        bossMonster = bossObj.GetComponentMust<BossMonster>();
        corpAni = gameObject.GetComponentMust<Animator>();
        corpCollider = gameObject.GetComponentMust<CircleCollider2D>();
        minDamage = bossMonster.minDamage;
        maxDamage = bossMonster.maxDamage;
    }

    //활성화될때 초기화
    private void OnEnable()
    {
        corpCollider.enabled = false;
        speed = 5f;
        isHit = false;
        isMove = false;
        //보스페이즈에 따른 공격타입 세팅
        if (bossMonster.isChangePhase == false)
        {
            corpAni.SetBool("isAttackP1", true);
        }
        else
        {
            corpAni.SetBool("isAttackP2", true);
        }
        corpAni.SetBool("isHitP1", false);
        corpAni.SetBool("isHitP2", false);
    } //OnEnable

    // Update is called once per frame
    void Update()
    {
        Attack();
    } //Update

    //타겟의 위치로 조준방향 정하는 함수
    private void GetTargetPos()
    {
        direction = bossMonster.hit.gameObject.transform.position;
        targetPos = (direction - transform.position).normalized;
    } //GetTargetPos

    //Corp의 공격준비가 끝나면 타겟방향으로 공격하고 땅에닿거나 Hit되면 그자리에서 터지는 애니메이션 시작
    private void Attack()
    {
        //Corp이 땅에 닿으면 isHit = true
        if (gameObject.transform.position.y <= -3f)
        {
            isHit = true;
        }

        if (isHit == true)
        {
            //1, 2페이즈에 따른 공격타입 교체
            if (bossMonster.isChangePhase == false)
            {
                corpAni.SetBool("isHitP1", true);
                speed = 2f;
            }
            else
            {
                corpAni.SetBool("isHitP2", true);
                speed = 0f;
            }
        }
        if (isMove == true)
        {
            transform.Translate(targetPos * speed * Time.deltaTime);
        }
    } //Attack

    //Corp의 준비모션이 끝나면 타겟방향으로 나아가는 애니메이션 이벤트함수
    public void MoveReady()
    {
        if (bossMonster.isChangePhase == false)
        {
            corpAni.SetBool("isAttackP1", false);
        }
        else
        {
            corpAni.SetBool("isAttackP2", false);
        }
        isMove = true;
        corpCollider.enabled = true;
        GetTargetPos();
    } //MoveReady

    //공격이 종료되면 비활성화 애니메이션 이벤트함수
    public void EndAttack()
    {
        gameObject.SetActive(false);
    } //EndAttack

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == GData.PLAYER_LAYER_MASK)
        {
            PlayerController target = collider.gameObject?.GetComponentMust<PlayerController>();
            target.playerHp -= Random.RandomRange(minDamage, maxDamage);
            Debug.Log($"보스 에너지볼 공격! 플레이어 hp = {target.playerHp}/{target.playerMaxHp}");
            isHit = true;
        }
    } //OnTriggerEnter2D
}