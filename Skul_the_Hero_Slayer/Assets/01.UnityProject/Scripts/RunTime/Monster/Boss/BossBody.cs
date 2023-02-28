using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour
{
    private Animator bossBodyAni;
    private AudioSource bodyAudio;
    public AudioClip attackSound;
    public AudioClip groggySound;
    public AudioClip deadSound;
    public AudioClip endAttackSound;
    // Start is called before the first frame update
    void Start()
    {
        bossBodyAni = gameObject.GetComponentMust<Animator>();
        bodyAudio = gameObject.GetComponentMust<AudioSource>();
    } //Start

    //1페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    public void P1ExitAttack()
    {
        bossBodyAni.SetBool("isAttackA", false);
        bossBodyAni.SetBool("isRightAttack", false);
        bossBodyAni.SetBool("isLeftAttack", false);
        bossBodyAni.SetBool("isAttackC", false);
        bossBodyAni.SetBool("isFistSlam", false);
        bossBodyAni.SetBool("isGroggy", false);
    } //P1ExitAttack

    //2페이즈 각 상태 애니메이션이 종료될때 조건 초기화하는 함수
    public void P2ExitAttack()
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

    public void ChangePhase()
    {
        bossBodyAni.SetBool("isChangePhase", false);
        bossBodyAni.SetBool("isP2Idle", true);
    } //ExitChangePhase

    public void Dead()
    {
        bossBodyAni.SetBool("isP2Idle", false);
        bossBodyAni.SetBool("isP2RightAttackA", false);
        bossBodyAni.SetBool("isP2LeftAttackA", false);
        bossBodyAni.SetBool("isP2RightAttackB", false);
        bossBodyAni.SetBool("isP2LeftAttackB", false);
        bossBodyAni.SetBool("isP2AttackC", false);
        bossBodyAni.SetBool("isP2FistSlam", false);
        bossBodyAni.SetBool("isP2Groggy", false);
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
}
