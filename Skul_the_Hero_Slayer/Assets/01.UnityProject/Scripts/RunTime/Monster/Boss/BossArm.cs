using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm : MonoBehaviour
{
    private BossMonster bossObj;
    private BoxCollider2D armCollider;
    private Animator armAni;
    private AudioSource armAudio;
    private SpriteRenderer armSprite;
    public AudioClip attackASound;
    public AudioClip attackBSound;
    public AudioClip attackCSound;
    public AudioClip groggySound;
    public AudioClip deadSound;
    public AudioClip endAttackSound;
    // Start is called before the first frame update
    void Start()
    {
        bossObj = gameObject.transform.parent.GetComponent<BossMonster>();
        armCollider = gameObject.GetComponentMust<BoxCollider2D>();
        armAudio = gameObject.GetComponentMust<AudioSource>();
        armAni = gameObject.GetComponentMust<Animator>();
        armSprite = gameObject.GetComponentMust<SpriteRenderer>();
        armCollider.enabled = false;
    } //Start

    //Attack 애니메이션 진행 중 공격판정 시작위치 정하는 함수
    private void Attack()
    {
        armAudio.clip = attackASound;
        armAudio.Play();
        //BoxCollider2D를 꺼둔상태에서 애니메이션 트리거가 발동하면 BoxCollider2D를 켜서 트리거엔터발동판정
        armCollider.enabled = true;
    } //Attack

    //Attack 애니메이션 진행 중 공격판정 종료위치 정하는 함수
    private void AttackExit()
    {
        armCollider.enabled = false;
    } //AttackExit

    private void AttackBSound()
    {
        armAudio.clip = attackBSound;
        armAudio.Play();
    }
    private void AttackCSound()
    {
        armAudio.clip = attackCSound;
        armAudio.Play();
    }
    private void EndAttakcSound()
    {
        armAudio.clip = endAttackSound;
        armAudio.Play();
    }
    private void GroggySound()
    {
        armAudio.clip = groggySound;
        armAudio.Play();
    }
    private void DeadSound()
    {
        armAudio.clip = deadSound;
        armAudio.Play();
    }

    //1페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    private void P1ExitAttack()
    {
        armAni.SetBool("isAttackA", false);
        armAni.SetBool("isAttackB", false);
        armAni.SetBool("isAttackC", false);
        armAni.SetBool("isFistSlam", false);
        armAni.SetBool("isWaitAttack", false);
        armAni.SetBool("isGroggy", false);
        armCollider.enabled = false;
    } //ExitAttack

    //2페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    private void P2ExitAttack()
    {
        armAni.SetBool("isP2AttackA", false);
        armAni.SetBool("isP2AttackB", false);
        armAni.SetBool("isP2AttackC", false);
        armAni.SetBool("isP2WaitAttackA", false);
        armAni.SetBool("isP2WaitAttackB", false);
        armAni.SetBool("isP2FistSlam", false);
        armAni.SetBool("isP2Groggy", false);
        armAni.SetBool("isP2Idle", true);
        armCollider.enabled = false;
    } //P2ExitAttack

    //Start페이즈 진행중 보스가 맵아래에서 올라온다음 SortingLayer를 변경해 맵보다 먼저 보이게 하는 함수
    private void ChangeSortingLayer()
    {
        armSprite.sortingLayerName = "EnemyEffect";
    } //ChangeSortingOrder
    private void StartPhase()
    {
        armAni.SetBool("isStart", false);
    } //StartPhase

    //Start페이즈 나갈때 애니메이션 체크 설정
    private void ChangePhase()
    {
        armAni.SetBool("isChangePhase", false);
        armAni.SetBool("isP2Idle", true);
    } //ExitChangePhase

    private void Dead()
    {
        armAni.SetBool("isP2Idle", false);
        armAni.SetBool("isP2AttackA", false);
        armAni.SetBool("isP2AttackB", false);
        armAni.SetBool("isP2AttackC", false);
        armAni.SetBool("isP2WaitAttackA", false);
        armAni.SetBool("isP2WaitAttackB", false);
        armAni.SetBool("isP2FistSlam", false);
        armAni.SetBool("isP2Groggy", false);
    } //Dead

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == GData.PLAYER_LAYER_MASK)
        {
            PlayerController target = collider.gameObject?.GetComponentMust<PlayerController>();
            target.playerHp -= Random.RandomRange(bossObj.minDamage, bossObj.maxDamage);
            int direction = target.transform.position.x - transform.position.x > 0 ? 1 : -1;
            target.player.playerRb.AddForce(new Vector2(direction, 3f), ForceMode2D.Impulse);
            Debug.Log($"보스 공격A 플레이어 hp = {target.playerHp}/{target.playerMaxHp}");
            armCollider.enabled = false;
        }
    } //OnTriggerEnter2D
}
