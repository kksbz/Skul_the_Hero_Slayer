using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    MonsterController monsterController;
    public string   _name;
    public int      hp;
    public int      maxHp;
    public int      minDamage;
    public int      maxDamage;
    public float    moveSpeed;
    public float    sightRange;

    public void InitMonsterData(MonsterData data)
    {
        this._name          = data.name;
        this.hp             = data.MonsterHp;
        this.maxHp          = data.MonsterMaxHp;
        this.minDamage      = data.MinDamage;
        this.maxDamage      = data.MaxDamage;
        this.moveSpeed      = data.MoveSpeed;
        this.sightRange     = data.SightRange;
    } //InitMonsterData

    public virtual void Attack()
    {

    }
}
public class NamuMonster : Monster
{
    public override void Attack()
    {
        //몬스터타입별로 공격 오버라이드해서 구현하자
    }
}
