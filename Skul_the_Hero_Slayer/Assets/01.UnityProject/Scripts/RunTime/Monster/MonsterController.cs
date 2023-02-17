using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState
    {
        IDLE = 0,
        MOVE,
        ATTACK,
        DEAD
    };

    public Monster monster;
    private StateMachine _stateMachine;
    public StateMachine stateMachine { get; private set; }
    private Dictionary<MonsterState, IMonsterState> dicState = new Dictionary<MonsterState, IMonsterState>();

    // Start is called before the first frame update
    void Start()
    {
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        Debug.Log(monster._name);
        dicState.Add(MonsterState.IDLE, idle);
        dicState.Add(MonsterState.MOVE, move);

        stateMachine = new StateMachine(idle, this);
    }

    float a;
    // Update is called once per frame
    void Update()
    {
        a += Time.deltaTime;
        if (a >= 2f)
        {
            stateMachine.SetState(dicState[MonsterState.MOVE]);
        }
        stateMachine.DoUpdate();
    } //Update

    void FixedUpdate()
    {
        stateMachine.DoFixedUpdate();
    } //FixedUpdate
}
