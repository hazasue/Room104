 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    /* 
     * ���߿� scv���Ͽ��� ��� �;� �� ���� ����
     * CSVReader.cs����
     */


    // �÷��̾� �⺻ ����
    private int health = 500;
    private int stress = 700;
    private int stamina = 1000;
    private int intelligence = 500;
    private int speed = 1000;
    private int money = 0;
    private int safe = 0;

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
        int health = this.health;
        return health;
    }

    public int getStress()
    {
        int stress = this.stress;
        return stress;
    }

    public int getstamina()
    {
        int stamina = this.stamina;
        return stamina;
    }

    public int getintelligence()
    {
        int Int = this.intelligence;
        return Int;
    }

    public int getmoney()
    {
        int money = this.money;
        return money;
    }

    public int getsafe()
    {
        int safe = this.safe;
        return safe;
    }

    // �÷��̾� ���� ���� �Լ�
    public void healthIncrease(int health = 50)
    {
        this.health += health;
    }

    public void stressIncrease(int stress = 50)
    {
        this.stress += stress;
    }

    public void staminaIncrease(int stamina = 50)
    {
        this.stamina += stamina;
    }

    public void intelligenceIncrease(int Int = 25)
    {
        this.intelligence += Int;
    }

    public void speedIncrease(int speed = 25)
    {
        this.speed += speed;
    }

    public void moneyIncrease(int money = 1)
    {
        this.money += money;
    }

    public void safeIncrease(int safe = 1)
    {
        this.safe += safe;
    }

    // ���� ���� �Լ�
    public void healthDecrease(int health = 200)
    {
        this.health -= health;
    }

    public void stressDecrease(int stress = 50)
    {
        this.stress -= stress;
    }

    public void moneyDecrease(int money = 1)
    {
        this.money -= money;
    }
}
