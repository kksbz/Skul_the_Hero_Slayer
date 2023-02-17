using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    
    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("GreenWooden") as MonsterData;
        InitMonsterData(monsterData);
        monsterController.monster = (Monster)(this as Monster);
    }
}
