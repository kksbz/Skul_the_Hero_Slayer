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

    public enum PlayerSkul
    {
        SKUL,
        MAGE
    }
    public PlayerSkul currentSkul = PlayerSkul.SKUL;
    public Player player;
    public int playerHp;
    public int playerMaxHp = 100;
    public bool isGround = true;
    public bool canDash = true;
    public PlayerGroundCheck isGroundRay;
    public PlayerState enumState = PlayerState.IDLE;
    private PStateMachine _pStateMachine;
    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dicState = new Dictionary<PlayerState, IPlayerState>();
    private SpriteRenderer playerSprite;
    public RuntimeAnimatorController BeforeChangeRuntimeC;
    public Animator playerAni;

    // Start is called before the first frame update
    void Start()
    {
        // var basicSkulObj = transform.GetChild((int)currentSkul).gameObject;
        // player = gameObject.AddComponent<Skul>()
        // playerSprite = basicSkulObj.GetComponentMust<SpriteRenderer>();
        // playerAni.runtimeAnimatorController = player.playerAni.runtimeAnimatorController;
        //기본 스컬의 런타임애니컨트롤러를 저장 => 스킬A,B사용시 런타임애니컨트롤러를 변경하는 로직
        BeforeChangeRuntimeC = player.playerAni.runtimeAnimatorController;
        // InitPlayer();
        isGroundRay = gameObject.GetComponentMust<PlayerGroundCheck>();
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
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && isGroundRay.hit.collider != null && enumState != PlayerState.DASH)
        {
            //현재 상태가 Attack상태라면 애니메이션이 끝나고 Move시작
            // if (enumState == PlayerState.ATTACK)
            // {
            //     if (player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            //     {
            //         return;
            //     }
            // }
            // else
            {
                pStateMachine.SetState(dicState[PlayerState.MOVE]);
            }
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
        if (Input.anyKey == false && isGroundRay.hit.collider != null && enumState != PlayerState.DASH)
        {
            pStateMachine.SetState(dicState[PlayerState.IDLE]);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.playerAni.SetBool("isSkillA", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.SkillB();
        }
        pStateMachine.DoUpdate();
    } //Update

    private void InitPlayer()
    {
        GameObject childObj = Resources.Load("Prefabs/Skul") as GameObject;
        playerSprite.sprite = childObj.GetComponentMust<SpriteRenderer>().sprite;
        playerHp = playerMaxHp;
    } //InitPlayer

    private void ChangePlayer()
    {

    } //ChangePlayer

    //interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    } //CoroutineDeligate

    private void OnCollisionEnter2D(Collision2D collision)
    {

    } //OnCollisionEnter2D
}
