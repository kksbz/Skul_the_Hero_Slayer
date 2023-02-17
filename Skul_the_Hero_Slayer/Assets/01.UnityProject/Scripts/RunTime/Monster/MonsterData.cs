using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private string monsterName;
    public string MonsterName { get { return monsterName; } }
    [SerializeField]
    private int monsterHp;
    public int MonsterHp { get { return monsterHp; } }
    [SerializeField]
    private int monsterMaxHp;
    public int MonsterMaxHp { get { return monsterMaxHp; } }
    [SerializeField]
    private int minDamage;
    public int MinDamage { get { return minDamage; } }
    [SerializeField]
    private int maxDamage;
    public int MaxDamage { get { return maxDamage; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private float sightRange;
    public float SightRange { get { return sightRange; } }
}
