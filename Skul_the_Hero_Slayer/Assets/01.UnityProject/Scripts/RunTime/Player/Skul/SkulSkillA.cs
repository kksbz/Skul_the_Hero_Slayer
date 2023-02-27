using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulSkillA : MonoBehaviour
{
    public Skul parentObj; //부모오브젝트를 찾기 위한 변수
    private float speed = 8f; //날아가는 속도
    private float range = 15f; //최대 사거리
    public float direction; //날아가는 방향 정할 변수
    private float originalGravity; //날아가는 도중에는 중력 영향X하기 위한 변수
    private bool isHit = false; //적에게 맞았는지 체크하는 변수
    private Vector3 startVector;
    private Rigidbody2D skillA_Rb;
    private Animator skillA_Ani;
    // Start is called before the first frame update
    void Start()
    {
        skillA_Rb = gameObject.GetComponentMust<Rigidbody2D>();
        skillA_Ani = gameObject.GetComponentMust<Animator>();
        //중력조절 히트하거나 사거리까지 이동했을때 중력적용할 것임
        originalGravity = skillA_Rb.gravityScale;
        skillA_Rb.gravityScale = 0f;
    } //Start

    // Update is called once per frame
    void Update()
    {
        OverRange();
        DetectTaget();
        gameObject.transform.Translate(new Vector2(direction, 0).normalized * speed * Time.deltaTime);
    } //Update

    //날아가는 도중 타겟을 감지하면 Hit처리 하는 함수
    private void DetectTaget()
    {
        //Hit상태가되면 LayerMask대상을 플레이어로 전환해 플레이어가 스컬헤드를 습득할 수 있도록 처리함
        string tagetObj;
        if (isHit == true)
        {
            tagetObj = GData.PLAYER_LAYER_MASK;
        }
        else
        {
            tagetObj = GData.ENEMY_LAYER_MASK;
        }
        //해골이 땅에 닿았을경우 처리
        RaycastHit2D hitGround = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
        if (hitGround.collider != null)
        {
            isHit = true;
            StartCoroutine(DestroySkul());
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask(tagetObj));
        if (hit.collider != null)
        {
            if (tagetObj == GData.ENEMY_LAYER_MASK)
            {
                BossHead boss = hit.collider.gameObject?.GetComponentMust<BossHead>();
                if (boss != null)
                {
                    boss.hp -= Random.RandomRange(20, 25);
                    Debug.Log($"스킬A공격 = {boss.hp}/{boss.maxHp}");
                    isHit = true;
                }
                MonsterController target = hit.collider.gameObject?.GetComponentMust<MonsterController>();
                if (target != null)
                {
                    target.monster.hp -= Random.RandomRange(20, 25);
                    Debug.Log($"스킬A공격 = {target.monster.hp}/{target.monster.maxHp}");
                    isHit = true;
                }
                GameObject hitEffect = Instantiate(Resources.Load("Prefabs/Effect/HitEffect") as GameObject);
                hitEffect.transform.position = hit.collider.transform.position;
            }
            if (tagetObj == GData.PLAYER_LAYER_MASK)
            {
                PlayerController playerController = hit.collider.gameObject?.GetComponentMust<PlayerController>();
                if (playerController.player._name == "Skul")
                {
                    playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
                    //머리 습득시 SkillA 쿨 초기화
                    playerController.isGetSkulSkillA = true;
                }
                Destroy(gameObject);
                return;
            }
            StartCoroutine(DestroySkul());
        }
    } //DetectTaget

    //사거리를 벗어나면 멈추고 중력영향 받게하는 함수
    private void OverRange()
    {
        float distance = Vector3.Distance(startVector, transform.position);
        if (distance >= range)
        {
            isHit = true;
            StartCoroutine(DestroySkul());
        }
    } //OverRange

    //자신의 부모를 입력받는 함수
    public void Init(Skul parent)
    {
        parentObj = parent;
        startVector = parentObj.gameObject.GetComponentMust<PlayerController>().transform.position;
        gameObject.transform.position = startVector;
        direction = parentObj.gameObject.GetComponentMust<PlayerController>().transform.localScale.x;
    } //Init

    //Hit되거나 최대 사거리에 도달했을 경우 4초뒤에 파괴하는 코루틴 함수
    private IEnumerator DestroySkul()
    {
        if (gameObject == null)
        {
            yield break;
        }
        speed = 0;
        skillA_Rb.gravityScale = originalGravity;
        skillA_Ani.StartPlayback();
        yield return new WaitForSeconds(4f);
        parentObj.onHeadBack?.Invoke();
        Destroy(gameObject);
    } //DestroySkul
}
