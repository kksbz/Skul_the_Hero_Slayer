using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string playerName;
    public string PlayerName { get { return playerName; } }
    [SerializeField]
    private int minDamage;
    public int MinDamage { get { return minDamage; } }
    [SerializeField]
    private int maxDamage;
    public int MaxDamage { get { return maxDamage; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
}
