using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public string _name;
    [HideInInspector] public int skulIndex;
    [HideInInspector] public int minDamage;
    [HideInInspector] public int maxDamage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float groundCheckLength;
    [HideInInspector] public float skillACool;
    [HideInInspector] public float skillBCool;
    [HideInInspector] public Sprite skulSprite;
    [HideInInspector] public Sprite skillASprite;
    [HideInInspector] public Sprite skillBSprite;
    [HideInInspector] public Rigidbody2D playerRb;
    [HideInInspector] public AudioSource playerAudio;
    [HideInInspector] public Animator playerAni;
    [HideInInspector] public AudioClip dashSound;
    [HideInInspector] public AudioClip jumpSound;
    [HideInInspector] public AudioClip deadSound;
    [HideInInspector] public AudioClip hitSound;
    [HideInInspector] public AudioClip atkASound;
    [HideInInspector] public AudioClip atkBSound;
    [HideInInspector] public AudioClip jumpAtkSound;
    [HideInInspector] public AudioClip skillASound;
    [HideInInspector] public AudioClip skillBSound;
    [HideInInspector] public AudioClip switchSound;

    private CapsuleCollider2D playerCollider;

    //플레이어데이터 초기화하는 함수
    public void InitPlayerData(PlayerData data)
    {
        this._name = data.PlayerName;
        this.minDamage = data.MinDamage;
        this.maxDamage = data.MaxDamage;
        this.moveSpeed = data.MoveSpeed;
        this.groundCheckLength = data.GroundRayLength;
        this.skulSprite = data.SkulSprite;
        this.skillASprite = data.SkillASprite;
        this.skillBSprite = data.SkillBSprite;
        this.skillACool = data.SkillACool;
        this.skillBCool = data.SkillBCool;
        this.playerRb = gameObject.GetComponentMust<Rigidbody2D>();
        this.playerAudio = gameObject.GetComponentMust<AudioSource>();
        this.playerAni = gameObject.GetComponentMust<Animator>();
        this.playerAni.runtimeAnimatorController = data.Controller;
        this.playerCollider = gameObject.GetComponentMust<CapsuleCollider2D>();
        this.playerCollider.size = new Vector2(data.ColliderSizeX, data.ColliderSizeY);
        this.skulIndex = data.SkulIndex;
        //사운드클립
        this.dashSound = data.DashAudio;
        this.jumpSound = data.JumpAudio;
        this.deadSound = data.DeadAudio;
        this.hitSound = data.HitAudio;
        this.atkASound = data.AtkAAudio;
        this.atkBSound = data.AtkBAudio;
        this.jumpAtkSound = data.JumpAtkAudio;
        this.skillASound = data.SkillAAudio;
        this.skillBSound = data.SkillBAudio;
        this.switchSound = data.SwitchAudio;
    } //InitMonsterData
}
