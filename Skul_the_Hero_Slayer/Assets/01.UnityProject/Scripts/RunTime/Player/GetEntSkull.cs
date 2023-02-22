using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEntSkull : MonoBehaviour
{
    private GameObject enterKeyIcon; //자식오브젝트 변수
    private bool isPushKey = false; //플레이어 키입력 받는 조건 변수
    private PlayerController player; //트리거에 감지된 플레이어 담을 변수

    // Start is called before the first frame update
    void Start()
    {
        enterKeyIcon = gameObject.FindChildObj("EnterKeyIcon");
    } //Start

    // Update is called once per frame
    void Update()
    {
        if (isPushKey == true)
        {
            //F키 입력시 EntSkul획득 조건 true
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetEntSkul(player);
                gameObject.SetActive(false);
            }
        }
    } //Update

    //플레이어가 EntSkul을 획득하는 함수
    private void GetEntSkul(PlayerController _player)
    {
        if (_player != null || _player != default)
        {
            //플레이어가 EntSkul을 가지고있으면 리턴 예외처리
            if (_player.GetComponent<EntSkul>() == true)
            {
                return;
            }
            //플레이어가 EntSkul을 획득하는 로직(컴포넌트로 스크립트를 붙임)
            //현재 활성화되어있는 Skul스크립트를 비활성화시킴
            _player.gameObject.GetComponent<Skul>().enabled = !(_player.gameObject.GetComponent<Skul>().enabled);
            if (GetComponent<EntSkul>() == null)
            {
                //EntSkul을 가지고 있지 않으면 AddComponent
                _player.gameObject.GetComponentMust<PlayerController>().playerSkulList.Add(_player.gameObject.AddComponent<EntSkul>());
                return;
            }
            //EntSkul스크립트를 활성화시킴
            _player.gameObject.GetComponent<EntSkul>().enabled = !(_player.gameObject.GetComponent<Skul>().enabled);
        }
    } //GetEntSkul

    void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어가 범위에 들어오면 F키 아이콘 활성화 및 버튼입력 가능하게 isPushKey = true
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            enterKeyIcon.SetActive(true);
            isPushKey = true;
        }
    } //OnTriggerEnter2D

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            player = other.gameObject.GetComponent<PlayerController>();
        }
    } //OnTriggerStay2D

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
