using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public RaycastHit2D hit; //Ground 체크 변수
    private PlayerController playerController;
    private float rayerLength;

    void Start()
    {
        playerController = gameObject.GetComponentMust<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        rayerLength = playerController.player.groundCheckLength;
        //플레이어의 위치가 Ground에 있는지 공중에 있는지 확인하는 BoxCast
        Debug.Log($"체크레이어길이 {rayerLength}");
        hit = Physics2D.BoxCast(transform.position, new Vector2(1f, 0.2f),
         0f, Vector2.down, rayerLength, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    } //Update
}
