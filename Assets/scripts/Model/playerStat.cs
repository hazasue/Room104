 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    /* 
     * ���߿� scv���Ͽ��� ��� �;� �� ���� ����
     * CSVReader.cs����
     */

    // �÷��̾� �⺻ ����
    private int health = 500;
    public int Health { get { return health; } }
    private int stress = 700;
    public int Stress { get { return stress; } }
    private int stamina = 1000;
    public int Stamina { get { return stamina; } }
    private int intelligence = 500;
    public int Intelligence { get { return intelligence; } }
    private int speed = 1000;
    public int Speed { get { return speed; } }
    private int money = 0;
    public int Money { get { return money; } }
    private int safe = 0;
    public int Safe { get { return safe; } }

    // �÷��̾� ���� ���� �Լ�
    public void HealthIncrease(int health = 50)
    {
        this.health += health;
    }

    public void StressIncrease(int stress = 50)
    {
        this.stress += stress;
    }

    public void StaminaIncrease(int stamina = 50)
    {
        this.stamina += stamina;
    }

    public void IntIncrease(int Int = 25)
    {
        this.intelligence += Int;
    }

    public void SpeedIncrease(int speed = 25)
    {
        this.speed += speed;
    }

    public void MoneyIncrease(int money = 1)
    {
        this.money += money;
    }

    public void SafeIncrease(int safe = 1)
    {
        this.safe += safe;
    }

    // ���� ���� �Լ�
    public void HealthDecrease(int health = 200)
    {
        this.health -= health;
    }

    public void StressDecrease(int stress = 50)
    {
        this.stress -= stress;
    }

    public void MoneyDecrease(int money = 1)
    {
        this.money -= money;
    }
}
