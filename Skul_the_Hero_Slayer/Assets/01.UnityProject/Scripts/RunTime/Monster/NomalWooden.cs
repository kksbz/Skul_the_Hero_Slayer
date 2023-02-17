using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    
    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("NomalWooden") as MonsterData;
        InitMonsterData(monsterData);
        monsterController.monster = (Monster)(this as Monster);
    }
}
