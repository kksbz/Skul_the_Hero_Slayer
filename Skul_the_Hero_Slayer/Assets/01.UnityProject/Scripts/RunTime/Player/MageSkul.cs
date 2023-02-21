using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkul : Player
{
    
    public PlayerData playerData;
    private PlayerController playerController;
    private GameObject skillAObj;
    void OnEnable()
    {
        playerController = gameObject.GetComponentMust<PlayerController>();
        playerData = Resources.Load("EntSkul") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
        Debug.Log("MageSkul");
    }
    void Start()
    {

    }
    public override void AttackA()
    {

    } //AttackA

    public override void AttackB()
    {

    } //AttackB

    public override void SkillA()
    {

    } //SkillA

    public override void SkillB()
    {

    } //SkillB
}
