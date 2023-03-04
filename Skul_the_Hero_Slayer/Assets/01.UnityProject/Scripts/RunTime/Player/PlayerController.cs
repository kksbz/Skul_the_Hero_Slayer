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
    public PlayerGroundCheck isGroundRay; //땅 체크 레이어
    public PlayerState enumState = PlayerState.IDLE; //기본상태 Idle
    private PStateMachine _pStateMachine; //상태처리 머신
    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dicState = new Dictionary<PlayerState, IPlayerState>(); //각상태를 담을 딕셔너리
    public RuntimeAnimatorController BeforeChangeRuntimeC; //Skul SkillA사용시 머리사라진 모습 런타임애니메이션컨트롤러
    public List<Player> playerSkulList; //플레이어가 사용할 수 있는 Skul의 List
    private SpriteRenderer playerSprite;
    public Player player; //컨트롤러에 가져올 플레이어블 스컬 데이터
    public int playerHp;
    public int playerMaxHp = 100;
    public int currentHp;
    public bool canDash = true; //대쉬 사용가능 체크
    public bool isGetSkulSkillA = false;
    public bool isHit = false;
    private bool isDead = false;

    //스왑스킬 쿨다운
    private float swapCoolDown = 6f;
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
        //현재씬이 마왕성 로비일 경우 플레이어 초기화
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == GData.CASTLELOBBY_SCENE_NAME)
        {
            possibleSkul = gameObject.AddComponent<Skul>();
            playerSkulList.Add(possibleSkul);
        }
        else
        {
            //현재씬이 마왕성 로비가 아닐 경우 세이브데이터 로드
            SaveManager.Instance.LoadData(this);
        }
#else
        possibleSkul = gameObject.AddComponent<Skul>();
        playerSkulList.Add(possibleSkul);
#endif
        //기본 스컬의 런타임애니컨트롤러를 저장 => 스컬스킬A,B사용시 런타임애니컨트롤러를 변경하는 로직
        BeforeChangeRuntimeC = Resources.Load("Animation/PlayerAni/Skul") as RuntimeAnimatorController;
        // Debug.Log($"스컬기본런타임 {BeforeChangeRuntimeC.name}");

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
        StateSelect();
        pStateMachine.DoUpdate();
    } //Update

    //플레이어의 State를 정하는 함수
    private void StateSelect()
    {
        //플레이어가 죽은 상태면 리턴
        if (isDead == true)
        {
            return;
        }

        //플레이어 Hp가 <= 0이면 Dead
        if (playerHp <= 0)
        {
            isDead = true;
            currentHp = playerHp;
            pStateMachine.SetState(dicState[PlayerState.DEAD]);
        }

        //플레이어가 피격당하면 피격처리
        if (playerHp < currentHp && isHit == false)
        {
            StartCoroutine(HitPlayer());
            currentHp = playerHp;
        }

        //Move상태 조건 체크
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        && isGroundRay.hit.collider != null
        && enumState != PlayerState.DASH
        && (enumState == PlayerState.IDLE || (enumState != PlayerState.MOVE && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
        {
            pStateMachine.SetState(dicState[PlayerState.MOVE]);
        }

        //Idle상태 조건 체크
        if (isGroundRay.hit.collider != null
        && (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false)
        && enumState != PlayerState.DASH
        && (enumState == PlayerState.MOVE || (enumState != PlayerState.IDLE && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
        {
            pStateMachine.SetState(dicState[PlayerState.IDLE]);
        }

        //점프 시작
        if (Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.DownArrow))
        {
            pStateMachine.SetState(dicState[PlayerState.JUMP]);
        }

        //대쉬 시작
        if (Input.GetKeyDown(KeyCode.Z) && canDash == true)
        {
            pStateMachine.SetState(dicState[PlayerState.DASH]);
        }

        //공격 시작
        if (Input.GetKeyDown(KeyCode.X) && enumState != PlayerState.DASH)
        {
            pStateMachine.SetState(dicState[PlayerState.ATTACK]);
        }

        //스킬A 사용
        if (Input.GetKeyDown(KeyCode.A)
        && skillACoolDown == player.skillACool
        && enumState != PlayerState.DASH)
        {
            pStateMachine.SetState(dicState[PlayerState.SKILLA]);
            StartCoroutine(Co_SkillACoolDown());
        }

        //스킬B 사용
        if (Input.GetKeyDown(KeyCode.S)
        && skillBCoolDown == player.skillBCool
        && enumState != PlayerState.DASH)
        {
            if (player.playerAni.runtimeAnimatorController.name == "Skul")
            {
                // Debug.Log($"스컬상태에선 B스킬사용못함 헤드리스만 B스킬가능
                // {player.playerAni.runtimeAnimatorController.name}");
                return;
            }
            pStateMachine.SetState(dicState[PlayerState.SKILLB]);
            StartCoroutine(Co_SkillBCoolDown());
        }

        //스왑스킬 사용
        if (Input.GetKeyDown(KeyCode.Space)
        && swapCoolDown == 6f
        && enumState != PlayerState.DASH)
        {
            ChangePlayer();
        }

        //옵션창 열기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.optionObj.SetActive(true);
            Time.timeScale = 0;
        }

        //Jump상태가 아닐때 Velocity.y값이 -1보다 작으면 낙하시작
        if (player.playerRb.velocity.y < -1
        && ((enumState == PlayerState.IDLE || enumState == PlayerState.MOVE)
        || (enumState != PlayerState.JUMP && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
        {
            pStateMachine.SetState(dicState[PlayerState.JUMP]);
        }

        UIManager.Instance.playerHp = playerHp;
    } //StateSelect

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
                //UI에 보여질 이미지와 스킬쿨 저장
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
                //UI에 보여질 서브이미지 저장
                UIManager.Instance.subSkul = playerSkulList[i].skulSprite;
                UIManager.Instance.subSkillA = playerSkulList[i].skillASprite;
                UIManager.Instance.subSkillB = playerSkulList[i].skillBSprite;
            }
            playerSkulList[i].enabled = !playerSkulList[i].enabled;
        }
        pStateMachine.SetState(dicState[PlayerState.IDLE]);
        StartCoroutine(Co_SwapCoolDown());
    } //ChangePlayer

    //캐릭터 Swap쿨다운 코루틴 함수
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

    //스킬A 쿨다운 코루틴함수
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

    //스킬B 쿨다운 코루틴함수
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

    //몬스터에게 피격당할 시 실행하는 코루틴함수
    private IEnumerator HitPlayer()
    {
        //플레이어의 Hit상태를 bool값으로 체크해 무적상태 구현
        isHit = true;
        //스프라이트 컬러의 알파값을 바꿔 깜빡거리게 구현
        Color original = playerSprite.color;
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
        isHit = false;
    } //HitPlayer
}
