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
        dicState.Add(PlayerState.IDLE, idle);
        dicState.Add(PlayerState.MOVE, move);
        dicState.Add(PlayerState.JUMP, jump);
        pStateMachine = new PStateMachine(idle, this);
    } //Start

    void FixedUpdate()
    {
        pStateMachine.DoFixedUpdate();
    } //FixedUpdate
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            pStateMachine.SetState(dicState[PlayerState.MOVE]);
        }
        else
        {
            pStateMachine.SetState(dicState[PlayerState.IDLE]);
        }
        if(Input.GetKey(KeyCode.C))
        {
            pStateMachine.SetState(dicState[PlayerState.JUMP]);
        }
        pStateMachine.DoUpdate();
    } //Update
}
