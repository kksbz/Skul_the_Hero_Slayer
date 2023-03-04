using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEnter : MonoBehaviour
{
    private GameObject enterKeyIcon; //자식오브젝트 변수
    private PlayerController player;
    public string nextStageName; //이동할 Scene 변수
    private bool isPushKey; //플레이어 키입력 받는 조건 변수
    // Start is called before the first frame update
    void Start()
    {
        enterKeyIcon = gameObject.FindChildObj("EnterKeyIcon");
    } //Start

    void Update()
    {
        if (isPushKey == true && GameManager.Instance.monsterRemainingNumber <= 0)
        {
            //스테이지에 남은 몬스터가 없을 때 F키 입력시 다음 씬으로 이동
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioManager.Instance.bgAudio.Stop();
                SaveManager.Instance.SaveData(player);
                SceneMgr.Instance.LoadAsyncScene(nextStageName);
            }
        }
    } //Update
    void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어가 범위에 들어오면 F키 아이콘 활성화 및 버튼입력 가능하게 isPushKey = true
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            player = other.gameObject.GetComponentMust<PlayerController>();
            enterKeyIcon.SetActive(true);
            isPushKey = true;
        }
    } //OnTriggerEnter2D

    void OnTriggerExit2D(Collider2D other)
    {
        //플레이어가 범위에서 벗어나면 F키 아이콘 비활성 및 버튼입력 불가능하게 isPushKey = false
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            enterKeyIcon.SetActive(false);
            isPushKey = false;
        }
    } //OnTriggerExit2D
}
