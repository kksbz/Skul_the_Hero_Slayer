using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWoodenBullet : MonoBehaviour
{
    private Vector3 direction;
    private BigWoodenAttackB parentObj;
    private MonsterController grandParents;
    private Animator bulletAni;
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
        gameObject.transform.Translate(direction * speed * Time.deltaTime);
        bulletFalse();
    } //Update

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
    
    //일정거리 날아가면 bullet 비활성화하는 함수
    private void bulletFalse()
    {
        float distance = Vector3.Distance(grandParents.transform.position, transform.position);
        if (distance >= 15f)
        {
            bulletAni.SetBool("isHit", true);
            //여기부분 처리해야됨
            if (bulletAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                bulletAni.SetBool("isHit", false);
                gameObject.SetActive(false);
            }
        }
    } //bulletFalse
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == GData.PLAYER_LAYER_MASK)
        {
            PlayerController target = collider.gameObject?.GetComponentMust<PlayerController>();
            target.hp -= Random.RandomRange(minDamage, maxDamage);
            Debug.Log($"빅우든 원거리공격! 플레이어 hp = {target.hp}");
        }
    } //OnTriggerEnter2D
}
