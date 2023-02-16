using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    NamuMonster,
    GREENE,
    BIG
}
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<MonsterData> monsterDatas;
    [SerializeField]
    private GameObject monsterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void M_SPAWN(params MonsterType[] types)
    {

    }
    // public Monster SpawnMonster(MonsterType type)
    // {
    //     Resources.Load<GameObject>("Prefabs/"+type.ToString());
    //     Monster monster = Instantiate(monsterPrefab)?.GetComponent<Monster>();
    //     monster.InitMonsterData(monsterDatas[0]);
        
    // }
    // Update is called once per frame
    void Update()
    {
        
    }

}
