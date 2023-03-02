using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    private BossMonster bossObj;
    private Animator bossHeadAni;
    private GameObject groggyEffect;
    private GameObject slamEffect;
    private AudioSource headAudio;
    private SpriteRenderer headSprite;
    private SpriteRenderer jawSprite;
    public AudioClip attackASound;
    public AudioClip attackBSound;
    public AudioClip attackCSound;
    public AudioClip fistSlamSound;
    public AudioClip groggySound;
    public AudioClip deadSound;
    public AudioClip startSound;
    public int maxHp { get => bossObj.maxHp; }
    public int hp { get => bossObj.hp; set => bossObj.hp = value; }
    // Start is called before the first frame update
    void Start()
    {
        bossObj = gameObject.transform.parent.GetComponent<BossMonster>();
        bossHeadAni = gameObject.GetComponentMust<Animator>();
        headAudio = gameObject.GetComponentMust<AudioSource>();
        headSprite = gameObject.GetComponentMust<SpriteRenderer>();
        jawSprite = gameObject.FindChildObj("Jaw").gameObject.GetComponentMust<SpriteRenderer>();
        groggyEffect = GFunc.GetRootObj("BossEffect").FindChildObj("GroggyEffect");
        slamEffect = GFunc.GetRootObj("BossEffect").FindChildObj("FistSlamEffect");
    }

    //1페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    private void P1ExitAttack()
    {
        bossHeadAni.SetBool("isAttackA", false);
        bossHeadAni.SetBool("isRightAttack", false);
        bossHeadAni.SetBool("isLeftAttack", false);
        bossHeadAni.SetBool("isAttackC", false);
        bossHeadAni.SetBool("isFistSlam", false);
        bossHeadAni.SetBool("isGroggy", false);
    } //P1ExitAttack

    //2페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    private void P2ExitAttack()
    {
        bossHeadAni.SetBool("isP2RightAttackA", false);
        bossHeadAni.SetBool("isP2LeftAttackA", false);
        bossHeadAni.SetBool("isP2RightAttackB", false);
        bossHeadAni.SetBool("isP2LeftAttackB", false);
        bossHeadAni.SetBool("isP2AttackC", false);
        bossHeadAni.SetBool("isP2FistSlam", false);
        bossHeadAni.SetBool("isP2Groggy", false);
        bossHeadAni.SetBool("isP2Idle", true);
    } //P2ExitAttack

    //Start페이즈 나갈때 애니메이션 체크 설정
    private void StartPhase()
    {
        gameObject.tag = GData.ENEMY_LAYER_MASK;
        gameObject.layer = 7;
        bossHeadAni.SetBool("isStart", false);
        bossObj.isChangeBossState = false;
    } //StartPhase

    //Start페이즈 진행중 보스가 맵아래에서 올라온다음 SortingLayer를 변경해 맵보다 먼저 보이게 하는 함수
    private void ChangeSortingLayer()
    {
        headSprite.sortingLayerName = GData.ENEMY_LAYER_MASK;
        jawSprite.sortingLayerName = GData.ENEMY_LAYER_MASK;
    } //ChangeSortingOrder
    private void ChangePhase()
    {
        gameObject.tag = GData.ENEMY_LAYER_MASK;
        gameObject.layer = 7;
        bossHeadAni.SetBool("isChangePhase", false);
        bossHeadAni.SetBool("isP2Idle", true);
        bossObj.isChangeBossState = false;
    } //ExitChangePhase

    private void Dead()
    {
        bossHeadAni.SetBool("isP2Idle", false);
        bossHeadAni.SetBool("isP2RightAttackA", false);
        bossHeadAni.SetBool("isP2LeftAttackA", false);
        bossHeadAni.SetBool("isP2RightAttackB", false);
        bossHeadAni.SetBool("isP2LeftAttackB", false);
        bossHeadAni.SetBool("isP2AttackC", false);
        bossHeadAni.SetBool("isP2FistSlam", false);
        bossHeadAni.SetBool("isP2Groggy", false);
    } //Dead

    //Groggy이펙트 활성화하는 함수
    private void OnOffGroggyEffect()
    {
        if (!groggyEffect.activeInHierarchy)
        {
            groggyEffect.SetActive(true);
        }
        else
        {
            groggyEffect.SetActive(false);
        }
    } //OnOffGroggyEffect

    //FistSlam이펙트 활성화하는 함수
    private void OnOffFistSlamEffect()
    {
        slamEffect.SetActive(true);
    } //OnOffFistSlamEffect

    private void AttackASound()
    {
        headAudio.clip = attackASound;
        headAudio.Play();
    }
    private void AttackBSound()
    {
        headAudio.clip = attackBSound;
        headAudio.Play();
    }
    private void AttackCSound()
    {
        headAudio.clip = attackCSound;
        headAudio.Play();
    }
    private void FistSlamSound()
    {
        headAudio.clip = fistSlamSound;
        headAudio.Play();
    }
    private void GroggySound()
    {
        headAudio.clip = groggySound;
        headAudio.Play();
    }
    private void DeadSound()
    {
        headAudio.clip = deadSound;
        headAudio.Play();
    }
    private void StartSound()
    {
        headAudio.clip = startSound;
        headAudio.Play();
    }
}
