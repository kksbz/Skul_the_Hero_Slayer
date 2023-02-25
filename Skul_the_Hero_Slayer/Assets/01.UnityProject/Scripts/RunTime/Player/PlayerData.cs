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
}
