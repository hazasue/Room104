using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    
    public GameObject PlayerObj;
    public PlayerStat Stat;

    public Image healthBar;
    public Text healthText;
    public Image intelligenceBar;
    public Text intelligenceText;
    public Image StressBar;
    public Text StressText;


    // Start is called before the first frame update
    void Start()
    {
        // 씬에서 태그가 "Player"인 오브젝트 가져오기
        PlayerObj = GameObject.FindWithTag("Player");
        Stat = PlayerObj.GetComponent<PlayerStat>();

        // 부모-자식 관계 추가 고려 필요
        // 스테미나 UI
        Image healthBar = transform.GetChild(5).GetComponent<Image>();
        Text healthText = transform.GetChild(6).GetComponent<Text>();

        // 지능 UI
        Image intelligenceBar = transform.GetChild(9).GetComponent<Image>();
        Text intelligencetext = transform.GetChild(10).GetComponent<Text>();

        // 스트레스 UI
        Image StressBar = transform.GetChild(13).GetComponent<Image>();
        Text StressText = transform.GetChild(14).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateIntelligence();
        UpdateStress();
    }

    public void UpdateHealth()
    {
        float healthRatio = (float)Stat.Stamina / PlayerStat.MAX_HEALTH; // 현재 채력 비율 계산 (0.0 ~ 1.0)

        // 채력 바 UI 업데이트
        healthBar.fillAmount = healthRatio;

        // 텍스트로 현재 채력 값을 표시
        healthText.text = Stat.Stamina.ToString() + " / " + PlayerStat.MAX_HEALTH.ToString();
    }

    public void UpdateIntelligence()
    {
        float IntelligenceRatio = (float)Stat.Intelligence / PlayerStat.MAX_INTELLIGENCE; // 현재 채력 비율 계산 (0.0 ~ 1.0)

        // 채력 바 UI 업데이트
        intelligenceBar.fillAmount = IntelligenceRatio;

        // 텍스트로 현재 채력 값을 표시
        intelligenceText.text = Stat.Intelligence.ToString() + " / " + PlayerStat.MAX_INTELLIGENCE.ToString();
    }


    public void UpdateStress()
    {
        float StressRatio = (float)Stat.Stress / PlayerStat.MAX_STRESS; // 현재 채력 비율 계산 (0.0 ~ 1.0)

        // 채력 바 UI 업데이트
        StressBar.fillAmount = StressRatio;

        // 텍스트로 현재 채력 값을 표시
        StressText.text = Stat.Stress.ToString() + " / " + PlayerStat.MAX_STRESS.ToString();
    }
}
