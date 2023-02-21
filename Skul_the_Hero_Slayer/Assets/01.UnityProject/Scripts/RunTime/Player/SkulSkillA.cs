using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulSkillA : MonoBehaviour
{
    public Skul parentObj;
    private float speed = 7f;
    private float range = 15f;
    public float direction;
    private float originalGravity;
    private bool isHit = false;
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
        //Hit상태가되면 CircleCast 발동 못하게 리턴
        string tagetObj;
        if (isHit == true)
        {
            tagetObj = GData.PLAYER_LAYER_MASK;
        }
        else
        {
            tagetObj = GData.ENEMY_LAYER_MASK;
        }
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask(tagetObj));
        if (hit.collider != null && isHit == false)
        {
            MonsterController target = hit.collider.gameObject?.GetComponentMust<MonsterController>();
            target.monster.hp -= Random.RandomRange(20, 25);
            Debug.Log($"스킬A공격 = {target.monster.hp}/{target.monster.maxHp}");
            isHit = true;
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
        speed = 0;
        skillA_Rb.gravityScale = originalGravity;
        skillA_Ani.StartPlayback();
        Destroy(gameObject, 4f);
        yield return new WaitForSeconds(3.5f);
        Debug.Log("해골 파괴됨");
    } //DestroySkul
}
