using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string playerName; //플레이어 스컬 이름
    public string PlayerName { get { return playerName; } }
    [SerializeField]
    private Sprite skulSprite; //플레이어 스컬 이미지
    public Sprite SkulSprite { get { return skulSprite; } }
    [SerializeField]
    private Sprite skillASprite; //스킬A이미지
    public Sprite SkillASprite { get { return skillASprite; } }
    [SerializeField]
    private Sprite skillBSprite; //스킬B이미지
    public Sprite SkillBSprite { get { return skillBSprite; } }
    [SerializeField]
    private int skulIndex; //플레이어 스컬 번호
    public int SkulIndex { get { return skulIndex; } }
    [SerializeField]
    private int minDamage; //플레이어 최소 데미지
    public int MinDamage { get { return minDamage; } }
    [SerializeField]
    private int maxDamage; //플레이어 최대 데미지
    public int MaxDamage { get { return maxDamage; } }
    [SerializeField]
    private float moveSpeed; //플레이어 이동속도
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private RuntimeAnimatorController controller; //플레이어 스컬 런타임애니컨트롤러
    public RuntimeAnimatorController Controller { get { return controller; } }
    [SerializeField]
    private float colliderSizeX; //플레이어 스컬 콜라이더 사이즈X
    public float ColliderSizeX { get { return colliderSizeX; } }
    [SerializeField]
    private float colliderSizeY; //플레이어 스컬 콜라이더 사이즈Y
    public float ColliderSizeY { get { return colliderSizeY; } }
    [SerializeField]
    private float groundRayLength; //플레이어 스컬 그라운드체크레이어 길이
    public float GroundRayLength { get { return groundRayLength; } }
    [SerializeField]
    private float skillACool; //스킬A 쿨다운
    public float SkillACool { get { return skillACool; } }
    [SerializeField]
    private float skillBCool; //스킬B 쿨다운
    public float SkillBCool { get { return skillBCool; } }
    [SerializeField]
    private AudioClip dashAudio; //대쉬 사운드
    public AudioClip DashAudio { get { return dashAudio; } }
    [SerializeField]
    private AudioClip jumpAudio; //점프 사운드
    public AudioClip JumpAudio { get { return jumpAudio; } }
    [SerializeField]
    private AudioClip deadAudio; //사망 사운드
    public AudioClip DeadAudio { get { return deadAudio; } }
    [SerializeField]
    private AudioClip hitAudio; //히트 사운드
    public AudioClip HitAudio { get { return hitAudio; } }
    [SerializeField]
    private AudioClip atkAAudio; //공격A 사운드
    public AudioClip AtkAAudio { get { return atkAAudio; } }
    [SerializeField]
    private AudioClip atkBAudio; //공격B 사운드
    public AudioClip AtkBAudio { get { return atkBAudio; } }
    [SerializeField]
    private AudioClip jumpAtkAudio; //점프공격 사운드
    public AudioClip JumpAtkAudio { get { return jumpAtkAudio; } }
    [SerializeField]
    private AudioClip skillAAudio; //스킬A 사운드
    public AudioClip SkillAAudio { get { return skillAAudio; } }
    [SerializeField]
    private AudioClip skillBAudio; //스킬B 사운드
    public AudioClip SkillBAudio { get { return skillBAudio; } }
    [SerializeField]
    private AudioClip switchAudio; //스왑스킬 사운드
    public AudioClip SwitchAudio { get { return switchAudio; } }
}
