using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    
    private GameObject PlayerObj;
    private PlayerStat Stat;

    private Image healthBar;
    private Text healthText;
    private Image intelligenceBar;
    private Text intelligenceText;
    private Image stressBar;
    private Text stressText;
    private Image staminaBar;


    // Start is called before the first frame update
    void Start()
    {
        this.PlayerObj = GameObject.FindWithTag("Player");
        this.Stat = PlayerObj.GetComponent<PlayerStat>();

        // �θ�-�ڽ� ���� �߰� ���� �ʿ�
        this.healthBar = transform.GetChild(5).GetComponent<Image>();
        this.healthText = transform.GetChild(6).GetComponent<Text>();

        this.intelligenceBar = transform.GetChild(9).GetComponent<Image>();
        this.intelligenceText = transform.GetChild(10).GetComponent<Text>();

        this.stressBar = transform.GetChild(13).GetComponent<Image>();
        this.stressText = transform.GetChild(14).GetComponent<Text>();

        // stamina
        Transform player = this.PlayerObj.GetComponent<Transform>();
        this.staminaBar = player.GetChild(0).GetComponent<Image>();
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

    public void UpdateHealth()
    {
        float healthRatio = (float)Stat.Stamina / PlayerStat.MAX_HEALTH;

        this.healthBar.fillAmount = healthRatio;

        this.healthText.text = Stat.Stamina.ToString() + " / " + PlayerStat.MAX_HEALTH.ToString();
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
        float IntelligenceRatio = (float)Stat.Intelligence / PlayerStat.MAX_INTELLIGENCE;

        this.intelligenceBar.fillAmount = IntelligenceRatio;

        this.intelligenceText.text = Stat.Intelligence.ToString() + " / " + PlayerStat.MAX_INTELLIGENCE.ToString();
    }


    public void UpdateStress()
    {
        float StressRatio = (float)Stat.Stress / PlayerStat.MAX_STRESS;

        this.stressBar.fillAmount = StressRatio;

        this.stressText.text = Stat.Stress.ToString() + " / " + PlayerStat.MAX_STRESS.ToString();
    }

    public void UpdateStamina()
    {
        float StaminaRatio = (float)Stat.Stamina / (float)Stat.MaxStamina;

        this.staminaBar.fillAmount = StaminaRatio;
    }
}
