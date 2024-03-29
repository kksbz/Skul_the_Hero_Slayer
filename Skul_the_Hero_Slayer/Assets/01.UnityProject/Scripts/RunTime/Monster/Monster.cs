using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector] public string _name;
    [HideInInspector] public int hp;
    [HideInInspector] public int maxHp;
    [HideInInspector] public int minDamage;
    [HideInInspector] public int maxDamage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float sightRangeX;
    [HideInInspector] public float sightRangeY;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float meleeAttackRange;
    [HideInInspector] public float hpBarPosY;
    [HideInInspector] public float hpBarWidth;
    [HideInInspector] public bool hasAdditionalAttack;
    [HideInInspector] public Rigidbody2D monsterRb;
    [HideInInspector] public AudioSource monsterAudio;
    [HideInInspector] public Animator monsterAni;
    [HideInInspector] public GroundCheckRay groundCheckRay;
    [HideInInspector] public TagetSearchRay tagetSearchRay;
    [HideInInspector] public AudioClip deadSound;
    [HideInInspector] public AudioClip hitSound;
    [HideInInspector] public AudioClip attackASound;
    [HideInInspector] public AudioClip attackBSound;
    public void InitMonsterData(MonsterData data)
    {
        this._name               = data.name;
        this.hp                  = data.MonsterHp;
        this.maxHp               = data.MonsterMaxHp;
        this.minDamage           = data.MinDamage;
        this.maxDamage           = data.MaxDamage;
        this.moveSpeed           = data.MoveSpeed;
        this.sightRangeX         = data.SightRangeX;
        this.sightRangeY         = data.SightRangeY;
        this.attackRange         = data.AttackRange;
        this.hpBarPosY           = data.HpBarPosY;
        this.hpBarWidth          = data.HpBarWidth;
        this.meleeAttackRange    = data.MeleeAttackRange;
        this.hasAdditionalAttack = data.HasAdditionalAttack;
        this.deadSound           = data.DeadAudio;
        this.hitSound            = data.HitAudio;
        this.attackASound        = data.AtkAAudio;
        this.attackBSound        = data.AtkBAudio;
        this.monsterRb = gameObject.GetComponentMust<Rigidbody2D>();
        this.monsterAudio = gameObject.GetComponentMust<AudioSource>();
        this.monsterAni = gameObject.GetComponentMust<Animator>();
        this.groundCheckRay = gameObject.GetComponentMust<GroundCheckRay>();
        this.tagetSearchRay = gameObject.GetComponentMust<TagetSearchRay>();
    } //InitMonsterData

    //공격하는 함수, 몬스터를 상속받는 각 몬스터의 공격방식이 다르기 때문에 override 시킴
    public virtual void AttackA()
    {
        /*Do Nothing*/
    } //AttackA

    public virtual void AttackB()
    {
        /*Do Nothing*/
    } //AttackB
}
