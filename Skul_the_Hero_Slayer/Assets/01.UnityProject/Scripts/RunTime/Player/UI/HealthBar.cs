using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    Image healthBar;
    TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.FindChildObj("Player_HealthBar").GetComponentMust<Image>();
        healthText = gameObject.FindChildObj("Player_Health").GetComponentMust<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealthBar();
    }

    private void PlayerHealthBar()
    {
        healthBar.fillAmount = (float)UIManager.Instance.playerHp / (float)UIManager.Instance.playerMaxHp;
        healthText.text = $"{UIManager.Instance.playerHp}/{UIManager.Instance.playerMaxHp}";
    }
}
