using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : MonoBehaviour
{
    private GreenWooden parentWooden;
    private int minDamage;
    private int maxDamage;

    //부모를 정하는 함수
    public void Init(GreenWooden parent)
    {
        parentWooden = parent;
        gameObject.transform.parent = parentWooden.transform;
        minDamage = parentWooden.gameObject.GetComponentMust<MonsterController>().monster.minDamage;
        maxDamage = parentWooden.gameObject.GetComponentMust<MonsterController>().monster.maxDamage;
    } //Init

    //공격판정 시작하는 함수
    public void OnAttack()
    {
        //BoxCollider2D를 꺼둔상태에서 애니메이션 트리거가 발동하면 BoxCollider2D를 켜서 트리거엔터발동판정
        gameObject.GetComponentMust<BoxCollider2D>().enabled = true;
    } //OnAttack
    
    //공격이 끝나면 부모에게 공격이 끝났다고 알려주는 함수
    public void ExitAttack()
    {
        parentWooden.NotifyFinish();
        Destroy(gameObject);
    } //ExitAttack

    //가시 트리거발동 안됨
    //가시의 Collider에 타겟이 충돌할 때
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"트리거발동? {collider.tag}");
        if(collider.tag == GData.PLAYER_LAYER_MASK)
        {
            PlayerController target = collider.gameObject?.GetComponentMust<PlayerController>();
            target.hp -= Random.RandomRange(minDamage, maxDamage);
            Debug.Log($"가시 공격 플레이어 hp = {target.hp}");
        }
    } //OnTriggerEnter2D
}