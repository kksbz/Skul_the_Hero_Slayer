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
    public int maxHp;
    public bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.FindChildObj("Body");
        Head = gameObject.FindChildObj("Head");
        leftArm = gameObject.FindChildObj("LeftArm");
        rightArm = gameObject.FindChildObj("RightArm");
        isChangePhase = true;
        bodyAni = body.GetComponentMust<Animator>();
        headAni = Head.GetComponentMust<Animator>();
        leftArmAni = leftArm.GetComponentMust<Animator>();
        rightArmAni = rightArm.GetComponentMust<Animator>();

        corpPool = gameObject.FindChildObj("CorpPool").GetComponentMust<CorpPool>();
    } //Start

    // Update is called once per frame
    void Update()
    {
        // ChangePhase();
        TargetCheck();
        SelectAttackType();
    } //Update

    //감지범위 안에 타겟과의 거리를 비교하여 다음 할 행동의 조건을 정하는 함수
    private void TargetCheck()
    {
        corpAttackCoolDown += Time.deltaTime;
        if (corpAttackCoolDown >= 15f)
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

    //2페이즈로 전환하는 함수
    public virtual void ChangePhase(bool ChangePhase)
    {
        Debug.Log($"부모오브젝트의 불값 체크 보스{isChangePhase}/ 머리{ChangePhase}");
        isChangePhase = ChangePhase;
        Debug.Log($"부모오브젝트의 불값 체크 보스{isChangePhase}/ 머리{ChangePhase}");
        //hp가 0보다 작거나 같고 현재 상태가 1페이즈일 때 2페이즈로 전환
        if (isChangePhase == true)
        {
            Debug.Log("부모 페이즈변환 들어옴?");
            bodyAni.SetBool("isChangePhase", true);
            headAni.SetBool("isChangePhase", true);
            rightArmAni.SetBool("isChangePhase", true);
            leftArmAni.SetBool("isChangePhase", true);
            Debug.Log("애니 실행됨?");
            hp = maxHp;
        }
    } //ChangePhase

    //AttackA 함수
    private void OnAttackA()
    {
        if (isChangePhase == false)
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
            headAni.SetBool("isAttackA", true);
            bodyAni.SetBool("isAttackA", true);
            isAttack = true;
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            headAni.SetBool("isP2Idle", false);
            bodyAni.SetBool("isP2Idle", false);
            rightArmAni.SetBool("isP2Idle", false);
            leftArmAni.SetBool("isP2Idle", false);

            headAni.SetBool("isP2AttackA", true);
            bodyAni.SetBool("isP2AttackA", true);
            rightArmAni.SetBool("isP2AttackA", true);
            leftArmAni.SetBool("isP2AttackA", true);
            //타겟이 오른쪽에 있으면 오른팔로 왼쪽이면 왼쪽팔로 공격
            // if (hit.transform.localPosition.x > 0)
            // {
            //     rightArmAni.SetBool("isP2AttackA", true);
            //     leftArmAni.SetBool("isWaitAttack", true);
            // }
            // else if (hit.transform.localPosition.x < 0)
            // {
            //     leftArmAni.SetBool("isP2AttackA", true);
            //     rightArmAni.SetBool("isWaitAttack", true);
            // }
            isAttack = true;
        }
    } //AttackA

    //AttackB 함수
    private void OnAttackB()
    {
        //타겟이 오른쪽에 있으면 오른팔로 왼쪽이면 왼쪽팔로 공격
        if (isChangePhase == false)
        {
            //1페이즈인 경우 실행
            if (hit.transform.localPosition.x > 0)
            {
                headAni.SetBool("isRightAttack", true);
                bodyAni.SetBool("isRightAttack", true);
                rightArmAni.SetBool("isAttackB", true);
                leftArmAni.SetBool("isWaitAttack", true);
            }
            else if (hit.transform.localPosition.x < 0)
            {
                headAni.SetBool("isLeftAttack", true);
                bodyAni.SetBool("isLeftAttack", true);
                leftArmAni.SetBool("isAttackB", true);
                rightArmAni.SetBool("isWaitAttack", true);
            }
            isAttack = true;
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            headAni.SetBool("isP2Idle", false);
            bodyAni.SetBool("isP2Idle", false);
            rightArmAni.SetBool("isP2Idle", false);
            leftArmAni.SetBool("isP2Idle", false);
            //타겟이 오른쪽에 있으면 오른팔로 왼쪽이면 왼쪽팔로 공격
            if (hit.transform.localPosition.x > 0)
            {
                headAni.SetBool("isP2RightAttack", true);
                bodyAni.SetBool("isP2RightAttack", true);
                rightArmAni.SetBool("isP2AttackB", true);
                leftArmAni.SetBool("isP2WaitAttack", true);
            }
            else if (hit.transform.localPosition.x < 0)
            {
                headAni.SetBool("isP2LeftAttack", true);
                bodyAni.SetBool("isP2LeftAttack", true);
                leftArmAni.SetBool("isP2AttackB", true);
                rightArmAni.SetBool("isP2WaitAttack", true);
            }
            isAttack = true;
        }
    } //AttackB

    //AttackC 함수
    private void OnAttackC()
    {
        if (isChangePhase == false)
        {
            //1페이즈인 경우 실행
            headAni.SetBool("isAttackC", true);
            bodyAni.SetBool("isAttackC", true);
            leftArmAni.SetBool("isAttackC", true);
            rightArmAni.SetBool("isAttackC", true);
            isAttack = true;
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            headAni.SetBool("isP2Idle", false);
            bodyAni.SetBool("isP2Idle", false);
            rightArmAni.SetBool("isP2Idle", false);
            leftArmAni.SetBool("isP2Idle", false);

            headAni.SetBool("isP2AttackC", true);
            bodyAni.SetBool("isP2AttackC", true);
            leftArmAni.SetBool("isP2AttackC", true);
            rightArmAni.SetBool("isP2AttackC", true);
            isAttack = true;
        }
    } //AttackC
}
