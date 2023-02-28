using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DEAD;
        pController.player.playerAudio.clip = pController.player.deadSound;
        pController.player.playerAudio.Play();
        GameObject deadEffect = GameObject.Instantiate(Resources.Load("Prefabs/Effect/EnemyDead") as GameObject);
        deadEffect.transform.position = pController.player.transform.position;
        deadEffect.SetActive(true);
        pController.gameObject.SetActive(false);
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate
    public void StateUpdate()
    {
        /*Do Nothing*/
    } //StateUpdate
    public void StateExit()
    {
        /*Do Nothing*/
    } //StateExit
}
