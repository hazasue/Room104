using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoUI : MonoBehaviour
{
    public Player player;
    private PlayerStat playerStat;

    public Image healthBar;
    public TMP_Text healthText;
    public Image intelligenceBar;
    public TMP_Text intelligenceText;
    public Image stressBar;
    public TMP_Text stressText;
    public Image staminaBar;


    // Start is called before the first frame update
    void Start()
    {
        init();
        // stamina
        //Transform player = this.PlayerObj.GetComponent<Transform>();
        //this.staminaBar = player.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        UpdateHealth();
        UpdateIntelligence();
        UpdateStress();
        */
    }

    private void init()
    {
        playerStat = player.Stat;
        UpdateHealth();
        UpdateIntelligence();
        UpdateStress();
        UpdateStamina();
    }

    public void UpdateHealth()
    {
        healthBar.fillAmount = playerStat.Health / PlayerStat.MAX_HEALTH;

        healthText.text = $"{(int)playerStat.Health} / {(int)PlayerStat.MAX_HEALTH}";
    }

    /*
     * using coroutine
    public void UpdateHealth()
    {
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
        }
        healthCoroutine = StartCoroutine(AnimateHealthBar());
    }

    private IEnumerator AnimateHealthBar()
    {
        float healthRatio = (float)stat.Health / PlayerStat.MAX_HEALTH;
        float startFillAmount = healthBar.fillAmount;
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(startFillAmount, healthRatio, elapsedTime / duration);
            yield return null;
        }

        healthBar.fillAmount = healthRatio;
        healthText.text = stat.Health.ToString() + " / " + PlayerStat.MAX_HEALTH.ToString();
    }
    */

    public void UpdateIntelligence()
    {
        intelligenceBar.fillAmount = playerStat.Intelligence / PlayerStat.MAX_INTELLIGENCE;
        
        intelligenceText.text = $"{(int)playerStat.Intelligence} / {(int)PlayerStat.MAX_INTELLIGENCE}";
    }


    public void UpdateStress()
    {
        stressBar.fillAmount = playerStat.Stress / PlayerStat.MAX_STRESS;

        stressText.text = $"{(int)playerStat.Stress} / {(int)PlayerStat.MAX_STRESS}";
    }

    public void UpdateStamina()
    {
        staminaBar.fillAmount = playerStat.Stamina / playerStat.MaxStamina;
    }
}
