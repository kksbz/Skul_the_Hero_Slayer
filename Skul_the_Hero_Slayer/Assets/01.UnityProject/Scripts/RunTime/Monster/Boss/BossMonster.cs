using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private GameObject body;
    private Animator bodyAni;
    private GameObject Head;
    private Animator headAni;
    private GameObject leftArm;
    private Animator leftArmAni;
    private GameObject rightArm;
    private Animator rightArmAni;
    private Collider2D hit;
    private float distance;
    private float meleeRange = 3.5f;
    protected int minDamage = 9;
    protected int maxDamage = 15;

    private bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.FindChildObj("Body");
        Head = gameObject.FindChildObj("Head");
        leftArm = gameObject.FindChildObj("LeftArm");
        rightArm = gameObject.FindChildObj("RightArm");

        bodyAni = body.GetComponentMust<Animator>();
        headAni = Head.GetComponentMust<Animator>();
        leftArmAni = leftArm.GetComponentMust<Animator>();
        rightArmAni = rightArm.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetCheck();
    } //Update

    //감지범위 안에 타겟과의 거리를 비교하여 다음 할 행동의 조건을 정하는 함수
    private void TargetCheck()
    {
        //감지범위는 OverlapBox로 구현
        hit = Physics2D.OverlapBox(transform.position, new Vector2(14f, 10f), 0, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
        if (hit != null)
        {
            //타겟이 감지될 경우 타겟과의 거리를 구함
            distance = Vector2.Distance(hit.transform.position, transform.position);
        }
        //왼쪽팔과 오른쪽팔의 진행중인 애니메이션이 끝났을 때 공격가능
        if (leftArmAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && rightArmAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            isAttack = false;
        }
    } //TargetCheck

    //조건에 따라 공격타입을 정하는 함수
    private void SelectAttackType()
    {
        //공격이 가능할 때 실행
        if (isAttack == false)
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

    ////AttackB 함수
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

    private void OnAttackC()
    {

    } //AttackC
}
