using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState
    {
        idle,
        move,
        attack,
        dead
    };

    MonsterState _monsterState = MonsterState.idle;
    private Transform playerTransform;
    public float eyesight = 10f;
    public float attackRange = 2f;
    private bool isDead = false;

    public void changeState(MonsterState state)
    {
        _monsterState = state;
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch(_monsterState)
        {
            case MonsterState.idle:
            break;
            case MonsterState.move:
            break;
            case MonsterState.attack:
            break;
            case MonsterState.dead:
            break;
        }
    }
}
