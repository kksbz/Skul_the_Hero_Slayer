using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class PlayerSaveInfo
{
    public int          playerHp;
    public int          playerMaxHp;
    public List<int>    playerSkul;
    public int          enabledSkul;
    public int          minDamage;
    public int          maxDamage;
    public float        speed;
    public float        groundCheckLength;

    public PlayerSaveInfo(PlayerController controller)
    {
        playerSkul      = new List<int>();
        playerHp        = controller.playerHp;
        playerMaxHp     = controller.playerMaxHp;

        controller.playerSkulList.ForEach(skul=>
        {
            playerSkul.Add(skul.skulIndex);
            if(skul.enabled)
            {
                enabledSkul = skul.skulIndex;
            }            
        });

        minDamage               = controller.player.minDamage;
        maxDamage               = controller.player.maxDamage;
        speed                   = controller.player.moveSpeed;
        groundCheckLength       = controller.player.groundCheckLength;
    }
}

public class SaveManager : MonoBehaviour
{
    string dataPath = default;

    string dataPathSceneChange = default;

    private static  SaveManager instance = null;
    public static   SaveManager Instance
    {
        get
        {
            if (instance == null || instance == default)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
#if UNITY_EDITOR
            dataPath = Application.dataPath + "/save/";
#else
            dataPath = Application.persistentDataPath + "/save/";
#endif
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SaveData(PlayerController playerController)
    {
        PlayerSaveInfo playerInfo = new PlayerSaveInfo(playerController);

        var jsonObj = JsonUtility.ToJson(playerInfo);

        Debug.Log($"SAVEDATA: {jsonObj}");

        if(!Directory.Exists(dataPath))
        {
        	Directory.CreateDirectory(dataPath);
        }

        var path = dataPath + System.DateTime.Now.ToString("MMddHHmmss") + ".json";
        Debug.Log(path);

        File.WriteAllText(path, jsonObj);
        dataPathSceneChange = path;

    }
    public void LoadData(PlayerController playerController)
    {
        var saveInfoJson = File.ReadAllText(dataPathSceneChange);
        Debug.Log($"LOADDATA: {saveInfoJson}");
        var saveInfo = JsonUtility.FromJson<PlayerSaveInfo>(saveInfoJson);

        playerController.playerHp = saveInfo.playerHp;
        playerController.playerMaxHp = saveInfo.playerMaxHp;

        saveInfo.playerSkul.ForEach(skul=>
        {
            if(skul == 0)
            {
                var comp = playerController.gameObject.AddComponent<Skul>();
                comp.enabled = false;
                playerController.playerSkulList.Add(comp);
            }
            else if (skul == 1)
            {
                var comp = playerController.gameObject.AddComponent<EntSkul>();
                comp.enabled = false;
                playerController.playerSkulList.Add(comp);
            }
        }); 
    
        playerController.playerSkulList.ForEach(skul=>
        {
            if(skul.skulIndex == saveInfo.enabledSkul)
            {
                skul.enabled = true;
            }
        });
        
        playerController.player.minDamage                = saveInfo.minDamage;
        playerController.player.maxDamage                = saveInfo.maxDamage;
        playerController.player.moveSpeed                = saveInfo.speed;
        playerController.player.groundCheckLength        = saveInfo.groundCheckLength;
    }
}