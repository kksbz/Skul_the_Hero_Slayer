using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillB : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.SKILLB;
        pController.player.playerAni.SetBool("isSkillB", true);
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
        pController.player.playerAni.SetBool("isSkillB", false);
        /*Do Nothing*/
    } //StateExit
}
