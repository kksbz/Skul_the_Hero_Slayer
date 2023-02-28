using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    NOMAL,
    GREENE,
    BIG
}
public class MonsterSpawner : MonoBehaviour
{
    public StageData stageData;
    private List<GameObject> monsterPool;
    private GameObject monsterPrefab;
    private Vector3 spawnCoordinate;
    private List<Vector3> spawnCoordinateNomal;
    private List<Vector3> spawnCoordinateRange;
    private List<Vector3> spawnCoordinateTurret;

    // Start is called before the first frame update
    void Start()
    {
        monsterPool = new List<GameObject>(stageData.MaxSpawnNumber);
        spawnCoordinateNomal = new List<Vector3>();
        spawnCoordinateRange = new List<Vector3>();
        spawnCoordinateTurret = new List<Vector3>();
        SetupSpawnCoordinate();
        SetupMonsterList();
    } //Start

    //StateData의 정보로 몬스터생성, 스폰좌표 설정 하는 함수
    private List<GameObject> SetupMonsterList()
    {
        for (int i = 0; i < stageData.MaxSpawnNumber; i++)
        {
            if (i < stageData.SpawnPrefab1_Number)
            {
                monsterPrefab = stageData.MonsterPrefab1;
                int dinateNumber = Random.RandomRange(0, spawnCoordinateNomal.Count);
                spawnCoordinate = spawnCoordinateNomal[dinateNumber];
            }
            else if (stageData.SpawnPrefab1_Number <= i && i < stageData.SpawnPrefab1_Number + stageData.SpawnPrefab2_Number)
            {
                monsterPrefab = stageData.MonsterPrefab2;
                int dinateNumber = Random.RandomRange(0, spawnCoordinateRange.Count);
                spawnCoordinate = spawnCoordinateRange[dinateNumber];
            }
            else if (stageData.SpawnPrefab1_Number + stageData.SpawnPrefab2_Number <= i)
            {
                monsterPrefab = stageData.MonsterPrefab3;
                int dinateNumber = Random.RandomRange(0, spawnCoordinateTurret.Count);
                spawnCoordinate = spawnCoordinateTurret[dinateNumber];
                spawnCoordinateTurret.Remove(spawnCoordinate);
            }
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
            monster.transform.parent = gameObject.transform;
            monster.transform.position = spawnCoordinate;
            monsterPool.Add(monster);
            GameManager.Instance.monsterRemainingNumber += 1;
        }
        return monsterPool;
    } //SetupMonsterList

    //statgeData에 입력한 좌표를 List에 저장하는 함수
    private void SetupSpawnCoordinate()
    {
        spawnCoordinateNomal.Add(stageData.SpawnCoordinate_Nomal1);
        spawnCoordinateNomal.Add(stageData.SpawnCoordinate_Nomal2);
        spawnCoordinateNomal.Add(stageData.SpawnCoordinate_Nomal3);
        spawnCoordinateNomal.Add(stageData.SpawnCoordinate_Nomal4);

        spawnCoordinateRange.Add(stageData.SpawnCoordinate_Range1);
        spawnCoordinateRange.Add(stageData.SpawnCoordinate_Range2);
        spawnCoordinateRange.Add(stageData.SpawnCoordinate_Range3);

        spawnCoordinateTurret.Add(stageData.SpawnCoordinate_turret1);
        spawnCoordinateTurret.Add(stageData.SpawnCoordinate_turret2);
        spawnCoordinateTurret.Add(stageData.SpawnCoordinate_turret3);
    } //SetupSpawnCoordinate
}
