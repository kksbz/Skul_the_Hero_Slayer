using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE = 0,
        MOVE,
        JUMP,
        DASH,
        ATTACK,
        DEAD
    }; //PlayerState

    public Player player;
    public int playerHp;
    public int playerMaxHp = 100;
    public bool isGround = true;
    public bool canDash = true;
    public PlayerState enumState = PlayerState.IDLE;
    private PStateMachine _pStateMachine;
    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dicState = new Dictionary<PlayerState, IPlayerState>();


    // Start is called before the first frame update
    void Start()
    {
        playerHp = playerMaxHp;
        IPlayerState idle = new PlayerIdle();
        IPlayerState move = new PlayerMove();
        IPlayerState jump = new PlayerJump();
        IPlayerState dash = new PlayerDash();
        IPlayerState attack = new PlayerAttack();

        dicState.Add(PlayerState.IDLE, idle);
        dicState.Add(PlayerState.MOVE, move);
        dicState.Add(PlayerState.JUMP, jump);
        dicState.Add(PlayerState.DASH, dash);
        dicState.Add(PlayerState.ATTACK, attack);
        pStateMachine = new PStateMachine(idle, this);
    } //Start
    void FixedUpdate()
    {
        pStateMachine.DoFixedUpdate();
    } //FixedUpdate
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && isGround == true)
        {
            pStateMachine.SetState(dicState[PlayerState.MOVE]);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            pStateMachine.SetState(dicState[PlayerState.JUMP]);
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash == true)
        {
            pStateMachine.SetState(dicState[PlayerState.DASH]);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            pStateMachine.SetState(dicState[PlayerState.ATTACK]);
        }
        if (Input.anyKey == false&&isGround == true)
        {
            pStateMachine.SetState(dicState[PlayerState.IDLE]);
        }
        pStateMachine.DoUpdate();
    } //Update

    //interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    } //CoroutineDeligate

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    } //OnCollisionEnter2D
}
