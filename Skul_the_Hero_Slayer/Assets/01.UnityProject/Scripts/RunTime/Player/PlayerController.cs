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
        SKILLA,
        SKILLB,
        DEAD
    }; //PlayerState
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
    public List<Player> playerSkulList; //플레이어가 사용할 수 있는 Skul의 List
    private float swapCoolDown = 0f;
    // Start is called before the first frame update

    void Start()
    {
        playerSkulList = new List<Player>();
        Player possibleSkul = default;
#if !DEBUG_ENABLED
        //기본 스컬의 런타임애니컨트롤러를 저장 => 스킬A,B사용시 런타임애니컨트롤러를 변경하는 로직
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == GData.CASTLELOBBY_SCENE_NAME)
        {
            possibleSkul = gameObject.AddComponent<Skul>();
            playerSkulList.Add(possibleSkul);
        }
        else
        {
            SaveManager.Instance.LoadData(this);
        }
#else
        possibleSkul = gameObject.AddComponent<Skul>();
        playerSkulList.Add(possibleSkul);
#endif

        BeforeChangeRuntimeC = player.playerAni.runtimeAnimatorController;
        isGroundRay = gameObject.GetComponentMust<PlayerGroundCheck>();
        playerHp = playerMaxHp;
        IPlayerState idle = new PlayerIdle();
        IPlayerState move = new PlayerMove();
        IPlayerState jump = new PlayerJump();
        IPlayerState dash = new PlayerDash();
        IPlayerState attack = new PlayerAttack();
        IPlayerState skillA = new PlayerSkillA();
        IPlayerState skillB = new PlayerSkillB();

        dicState.Add(PlayerState.IDLE, idle);
        dicState.Add(PlayerState.MOVE, move);
        dicState.Add(PlayerState.JUMP, jump);
        dicState.Add(PlayerState.DASH, dash);
        dicState.Add(PlayerState.ATTACK, attack);
        dicState.Add(PlayerState.SKILLA, skillA);
        dicState.Add(PlayerState.SKILLB, skillB);
        pStateMachine = new PStateMachine(idle, this);
    } //Start
    void FixedUpdate()
    {
        pStateMachine.DoFixedUpdate();
    } //FixedUpdate

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.LeftArrow))
            && isGroundRay.hit.collider != null
            && enumState != PlayerState.JUMP
            && enumState != PlayerState.DASH
            && enumState != PlayerState.ATTACK
            && enumState != PlayerState.SKILLA
            && enumState != PlayerState.SKILLB)
        {
            pStateMachine.SetState(dicState[PlayerState.MOVE]);
        }

        if (isGroundRay.hit.collider != null
        && Input.anyKeyDown == false
        && enumState != PlayerState.MOVE
        /*&& enumState != PlayerState.JUMP*/
        && enumState != PlayerState.DASH
        && enumState != PlayerState.ATTACK
        && enumState != PlayerState.SKILLA
        && enumState != PlayerState.SKILLB)
        {
            pStateMachine.SetState(dicState[PlayerState.IDLE]);
        }

        if (Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.DownArrow))
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            pStateMachine.SetState(dicState[PlayerState.SKILLA]);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            pStateMachine.SetState(dicState[PlayerState.SKILLB]);
        }

        if (Input.GetKeyDown(KeyCode.Space) && swapCoolDown == 0f)
        {
            ChangePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveManager.Instance.SaveData(this);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveManager.Instance.LoadData(this);
        }

        // if (enumState != PlayerState.JUMP && enumState != PlayerState.ATTACK)
        // {
        //     if (player.playerRb.velocity.y < -1)
        //     {
        //         player.playerAni.SetBool("isFall", true);
        //     }
        //     else
        //     {
        //         player.playerAni.SetBool("isFall", false);
        //     }
        // }
        pStateMachine.DoUpdate();
    } //Update

    //플레이어 스컬교체하는 함수
    private void ChangePlayer()
    {
        //스컬을 1개만 가지고있으면 리턴
        if (playerSkulList.Count < 2)
        {
            return;
        }
        //스컬리스트의 활성화 상태를 반전시킴
        for (int i = 0; i < playerSkulList.Count; i++)
        {
            playerSkulList[i].enabled = !playerSkulList[i].enabled;
        }
        StartCoroutine(SwapCoolDown());
    } //ChangePlayer

    //캐릭터 Swap쿨다운 적용 코루틴 함수
    private IEnumerator SwapCoolDown()
    {
        //스왑쿨다운 6초 설정
        for (int i = 0; i < 60; i++)
        {
            var tick = 0.1f;
            swapCoolDown += tick;
            yield return new WaitForSeconds(tick);
        }
        swapCoolDown = 0f;
    } //SwapCoolDown

    //interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    } //CoroutineDeligate

    private void OnCollisionEnter2D(Collision2D collision)
    {

    } //OnCollisionEnter2D
}
