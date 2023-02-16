using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class MonsterController : MonoBehaviour
{
    Monster monster;
    public enum States
    {
        IDLE, 
        ATTACK, 
        DIE, 
        MOVE,
        TRACE,
    }

    StateMachine<States> fsm;
    void Awake()
    {
        monster = gameObject.GetComponent<Monster>();
        fsm = StateMachine<States>.Initialize(this); //2. The main bit of "magic". 

        fsm.ChangeState(States.IDLE);
    }
    IEnumerator IDLE_Enter()
    {
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("4");
    }

    void IDLE_Update()
    {
        Debug.Log("5");
        if(monster.hp <=0)
        {
            //fsm.ChangeState(States.DIE);
        }
        Debug.Log("6");
    }
    void IDLE_FixedUpdate()
    {

    }
    void IDLE_Exit()
    {

    }
    void DIE_Enter()
    {
        Debug.Log("뒤짐");
    }
    void ATTACK_Update()
    {
        
    }
    // void IDLE_OntriggerEnter2D(Collision2D col)
    // {
    //     Debug.Log("dddd");
    //     fsm.ChangeState(States.DIE);
    // }



    // public enum MonsterState
    // {
    //     IDLE = 0,
    //     MOVE,
    //     ATTACK,
    //     DEAD
    // };

    // private StateMachine _stateMachine;
    // public StateMachine stateMachine{get;private set;}
    // private Dictionary<MonsterState, IMonsterState> dicState = new Dictionary<MonsterState, IMonsterState>();


    // // Start is called before the first frame update
    // void Start()
    // {
    //     IMonsterState idle = new MonsterIdle();
    //     IMonsterState move = new MonsterMove();

    //     dicState.Add(MonsterState.IDLE, idle);
    //     dicState.Add(MonsterState.MOVE, move);

    //     // stateMachine = new StateMachine(idle);
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
