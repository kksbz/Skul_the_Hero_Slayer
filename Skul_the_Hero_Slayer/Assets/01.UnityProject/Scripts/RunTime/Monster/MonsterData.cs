using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private string monsterName; //몬스터이름
    public string MonsterName { get { return monsterName; } }
    [SerializeField]
    private int monsterHp; //Hp
    public int MonsterHp { get { return monsterHp; } }
    [SerializeField]
    private int monsterMaxHp; //MaxHp
    public int MonsterMaxHp { get { return monsterMaxHp; } }
    [SerializeField]
    private int minDamage; //최소 데미지
    public int MinDamage { get { return minDamage; } }
    [SerializeField]
    private int maxDamage; //최대 데미지
    public int MaxDamage { get { return maxDamage; } }
    [SerializeField]
    private float moveSpeed; //이동속도
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private float sightRangeX; //감지범위 X
    public float SightRangeX { get { return sightRangeX; } }
    [SerializeField]
    private float sightRangeY; //감지범위 Y
    public float SightRangeY { get { return sightRangeY; } }
    [SerializeField]
    private float attackRange; //공격 사거리
    public float AttackRange { get { return attackRange; } }
    [SerializeField]
    private float meleeAttackRange; //근접공격 사거리
    public float MeleeAttackRange { get { return meleeAttackRange; } }
    [SerializeField]
    private bool hasAdditionalAttack; //공격패턴 추가 체크
    public bool HasAdditionalAttack { get { return hasAdditionalAttack; } }
    [SerializeField]
    private float hpBarPosY; //HpBar 위치 조절 offsetY
    public float HpBarPosY { get { return hpBarPosY; } }
    [SerializeField]
    private float hpBarWidth; //HpBar Width 조절 offset
    public float HpBarWidth { get { return hpBarWidth; } }
    [SerializeField]
    private AudioClip deadAudio; //사망 사운드
    public AudioClip DeadAudio { get { return deadAudio; } }
    [SerializeField]
    private AudioClip hitAudio; //피격 사운드
    public AudioClip HitAudio { get { return hitAudio; } }
    [SerializeField]
    private AudioClip atkAAudio; //공격A 사운드
    public AudioClip AtkAAudio { get { return atkAAudio; } }
    [SerializeField]
    private AudioClip atkBAudio; //공격B 사운드
    public AudioClip AtkBAudio { get { return atkBAudio; } }
}
