using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalMonster : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    // Start is called before the first frame update
    void Start()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
        this.monsterRb = gameObject.GetComponentMust<Rigidbody2D>();
        this.monsterAudio = gameObject.GetComponentMust<AudioSource>();
        this.monsterAni = gameObject.GetComponentMust<Animator>();
        InitMonsterData(monsterData);
        monsterController.monster = (Monster)(this as Monster );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
