using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    private Animator bigWoodenAni;
    
    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        monsterData = Resources.Load("BigWooden") as MonsterData;
        InitMonsterData(monsterData);
        //몬스터컨트롤러에서 몬스터타입 변수로 접근가능하게 하기 위한 처리
        //몬스터컨트롤러의 몬스터타입 변수에 자신을 몬스터로 캐스팅하여 저장
        monsterController.monster = (Monster)(this as Monster);
        bigWoodenAni = gameObject.GetComponent<Animator>();
    }
}
