using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoUI : MonoBehaviour
{
    private Player player;

    public Image healthBar;
    public TMP_Text healthText;
    public Image intelligenceBar;
    public TMP_Text intelligenceText;
    public Image stressBar;
    public TMP_Text stressText;
    public Image staminaBar;

    public void SetPlayerHandler(Player player)
    {
        this.player = player;
        player.StatEventHandler += UpdateHealth;
        player.StatEventHandler += UpdateIntelligence;
        player.StatEventHandler += UpdateStress;
        player.StatEventHandler += UpdateStamina;
    }

    private void UpdateHealth()
    {
        healthBar.fillAmount = player.Stat.Health / PlayerStat.MAX_HEALTH;

        healthText.text = $"{(int)player.Stat.Health} / {(int)PlayerStat.MAX_HEALTH}";
    }

    private void UpdateIntelligence()
    {
        intelligenceBar.fillAmount = player.Stat.Intelligence / PlayerStat.MAX_INTELLIGENCE;
        
        intelligenceText.text = $"{(int)player.Stat.Intelligence} / {(int)PlayerStat.MAX_INTELLIGENCE}";
    }


    private void UpdateStress()
    {
        stressBar.fillAmount = player.Stat.Stress / PlayerStat.MAX_STRESS;

        stressText.text = $"{(int)player.Stat.Stress} / {(int)PlayerStat.MAX_STRESS}";
    }

    private void UpdateStamina()
    {
        staminaBar.fillAmount = player.Stat.Stamina / player.Stat.MaxStamina;
    }
}
