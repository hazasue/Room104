 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStat : MonoBehaviour
{
    /* 
     * ���߿� scv���Ͽ��� ��� �;� �� ���� ����
     * CSVReader.cs����
     */


    // �÷��̾� �⺻ ����
    private int S_health = 500;
    private int S_stress = 700;
    private int S_stamina = 1000;
    private int S_int = 500;
    private int S_speed = 1000;
    private int S_money = 0;
    private int S_safe = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getHealth()
    {
        int health = this.S_health;
        return health;
    }

    public int getStress()
    {
        int stress = this.S_stress;
        return stress;
    }

    public int getstamina()
    {
        int stamina = this.S_stamina;
        return stamina;
    }

    public int getInt()
    {
        int Int = this.S_int;
        return Int;
    }

    public int getmoney()
    {
        int money = this.S_money;
        return money;
    }

    public int getsafe()
    {
        int safe = this.S_safe;
        return safe;
    }

    // �÷��̾� ���� ���� �Լ�
    public void healthIncrease(int health = 50)
    {
        this.S_health += health;
    }

    public void stressIncrease(int stress = 50)
    {
        this.S_stress += stress;
    }

    public void staminaIncrease(int stamina = 50)
    {
        this.S_stamina += stamina;
    }

    public void intIncrease(int Int = 25)
    {
        this.S_int += Int;
    }

    public void speedIncrease(int speed = 25)
    {
        this.S_speed += speed;
    }

    public void moneyIncrease(int money = 1)
    {
        this.S_money += money;
    }

    public void safeIncrease(int safe = 1)
    {
        this.S_safe += safe;
    }

    // ���� ���� �Լ�
    public void healthDecrease(int health = 200)
    {
        this.S_health -= health;
    }

    public void stressDecrease(int stress = 50)
    {
        this.S_stress -= stress;
    }

    public void moneyDecrease(int money = 1)
    {
        this.S_money -= money;
    }
}
