using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Object/StageData", order = int.MaxValue)]
public class StageData : ScriptableObject
{
    [SerializeField]
    private string stageName; //스테이지 이름
    public string StageName { get { return stageName; } }
    [SerializeField]
    private int maxSpawnNumber; //스테이지에 존재하는 최대 몬스터 수
    public int MaxSpawnNumber { get { return maxSpawnNumber; } }
    [SerializeField]
    private int spawnPrefab1_Number; //프리팹1 생성할 갯수
    public int SpawnPrefab1_Number { get { return spawnPrefab1_Number; } }
    [SerializeField]
    private int spawnPrefab2_Number; ////프리팹2 생성할 갯수
    public int SpawnPrefab2_Number { get { return spawnPrefab2_Number; } }
    [SerializeField]
    private int spawnPrefab3_Number; ////프리팹3 생성할 갯수
    public int SpawnPrefab3_Number { get { return spawnPrefab3_Number; } }
    [SerializeField]
    private GameObject monsterPrefab1; //프리팹1
    public GameObject MonsterPrefab1 { get { return monsterPrefab1; } }
    [SerializeField]
    private GameObject monsterPrefab2; //프리팹2
    public GameObject MonsterPrefab2 { get { return monsterPrefab2; } }
    [SerializeField]
    private GameObject monsterPrefab3; //프리팹3
    public GameObject MonsterPrefab3 { get { return monsterPrefab3; } }
    [SerializeField]
    private Vector3 spawnCoordinate_Nomal1; //일반 몬스터 스폰 좌표
    public Vector3 SpawnCoordinate_Nomal1 { get { return spawnCoordinate_Nomal1; } }
    [SerializeField]
    private Vector3 spawnCoordinate_Nomal2;
    public Vector3 SpawnCoordinate_Nomal2 { get { return spawnCoordinate_Nomal2; } }
    [SerializeField]
    private Vector3 spawnCoordinate_Nomal3;
    public Vector3 SpawnCoordinate_Nomal3 { get { return spawnCoordinate_Nomal3; } }
    [SerializeField]
    private Vector3 spawnCoordinate_Nomal4;
    public Vector3 SpawnCoordinate_Nomal4 { get { return spawnCoordinate_Nomal4; } }
    [SerializeField]
    private Vector3 spawnCoordinate_Range1; //원거리 몬스터 스폰 좌표
    public Vector3 SpawnCoordinate_Range1 { get { return spawnCoordinate_Range1; } }
    [SerializeField]
    private Vector3 spawnCoordinate_Range2;
    public Vector3 SpawnCoordinate_Range2 { get { return spawnCoordinate_Range2; } }
    [SerializeField]
    private Vector3 spawnCoordinate_Range3;
    public Vector3 SpawnCoordinate_Range3 { get { return spawnCoordinate_Range3; } }
    [SerializeField]
    private Vector3 spawnCoordinate_turret1; //고정형 몬스터 스폰 좌표
    public Vector3 SpawnCoordinate_turret1 { get { return spawnCoordinate_turret1; } }
    [SerializeField]
    private Vector3 spawnCoordinate_turret2;
    public Vector3 SpawnCoordinate_turret2 { get { return spawnCoordinate_turret2; } }
    [SerializeField]
    private Vector3 spawnCoordinate_turret3;
    public Vector3 SpawnCoordinate_turret3 { get { return spawnCoordinate_turret3; } }
}
