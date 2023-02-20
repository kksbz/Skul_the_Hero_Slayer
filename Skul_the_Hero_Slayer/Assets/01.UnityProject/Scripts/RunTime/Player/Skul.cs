using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponentMust<PlayerController>();
        playerData = Resources.Load("SkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
