using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState
    {
        IDLE = 0,
        MOVE,
        SEARCH,
        ATTACK,
        DEAD
    };

    public Monster monster;
    public MonsterState enumState = MonsterState.IDLE;
    private StateMachine _stateMachine;
    public StateMachine stateMachine { get; private set; }
    private Dictionary<MonsterState, IMonsterState> dicState = new Dictionary<MonsterState, IMonsterState>();

    // Start is called before the first frame update
    void Start()
    {
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        IMonsterState search= new TagetSearch();
        IMonsterState attack = new MonsterAttack();
        dicState.Add(MonsterState.IDLE, idle);
        dicState.Add(MonsterState.MOVE, move);
        dicState.Add(MonsterState.SEARCH, search);
        dicState.Add(MonsterState.ATTACK, attack);

        stateMachine = new StateMachine(idle, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (monster.tagetSearchRay.hit == null && enumState != MonsterState.MOVE)
        {
            stateMachine.SetState(dicState[MonsterState.MOVE]);
        }
        if(monster.tagetSearchRay.hit != null)
        {
            float distance = Vector2.Distance(monster.transform.position, monster.tagetSearchRay.hit.transform.position);
            if(monster.attackRange < distance)
            {
                stateMachine.SetState(dicState[MonsterState.SEARCH]);
            }
            else
            {
                stateMachine.SetState(dicState[MonsterState.ATTACK]);
            }
        }
        
        
        
        // AttackStart();
        stateMachine.DoUpdate();
    } //Update

    void FixedUpdate()
    {
        stateMachine.DoFixedUpdate();
    } //FixedUpdate

    // private void AttackStart()
    // {
    //     if(monster.tagetSearchRay.hit != null)
    //     {
    //         Debug.Log("공격시작!");
    //         stateMachine.SetState(dicState[MonsterState.ATTACK]);
    //     }
    //     else
    //     {
    //         stateMachine.SetState(dicState[MonsterState.MOVE]);
    //     }
    // }

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
