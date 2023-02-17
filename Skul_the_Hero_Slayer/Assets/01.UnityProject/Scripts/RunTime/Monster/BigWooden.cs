using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    
    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("BigWooden") as MonsterData;
        InitMonsterData(monsterData);
        monsterController.monster = (Monster)(this as Monster);
    }
}
