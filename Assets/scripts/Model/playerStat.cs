using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{

    public const int MAX_HEALTH = 1000;
    public const int MAX_STRESS = 1000;
    public const int MAX_STAMINA = 2000;
    public const int MAX_INTELLIGENCE = 1000;
    public const int MAX_SPEED = 1500;
    public const int MAX_MONEY = 9999999;
    public const int MAX_SAFE = 3;

    private int health = 500;
    public int Health { get { return health; } }
    private int stress = 700;
    public int Stress { get { return stress; } }
    private int stamina = 1000;
    public int Stamina { get { return stamina; } }
    private int maxStamina = 1000;
    public int MaxStamina { get { return maxStamina; } }
    private int intelligence = 500;
    public int Intelligence { get { return intelligence; } }
    private int speed = 1000;
    public int Speed { get { return speed; } }
    private int money = 0;
    public int Money { get { return money; } }
    private int safe = 0;
    public int Safe { get { return safe; } }

    public void HealthIncrease(int health = 50)
    {
        this.health += health;
        if (this.health >= MAX_HEALTH) this.health = MAX_HEALTH;
    }

    public void StressIncrease(int stress = 50)
    {
        this.stress += stress;
        if (this.stress >= MAX_STAMINA) this.stress = MAX_STRESS;
    }

    public void MaxStaminaIncrease(int maxstamina = 1000)
    {
        this.maxStamina += maxstamina;
        if (this.maxStamina >= MAX_STAMINA) this.maxStamina = MAX_STAMINA;
    }

    public void StaminaIncrease(int stamina = 50)
    {
        this.stamina += stamina;
        if (this.stamina >= this.maxStamina) this.stamina = this.maxStamina;
    }

    public void IntelligenceIncrease(int Int = 25)
    {
        this.intelligence += Int;
        if (this.intelligence >= MAX_INTELLIGENCE) this.intelligence = MAX_INTELLIGENCE;
    }

    public void SpeedIncrease(int speed = 25)
    {
        this.speed += speed;
        if (this.Speed >= MAX_SPEED) this.speed = MAX_SPEED;
    }

    public void MoneyIncrease(int money = 1)
    {
        this.money += money;
        if(this.money >= MAX_MONEY) this.money = MAX_MONEY;
    }

    public void SafeIncrease(int safe = 1)
    {
        this.safe += safe;
        if (this.safe >= MAX_SAFE)  this.safe = MAX_SAFE;
    }

    public void HealthDecrease(int health = 200)
    {
        this.health -= health;
        if(this.health <= 0) this.health = 0;
    }

    public void IntelligenceDecrease(int intelligence = 50)
    {
        this.intelligence -= intelligence;
        if (this.intelligence <= 0) this.intelligence = 0;
    }

    public void StressDecrease(int stress = 50)
    {
        this.stress -= stress;
        if (this.stress <= 0) this.stress = 0;
    }

    public void StaminaDecrease(int stamina = 1)
    {
        this.stamina -= stamina;
        if( this.stamina <= 0) this.stamina = 0;
    }

    public void MoneyDecrease(int money = 1)
    {
        this.money -= money;
        if (this.money <= 0) this.money = 0;
    }

}
