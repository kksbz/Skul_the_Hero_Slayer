using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string   _name;
    public int      hp;
    public int      maxHp;
    public int      minDamage;
    public int      maxDamage;
    public float    moveSpeed;
    public float    sightRange;
    public Rigidbody2D monsterRb;
    public AudioSource monsterAudio;
    public Animator monsterAni;
    public GroundCheckRay groundCheckRay;
    public void InitMonsterData(MonsterData data)
    {
        this._name          = data.name;
        this.hp             = data.MonsterHp;
        this.maxHp          = data.MonsterMaxHp;
        this.minDamage      = data.MinDamage;
        this.maxDamage      = data.MaxDamage;
        this.moveSpeed      = data.MoveSpeed;
        this.sightRange     = data.SightRange;
        this.monsterRb      = gameObject.GetComponentMust<Rigidbody2D>();
        this.monsterAudio   = gameObject.GetComponentMust<AudioSource>();
        this.monsterAni     = gameObject.GetComponentMust<Animator>();
        this.groundCheckRay = gameObject.GetComponentMust<GroundCheckRay>();
    } //InitMonsterData
}
