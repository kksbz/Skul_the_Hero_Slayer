using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //몬스터의 상태종류
    public enum MonsterState
    {
        IDLE = 0,
        MOVE,
        SEARCH,
        ATTACK,
        HIT,
        DEAD
    }; //MonsterState

    private EnemyHealthBar hpBar;
    public Monster monster;
    public MonsterState enumState = MonsterState.IDLE; //몬스터의 현재상태를 체크하기위한 변수
    private StateMachine _stateMachine; //입력받은 상태를 처리하기 위한 StateManchine
    public StateMachine stateMachine { get; private set; }
    private Dictionary<MonsterState, IMonsterState> dicState = new Dictionary<MonsterState, IMonsterState>(); //각 상태를 갖고 있을 딕셔너리
    public int currentHp; //현재 Hp의 정보를 담을 변수
    // Start is called before the first frame update
    void Start()
    {
        hpBar = gameObject.GetComponentMust<EnemyHealthBar>();
        currentHp = monster.hp;
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        IMonsterState search = new TagetSearch();
        IMonsterState attack = new MonsterAttack();
        IMonsterState hit = new MonsterHit();
        IMonsterState dead = new MonsterDead();
        //각 상태를 딕셔너리로 저장
        dicState.Add(MonsterState.IDLE, idle);
        dicState.Add(MonsterState.MOVE, move);
        dicState.Add(MonsterState.SEARCH, search);
        dicState.Add(MonsterState.ATTACK, attack);
        dicState.Add(MonsterState.HIT, hit);
        dicState.Add(MonsterState.DEAD, dead);

        //입력받은 상태를 처리해 줄 StateMachine 초기화
        stateMachine = new StateMachine(idle, this);
    }

    // Update is called once per frame
    void Update()
    {
        InputState();
        stateMachine.DoUpdate();
    } //Update

    void FixedUpdate()
    {
        stateMachine.DoFixedUpdate();
    } //FixedUpdate

    //상황에 맞는 State를 입력해주는 함수
    private void InputState()
    {
        //몬스터의 hp가 0보다 작거나 같으면 Dead상태
        if (monster.hp <= 0)
        {
            stateMachine.SetState(dicState[MonsterState.DEAD]);
        }
        if (monster.hp < currentHp)
        {
            hpBar.canvas.SetActive(true);
            currentHp = monster.hp;
            if (monster.moveSpeed == 0)
            {
                return;
            }
            stateMachine.SetState(dicState[MonsterState.HIT]);
        }
        if (enumState != MonsterState.HIT)
        {
            //몬스터의 탐색범위에 타겟이 없고 Move상태가 아닐경우
            if (monster.tagetSearchRay.hit == null && enumState != MonsterState.MOVE)
            {
                stateMachine.SetState(dicState[MonsterState.MOVE]);
            }
            //몬스터의 탐색범위에 타겟이 있을 경우
            if (monster.tagetSearchRay.hit != null)
            {
                float distance = Vector2.Distance(monster.transform.position, monster.tagetSearchRay.hit.transform.position);
                //타겟과 자신의 거리가 공격사거리보다 크면 Search상태, 작으면 Attack상태로 전환
                if (monster.attackRange < distance)
                {
                    //공격애니메이션이 끝날경우 Search상태로 전환
                    if (monster.monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    {
                        stateMachine.SetState(dicState[MonsterState.SEARCH]);
                    }
                }
                else
                {
                    stateMachine.SetState(dicState[MonsterState.ATTACK]);
                }
            }
        }
    } //InputState

    //interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    } //CoroutineDeligate
}
