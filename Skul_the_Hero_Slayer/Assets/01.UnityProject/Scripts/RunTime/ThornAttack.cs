using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : MonoBehaviour
{
    GreenWooden parentWooden;

    //부모를 정하는 함수
    public void Init(GreenWooden parent)
    {
        parentWooden = parent;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == GData.PLAYER_LAYER_MASK)
        {
            PlayerController taget = collider.gameObject?.GetComponentMust<PlayerController>();
            taget.hp -= 15;
            Debug.Log($"플레이어 hp = {taget.hp}");
        }
    } //OnTriggerEnter2D
}
