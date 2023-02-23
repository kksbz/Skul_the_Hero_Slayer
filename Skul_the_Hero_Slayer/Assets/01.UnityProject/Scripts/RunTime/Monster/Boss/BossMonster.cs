using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private GameObject body;
    private GameObject Head;
    private GameObject leftArm;
    private GameObject rightArm;
    public Animator bodyAni;
    public Animator headAni;
    public Animator leftArmAni;
    public Animator rightArmAni;
    private CorpPool corpPool;
    private float meleeRange = 3.5f;
    private bool isCorpAttack = false;
    private float corpAttackCoolDown = 0f;
    public Collider2D hit;
    public float distance;
    public int minDamage = 9;
    public int maxDamage = 15;
    public bool isChangePhase = false;
    public int hp;
    public int maxHp = 500;
    public bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        body = gameObject.FindChildObj("Body");
        Head = gameObject.FindChildObj("Head");
        leftArm = gameObject.FindChildObj("LeftArm");
        rightArm = gameObject.FindChildObj("RightArm");

        bodyAni = body.GetComponentMust<Animator>();
        headAni = Head.GetComponentMust<Animator>();
        leftArmAni = leftArm.GetComponentMust<Animator>();
        rightArmAni = rightArm.GetComponentMust<Animator>();

        corpPool = gameObject.FindChildObj("CorpPool").GetComponentMust<CorpPool>();
    } //Start

    // Update is called once per frame
    void Update()
    {
        TargetCheck();
        SelectAttackType();
    } //Update

    //감지범위 안에 타겟과의 거리를 비교하여 다음 할 행동의 조건을 정하는 함수
    private void TargetCheck()
    {
        corpAttackCoolDown += Time.deltaTime;
        if (corpAttackCoolDown >= 10f)
        {
            isCorpAttack = true;
            corpAttackCoolDown = 0;
        }
        //감지범위는 OverlapBox로 구현
        hit = Physics2D.OverlapBox(transform.position, new Vector2(30f, 10f), 0, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
        if (hit != null)
        {
            //타겟이 감지될 경우 타겟과의 거리를 구함
            distance = Vector2.Distance(hit.transform.position, transform.position);
        }
        //왼쪽팔과 오른쪽팔의 진행중인 애니메이션이 끝났을 때 공격가능
        if (leftArmAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && rightArmAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && isCorpAttack == false)
        {
            isAttack = false;
        }
        Debug.Log(isCorpAttack);
    } //TargetCheck

    void OnDrawGizmos()
    {
        if (transform.parent != null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(30f, 10f));
    } //OnDrawGizmos

    //조건에 따라 공격타입을 정하는 함수
    private void SelectAttackType()
    {
        if (isCorpAttack == true)
        {
            OnAttackC();
            corpPool.ShootBullet();
            isCorpAttack = false;
            return;
        }
        //공격이 가능할 때 실행
        if (isAttack == false && isCorpAttack == false)
        {
            //타겟이 근접사거리 안에 있으면 AttackA 실행, 멀면 AttackB 실행
            if (meleeRange >= distance)
            {
                OnAttackA();
            }
            else if (meleeRange < distance)
            {
                OnAttackB();
            }
        }
    } //SelectAttackType

    //AttackA 함수
    private void OnAttackA()
    {
        //타겟이 오른쪽에 있으면 오른팔로 왼쪽이면 왼쪽팔로 공격
        if (hit.transform.localPosition.x > 0)
        {
            rightArmAni.SetBool("isAttackA", true);
            leftArmAni.SetBool("isWaitAttack", true);
        }
        else if (hit.transform.localPosition.x < 0)
        {
            leftArmAni.SetBool("isAttackA", true);
            rightArmAni.SetBool("isWaitAttack", true);
        }
        isAttack = true;
    } //AttackA

    //AttackB 함수
    private void OnAttackB()
    {
        //타겟이 오른쪽에 있으면 오른팔로 왼쪽이면 왼쪽팔로 공격
        if (hit.transform.localPosition.x > 0)
        {
            rightArmAni.SetBool("isAttackB", true);
            leftArmAni.SetBool("isWaitAttack", true);
        }
        else if (hit.transform.localPosition.x < 0)
        {
            leftArmAni.SetBool("isAttackB", true);
            rightArmAni.SetBool("isWaitAttack", true);
        }
        isAttack = true;
    } //AttackB

    //AttackC 함수
    private void OnAttackC()
    {
        leftArmAni.SetBool("isAttackC", true);
        rightArmAni.SetBool("isAttackC", true);
        isAttack = true;
    } //AttackC
}
