using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWoodenBullet : MonoBehaviour
{
    private Vector3 direction;
    private BigWoodenAttackB parentObj;
    private MonsterController grandParents;
    private Animator bulletAni;
    private bool isHitTarget = false; //타겟에 맞았는지 확인하는 변수
    private float speed = 4f;
    private int minDamage;
    private int maxDamage;
    // Start is called before the first frame update
    void Start()
    {
        parentObj = gameObject.transform.parent.GetComponent<BigWoodenAttackB>();
        grandParents = parentObj.transform.parent.GetComponent<MonsterController>();
        bulletAni = gameObject.GetComponentMust<Animator>();
        minDamage = grandParents.monster.minDamage;
        maxDamage = grandParents.monster.maxDamage;
        SetupDirection();
    } //Start

    // Update is called once per frame
    void Update()
    {
        //타겟에 맞았으면 속도를 1로 변경해서 자연스럽게 사라지게하는 처리
        if (isHitTarget == true)
        {
            speed = 1f;
        }
        gameObject.transform.Translate(direction * speed * Time.deltaTime);
        IsHitbullet();
    } //Update

    //활성화될때 초기화
    private void OnEnable()
    {
        speed = 4f;
        isHitTarget = false;
    } //OnEnable

    //Bullet의 이동방향을 부모로부터 가져오는 함수
    private void SetupDirection()
    {
        for (int i = 0; i < parentObj.objPool.Count; i++)
        {
            if (parentObj.objPool[i].name == gameObject.name)
            {
                direction = parentObj.vectorList[i];
            }
        }
    } //SetupDirection

    //일정거리 날아가면 bullet의 상태를 isHit 상태로 전환하는 함수
    private void IsHitbullet()
    {
        float distance = Vector3.Distance(grandParents.transform.position, transform.position);
        if (distance >= 12f)
        {
            bulletAni.SetBool("isHit", true);
        }
    } //bulletFalse

    //피격되거나 일정거리 이상 날아간 isHit상태의 Bullet 비활성화하는 함수
    public void IsHitBulletFalse()
    {
        bulletAni.SetBool("isHit", false);
        gameObject.SetActive(false);
    } //IshitBulletFalse
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == GData.PLAYER_LAYER_MASK)
        {
            PlayerController target = collider.gameObject?.GetComponentMust<PlayerController>();
            target.playerHp -= Random.RandomRange(minDamage, maxDamage);
            bulletAni.SetBool("isHit", true);
            isHitTarget = true;
            Debug.Log($"빅우든 원거리공격! 플레이어 hp = {target.playerHp}/{target.playerMaxHp}");
        }
    } //OnTriggerEnter2D
}
