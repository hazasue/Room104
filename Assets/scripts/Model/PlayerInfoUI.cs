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
        // ������ �±װ� "Player"�� ������Ʈ ��������
        PlayerObj = GameObject.FindWithTag("Player");
        Stat = PlayerObj.GetComponent<PlayerStat>();

        // �θ�-�ڽ� ���� �߰� ��� �ʿ�
        // ���׹̳� UI
        Image healthBar = transform.GetChild(5).GetComponent<Image>();
        Text healthText = transform.GetChild(6).GetComponent<Text>();

        // ���� UI
        Image intelligenceBar = transform.GetChild(9).GetComponent<Image>();
        Text intelligencetext = transform.GetChild(10).GetComponent<Text>();

        // ��Ʈ���� UI
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
        float healthRatio = (float)Stat.Stamina / PlayerStat.MAX_HEALTH; // ���� ä�� ���� ��� (0.0 ~ 1.0)

        // ä�� �� UI ������Ʈ
        healthBar.fillAmount = healthRatio;

        // �ؽ�Ʈ�� ���� ä�� ���� ǥ��
        healthText.text = Stat.Stamina.ToString() + " / " + PlayerStat.MAX_HEALTH.ToString();
    }

    public void UpdateIntelligence()
    {
        float IntelligenceRatio = (float)Stat.Intelligence / PlayerStat.MAX_INTELLIGENCE; // ���� ä�� ���� ��� (0.0 ~ 1.0)

        // ä�� �� UI ������Ʈ
        intelligenceBar.fillAmount = IntelligenceRatio;

        // �ؽ�Ʈ�� ���� ä�� ���� ǥ��
        intelligenceText.text = Stat.Intelligence.ToString() + " / " + PlayerStat.MAX_INTELLIGENCE.ToString();
    }


    public void UpdateStress()
    {
        float StressRatio = (float)Stat.Stress / PlayerStat.MAX_STRESS; // ���� ä�� ���� ��� (0.0 ~ 1.0)

        // ä�� �� UI ������Ʈ
        StressBar.fillAmount = StressRatio;

        // �ؽ�Ʈ�� ���� ä�� ���� ǥ��
        StressText.text = Stat.Stress.ToString() + " / " + PlayerStat.MAX_STRESS.ToString();
    }
}
