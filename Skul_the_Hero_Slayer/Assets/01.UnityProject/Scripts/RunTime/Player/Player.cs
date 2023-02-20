using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public string _name;
    [HideInInspector] public int minDamage;
    [HideInInspector] public int maxDamage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public Rigidbody2D playerRb;
    [HideInInspector] public AudioSource playerAudio;
    [HideInInspector] public Animator playerAni;

    //플레이어데이터 초기화하는 함수
    public void InitPlayerData(PlayerData data)
    {
        this._name          = data.name;
        this.minDamage      = data.MinDamage;
        this.maxDamage      = data.MaxDamage;
        this.moveSpeed      = data.MoveSpeed;
        this.playerRb       = gameObject.GetComponentMust<Rigidbody2D>();
        this.playerAudio    = gameObject.GetComponentMust<AudioSource>();
        this.playerAni      = gameObject.GetComponentMust<Animator>();
    } //InitMonsterData

    //플레이어 AttackA
    public virtual void AttackA()
    {
        /*Do Nothing*/
    } //AttackA

    //플레이어 AttackB
    public virtual void AttackB()
    {
        /*Do Nothing*/
    } //AttackB

    //플레이어 SkillA
    public virtual void SkillA()
    {
        /*Do Nothing*/
    } //AttackA

    //플레이어 SkillB
    public virtual void SkillB()
    {
        /*Do Nothing*/
    } //AttackB
}
