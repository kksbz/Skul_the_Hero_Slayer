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

    //interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    } //CoroutineDeligate

    //코루틴을 대신 종료시켜줄 함수
    public void StopCoroutineDeligate(IEnumerator func)
    {
        StopCoroutine(func);
    } //StopCoroutineDeligate
}
