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
    public Player player; //컨트롤러에 가져올 플레이어
    public int playerHp;
    public int playerMaxHp = 100;
    public bool isGround = true; //땅 체크
    public bool canDash = true; //대쉬 사용가능 체크
    public PlayerGroundCheck isGroundRay; //땅 체크 레이어
    public PlayerState enumState = PlayerState.IDLE; //기본상태 Idle
    private PStateMachine _pStateMachine; //상태처리 머신
    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dicState = new Dictionary<PlayerState, IPlayerState>(); //각상태를 담을 딕셔너리
    public RuntimeAnimatorController BeforeChangeRuntimeC; //Skul SkillA사용시 머리사라진 모습 런타임애니메이션컨트롤러
    public List<Player> playerSkulList; //플레이어가 사용할 수 있는 Skul의 List
    private SpriteRenderer playerSprite;
    public bool isGetSkulSkillA = false;
    public bool isHit = false;
    private bool isDead = false;
    public int currentHp;
    private float swapCoolDown = 6f; //스왑스킬 쿨다운
    public float SwapCoolDown
    {
        get
        {
            return swapCoolDown;
        }
        set
        {
            UIManager.Instance.swapCoolDown = value;
            swapCoolDown = value;
        }
    }

    //SkillA 쿨다운
    private float skillACoolDown;
    public float SkillACoolDown
    {
        get
        {
            return skillACoolDown;
        }
        set
        {
            UIManager.Instance.skillACoolDown = value;
            skillACoolDown = value;
        }
    }

    //SkillB 쿨다운
    private float skillBCoolDown;
    public float SkillBCoolDown
    {
        get
        {
            return skillBCoolDown;
        }
        set
        {
            UIManager.Instance.skillBCoolDown = value;
            skillBCoolDown = value;
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        playerHp = playerMaxHp;
        playerSkulList = new List<Player>();
        playerSprite = gameObject.GetComponentMust<SpriteRenderer>();
        Player possibleSkul = default;
#if !DEBUG_ENABLED
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
        //기본 스컬의 런타임애니컨트롤러를 저장 => 스컬스킬A,B사용시 런타임애니컨트롤러를 변경하는 로직
        BeforeChangeRuntimeC = player.playerAni.runtimeAnimatorController;
        isGroundRay = gameObject.GetComponentMust<PlayerGroundCheck>();
        currentHp = playerHp;
        skillACoolDown = player.skillACool;
        skillBCoolDown = player.skillBCool;
        UIManager.Instance.maxSkillACool = player.skillACool;
        UIManager.Instance.maxSkillBCool = player.skillBCool;

        IPlayerState idle = new PlayerIdle();
        IPlayerState move = new PlayerMove();
        IPlayerState jump = new PlayerJump();
        IPlayerState dash = new PlayerDash();
        IPlayerState attack = new PlayerAttack();
        IPlayerState skillA = new PlayerSkillA();
        IPlayerState skillB = new PlayerSkillB();
        IPlayerState dead = new PlayerDead();

        dicState.Add(PlayerState.IDLE, idle);
        dicState.Add(PlayerState.MOVE, move);
        dicState.Add(PlayerState.JUMP, jump);
        dicState.Add(PlayerState.DASH, dash);
        dicState.Add(PlayerState.ATTACK, attack);
        dicState.Add(PlayerState.SKILLA, skillA);
        dicState.Add(PlayerState.SKILLB, skillB);
        dicState.Add(PlayerState.DEAD, dead);

        pStateMachine = new PStateMachine(idle, this);
        UIManager.Instance.playerMaxHp = playerMaxHp;
        UIManager.Instance.mainSkul = player.skulSprite;
        UIManager.Instance.mainSkillA = player.skillASprite;
        UIManager.Instance.mainSkillB = player.skillBSprite;
    } //Start
    void FixedUpdate()
    {
        pStateMachine.DoFixedUpdate();
    } //FixedUpdate

    // Update is called once per frame
    void Update()
    {
        if (playerHp <= 0)
        {
            isDead = true;
            pStateMachine.SetState(dicState[PlayerState.DEAD]);
        }
        if (isDead == true)
        {
            return;
        }
        if (playerHp < currentHp && isHit == false)
        {
            isHit = true;
            StartCoroutine(HitPlayer());
            currentHp = playerHp;
        }

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
        if (Input.GetKeyDown(KeyCode.A) && skillACoolDown == player.skillACool)
        {
            pStateMachine.SetState(dicState[PlayerState.SKILLA]);
            if (enumState == PlayerState.SKILLA)
            {
                StartCoroutine(Co_SkillACoolDown());
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && skillBCoolDown == player.skillBCool)
        {
            pStateMachine.SetState(dicState[PlayerState.SKILLB]);
            if (enumState == PlayerState.SKILLB)
            {
                StartCoroutine(Co_SkillBCoolDown());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && swapCoolDown == 6f)
        {
            ChangePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.optionObj.SetActive(true);
            Time.timeScale = 0;
        }

        if (enumState != PlayerState.JUMP && enumState != PlayerState.ATTACK)
        {
            if (player.playerRb.velocity.y < -1)
            {
                pStateMachine.SetState(dicState[PlayerState.JUMP]);
            }
        }

        UIManager.Instance.playerHp = playerHp;

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
        player.playerAudio.clip = player.switchSound;
        player.playerAudio.Play();
        //스컬리스트의 활성화 상태를 반전시킴
        for (int i = 0; i < playerSkulList.Count; i++)
        {
            if (playerSkulList[i].enabled == false)
            {
                UIManager.Instance.mainSkul = playerSkulList[i].skulSprite;
                UIManager.Instance.mainSkillA = playerSkulList[i].skillASprite;
                UIManager.Instance.mainSkillB = playerSkulList[i].skillBSprite;
                UIManager.Instance.maxSkillACool = playerSkulList[i].skillACool;
                UIManager.Instance.maxSkillBCool = playerSkulList[i].skillBCool;
                skillACoolDown = playerSkulList[i].skillACool;
                skillBCoolDown = playerSkulList[i].skillBCool;
            }
            else
            {
                UIManager.Instance.subSkul = playerSkulList[i].skulSprite;
                UIManager.Instance.subSkillA = playerSkulList[i].skillASprite;
                UIManager.Instance.subSkillB = playerSkulList[i].skillBSprite;
            }
            playerSkulList[i].enabled = !playerSkulList[i].enabled;
        }
        StartCoroutine(Co_SwapCoolDown());
    } //ChangePlayer

    //캐릭터 Swap쿨다운 적용 코루틴 함수
    private IEnumerator Co_SwapCoolDown()
    {
        //스왑쿨다운 6초 설정
        for (int i = 0; i < 60; i++)
        {
            var tick = 0.1f;
            SwapCoolDown -= tick;
            UIManager.Instance.swapCoolDown = SwapCoolDown;
            yield return new WaitForSeconds(tick);
        }
        SwapCoolDown = 6f;
    } //SwapCoolDown

    private IEnumerator Co_SkillACoolDown()
    {
        float skillACool = player.skillACool;
        for (int i = 0; i < skillACool * 10; i++)
        {
            var tick = 0.1f;
            if (isGetSkulSkillA == true)
            {
                //Skull의 SkillA사용후 스컬헤드를 습득했을시 쿨초기화
                skillACoolDown = player.skillACool;
                isGetSkulSkillA = false;
                UIManager.Instance.skillACoolDown = skillACoolDown;
                yield break;
            }
            if (skillACool != player.skillACool)
            {
                skillACoolDown = player.skillACool;
                UIManager.Instance.skillACoolDown = skillACoolDown;
                yield break;
            }
            skillACoolDown -= tick;
            UIManager.Instance.skillACoolDown = skillACoolDown;
            yield return new WaitForSeconds(tick);
        }
        skillACoolDown = player.skillACool;
    } //Co_SkillACoolDown

    private IEnumerator Co_SkillBCoolDown()
    {
        float skillBCool = player.skillBCool;
        for (int i = 0; i < skillBCool * 10; i++)
        {
            var tick = 0.1f;
            if (skillBCool != player.skillBCool)
            {
                skillBCoolDown = player.skillBCool;
                UIManager.Instance.skillBCoolDown = skillBCoolDown;
                yield break;
            }
            skillBCoolDown -= tick;
            UIManager.Instance.skillBCoolDown = skillBCoolDown;
            yield return new WaitForSeconds(tick);
        }
        skillBCoolDown = player.skillBCool;
    } //Co_SkillBCoolDown

    //interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    } //CoroutineDeligate

    private IEnumerator HitPlayer()
    {
        Color original = playerSprite.color;
        player.tag = GData.ENEMY_LAYER_MASK;
        playerSprite.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 1f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 1f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 1f);
        playerSprite.color = original;
        player.tag = GData.PLAYER_LAYER_MASK;
        isHit = false;
    }
}
