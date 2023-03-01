using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour
{
    private Animator bossBodyAni;
    private AudioSource bodyAudio;
    private GameObject leftGround;
    private GameObject rightGround;
    public AudioClip attackSound;
    public AudioClip groggySound;
    public AudioClip deadSound;
    public AudioClip endAttackSound;
    public AudioClip changeSound;
    // Start is called before the first frame update
    void Start()
    {
        bossBodyAni = gameObject.GetComponentMust<Animator>();
        bodyAudio = gameObject.GetComponentMust<AudioSource>();
        leftGround = gameObject.FindChildObj("LeftGround");
        rightGround = gameObject.FindChildObj("RightGround");
    } //Start

    //1페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    private void P1ExitAttack()
    {
        bossBodyAni.SetBool("isAttackA", false);
        bossBodyAni.SetBool("isRightAttack", false);
        bossBodyAni.SetBool("isLeftAttack", false);
        bossBodyAni.SetBool("isAttackC", false);
        bossBodyAni.SetBool("isFistSlam", false);
        bossBodyAni.SetBool("isGroggy", false);
    } //P1ExitAttack

    //2페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    private void P2ExitAttack()
    {
        bossBodyAni.SetBool("isP2RightAttackA", false);
        bossBodyAni.SetBool("isP2LeftAttackA", false);
        bossBodyAni.SetBool("isP2RightAttackB", false);
        bossBodyAni.SetBool("isP2LeftAttackB", false);
        bossBodyAni.SetBool("isP2AttackC", false);
        bossBodyAni.SetBool("isP2FistSlam", false);
        bossBodyAni.SetBool("isP2Groggy", false);
        bossBodyAni.SetBool("isP2Idle", true);
    } //P2ExitAttack

    //Start페이즈 나갈때 애니메이션 체크 설정
    private void StartPhase()
    {
        bossBodyAni.SetBool("isStart", false);
    } //StartPhase

    //페이즈 변환 탈출시 실행 함수
    private void ChangePhase()
    {
        bossBodyAni.SetBool("isChangePhase", false);
        bossBodyAni.SetBool("isP2Idle", true);
    } //ExitChangePhase

    //보스 죽을 때 실행 함수
    private void Dead()
    {
        bossBodyAni.SetBool("isP2Idle", false);
        bossBodyAni.SetBool("isP2RightAttackA", false);
        bossBodyAni.SetBool("isP2LeftAttackA", false);
        bossBodyAni.SetBool("isP2RightAttackB", false);
        bossBodyAni.SetBool("isP2LeftAttackB", false);
        bossBodyAni.SetBool("isP2AttackC", false);
        bossBodyAni.SetBool("isP2FistSlam", false);
        bossBodyAni.SetBool("isP2Groggy", false);
        leftGround.SetActive(false);
        rightGround.SetActive(false);
    } //Dead

    private void AttackSound()
    {
        bodyAudio.clip = attackSound;
        bodyAudio.Play();
    }

    private void EndAttackSound()
    {
        bodyAudio.clip = endAttackSound;
        bodyAudio.Play();
    }

    private void GroggySound()
    {
        bodyAudio.clip = groggySound;
        bodyAudio.Play();

    }
    private void DeadSound()
    {
        bodyAudio.clip = deadSound;
        bodyAudio.Play();
    }
    private void ChangeSound()
    {
        bodyAudio.clip = changeSound;
        bodyAudio.Play();
    }
    //보스몸 밟을 수 있는 땅 2개 활성하는 함수
    private void OnBodyGround()
    {
        leftGround.SetActive(true);
        rightGround.SetActive(true);
    }
    //보스몸 밟을 수 있는 땅 2개 비활성하는 함수
    private void OffBodyGround()
    {
        leftGround.SetActive(false);
        rightGround.SetActive(false);
    }
}
