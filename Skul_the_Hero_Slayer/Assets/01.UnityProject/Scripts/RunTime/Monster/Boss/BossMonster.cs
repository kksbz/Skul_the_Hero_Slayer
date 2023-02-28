using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private GameObject body;
    private GameObject Head;
    private GameObject leftArm;
    private GameObject rightArm;
    private GameObject groggyEffect;
    public Animator bodyAni;
    public Animator headAni;
    public Animator leftArmAni;
    public Animator rightArmAni;
    private CorpPool corpPool;
    private float meleeRange = 3.5f;
    private bool isGroggy = false;
    private bool isAttack = false;
    private bool isCorpAttack = false;
    private float corpAttackCoolDown = 0f;
    private bool isFistSlam = false;
    private float fistSlamCoolDown = 0f;
    private bool isDead = false;
    public Collider2D hit;
    public float distance;
    public int minDamage = 9;
    public int maxDamage = 15;
    public bool isChangePhase = false;
    private int phaseCheck = 0;
    public int hp;
    public int maxHp;

    void Awake()
    {
        maxHp = 30;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            ChangePhase();
            TargetCheck();
            SelectAttackType();
        }
    } //Update

    //감지범위 안에 타겟과의 거리를 비교하여 다음 할 행동의 조건을 정하는 함수
    private void TargetCheck()
    {
        //hp 0보다 작거나 같으면 2페이즈로 전환
        if (hp <= 0)
        {
            if (isChangePhase == false)
            {
                isChangePhase = true;
            }
            else if (isChangePhase == true)
            {
                isDead = true;
                Dead();
            }
        }
        corpAttackCoolDown += Time.deltaTime;
        //corp공격 쿨타임
        if (corpAttackCoolDown >= 20f)
        {
            isCorpAttack = true;
        }

        fistSlamCoolDown += Time.deltaTime;
        // Debug.Log($"크롭쿨{corpAttackCoolDown}");
        // Debug.Log($"슬렘쿨{corpAttackCoolDown}");
        if (fistSlamCoolDown >= 10f)
        {
            isFistSlam = true;
        }

        //감지범위는 OverlapBox로 구현
        hit = Physics2D.OverlapBox(transform.position, new Vector2(30f, 10f), 0, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
        if (hit != null)
        {
            //타겟이 감지될 경우 타겟과의 거리를 구함
            distance = Vector2.Distance(hit.transform.position, transform.position);
        }
        //Boss의 모든 파츠의 진행중인 애니메이션이 끝났을 때 공격가능
        if (leftArmAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && rightArmAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && headAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && bodyAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
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

    //Dead 상태 실행 함수
    private void Dead()
    {
        bodyAni.SetBool("isDead", true);
        headAni.SetBool("isDead", true);
        rightArmAni.SetBool("isDead", true);
        leftArmAni.SetBool("isDead", true);
    } //Dead

    //조건에 따라 공격타입을 정하는 함수
    private void SelectAttackType()
    {
        //공격이 가능할 때 실행
        if (isAttack == false)
        {
            //corp 조건이 달성되면 AttackC corp공격 실행
            if (isCorpAttack == true && isGroggy == false)
            {
                OnAttackC();
                return;
            }
            //그로기 조건이 달성되면 그로기상태 실행
            if (isGroggy == true)
            {
                OnGroggy();
                return;
            }
            //FistSlam 조건이 달성되면 공격 실행
            if (isFistSlam == true)
            {
                FistSlam();
                return;
            }
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
    private void ChangePhase()
    {
        //hp가 0보다 작거나 같고 현재 상태가 1페이즈일 때 2페이즈로 전환
        if (isChangePhase == true && phaseCheck == 0)
        {
            Head.tag = GData.PLAYER_LAYER_MASK;
            bodyAni.SetBool("isChangePhase", true);
            headAni.SetBool("isChangePhase", true);
            rightArmAni.SetBool("isChangePhase", true);
            leftArmAni.SetBool("isChangePhase", true);
            hp = maxHp;
            phaseCheck += 1;
        }
    } //ChangePhase

    //crop공격이후에 그로기하는 함수
    private void OnGroggy()
    {
        isGroggy = false;
        isAttack = true;
        if (isChangePhase == false)
        {
            headAni.SetBool("isGroggy", true);
            bodyAni.SetBool("isGroggy", true);
            rightArmAni.SetBool("isGroggy", true);
            leftArmAni.SetBool("isGroggy", true);
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            OffP2Idle();
            headAni.SetBool("isP2Groggy", true);
            bodyAni.SetBool("isP2Groggy", true);
            rightArmAni.SetBool("isP2Groggy", true);
            leftArmAni.SetBool("isP2Groggy", true);
        }
    } //OnGroggy

    //2페이즈 상태에서 공격실행 전 Idle모션 초기화하는 함수
    private void OffP2Idle()
    {
        headAni.SetBool("isP2Idle", false);
        bodyAni.SetBool("isP2Idle", false);
        rightArmAni.SetBool("isP2Idle", false);
        leftArmAni.SetBool("isP2Idle", false);
    } //OffP2Idle

    //AttackA 함수
    private void OnAttackA()
    {
        isAttack = true;
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
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            OffP2Idle();
            // 타겟이 오른쪽에 있으면 오른팔로 왼쪽이면 왼쪽팔로 공격
            if (hit.transform.localPosition.x > 0)
            {
                headAni.SetBool("isP2RightAttackA", true);
                bodyAni.SetBool("isP2RightAttackA", true);
                rightArmAni.SetBool("isP2AttackA", true);
                leftArmAni.SetBool("isP2WaitAttackA", true);
            }
            else if (hit.transform.localPosition.x < 0)
            {
                headAni.SetBool("isP2LeftAttackA", true);
                bodyAni.SetBool("isP2LeftAttackA", true);
                leftArmAni.SetBool("isP2AttackA", true);
                rightArmAni.SetBool("isP2WaitAttackA", true);
            }
        }
    } //AttackA

    //AttackB 함수
    private void OnAttackB()
    {
        isAttack = true;
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
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            OffP2Idle();
            //타겟이 오른쪽에 있으면 오른팔로 왼쪽이면 왼쪽팔로 공격
            if (hit.transform.localPosition.x > 0)
            {
                headAni.SetBool("isP2RightAttackB", true);
                bodyAni.SetBool("isP2RightAttackB", true);
                rightArmAni.SetBool("isP2AttackB", true);
                leftArmAni.SetBool("isP2WaitAttackB", true);
            }
            else if (hit.transform.localPosition.x < 0)
            {
                headAni.SetBool("isP2LeftAttackB", true);
                bodyAni.SetBool("isP2LeftAttackB", true);
                leftArmAni.SetBool("isP2AttackB", true);
                rightArmAni.SetBool("isP2WaitAttackB", true);
            }
        }
    } //AttackB

    //AttackC 함수
    private void OnAttackC()
    {
        isCorpAttack = false;
        isGroggy = true;
        isAttack = true;
        StartCoroutine(ShootCorp());
        Debug.Log($"crop 들옴?{corpAttackCoolDown}");
        corpAttackCoolDown = 0f;
        Debug.Log($"crop 쿨다운{corpAttackCoolDown}");
        if (isChangePhase == false)
        {
            //1페이즈인 경우 실행
            headAni.SetBool("isAttackC", true);
            bodyAni.SetBool("isAttackC", true);
            leftArmAni.SetBool("isAttackC", true);
            rightArmAni.SetBool("isAttackC", true);
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            OffP2Idle();
            headAni.SetBool("isP2AttackC", true);
            bodyAni.SetBool("isP2AttackC", true);
            leftArmAni.SetBool("isP2AttackC", true);
            rightArmAni.SetBool("isP2AttackC", true);
        }
    } //AttackC

    //AttackC 진행중 Corp이 생성될 시기 정하는 코루틴함수 
    private IEnumerator ShootCorp()
    {
        yield return new WaitForSeconds(1f);
        corpPool.ShootBullet();
    } //ShootCorp

    //공격타입 : FistSlam 실행하는 함수
    private void FistSlam()
    {
        isFistSlam = false;
        isAttack = true;
        Debug.Log($"Slam들옴? {fistSlamCoolDown}");
        fistSlamCoolDown = 0f;
        Debug.Log($"Slam 쿨다운 {fistSlamCoolDown}");

        if (isChangePhase == false)
        {
            //1페이즈인 경우 실행
            headAni.SetBool("isFistSlam", true);
            bodyAni.SetBool("isFistSlam", true);
            leftArmAni.SetBool("isFistSlam", true);
            rightArmAni.SetBool("isFistSlam", true);
        }
        else if (isChangePhase == true)
        {
            //2페이즈인 경우 실행
            OffP2Idle();
            headAni.SetBool("isP2FistSlam", true);
            bodyAni.SetBool("isP2FistSlam", true);
            leftArmAni.SetBool("isP2FistSlam", true);
            rightArmAni.SetBool("isP2FistSlam", true);
        }
    } //FistSlam


}
