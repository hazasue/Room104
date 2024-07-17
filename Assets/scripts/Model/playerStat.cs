using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStat
{
    public const float MAX_HEALTH = 1000f;
    public const float MAX_STRESS = 1000f;
    public const float MAX_STAMINA = 2000f;
    public const float MAX_INTELLIGENCE = 1000f;
    public const float MAX_SPEED = 1500f;
    public const int MAX_MONEY = 9999999;
    public const float MAX_SAFETY = 3f;

    private float health;
    public float Health { get { return health; } }
    private float stress;
    public float Stress { get { return stress; } }
    private float stamina;
    public float Stamina { get { return stamina; } }
    private float maxStamina;
    public float MaxStamina { get { return maxStamina; } }
    private float intelligence;
    public float Intelligence { get { return intelligence; } }
    private float speed;
    public float Speed { get { return speed; } }
    private int money;
    public int Money { get { return money; } }
    private float safety;
    public float Safety { get { return safety; } }

    public PlayerStat()
    {
        health = 500f;
        stress = 700f;
        stamina = 1000f;
        maxStamina = 1000f;
        intelligence = 500f;
        speed = 1000f;
        money = 0;
        safety = 0f;
    }

    public void InitSavedStats(StatsData data)
    {
        this.health = data.health;
        this.stress = data.stress;
        this.stamina = data.stamina;
        this.maxStamina = data.maxStamina;
        this.intelligence = data.intelligence;
        this.speed = data.speed;
        this.money = data.money;
        this.safety = data.safety;
    }

    public StatsData ConvertToData()
    {
        return new StatsData(health, stress, stamina, maxStamina, intelligence, speed, money, safety);
    }

    public void DebugLogPlayerStat()
    {
        Debug.Log(this.health + ", " + this.stress + ", " + this.stamina + ", " + this.intelligence + ", " + this.speed);
    }

    public void IncreaseHealth(float health = 50f)
    {
        this.health += health;
        if (this.health >= MAX_HEALTH) this.health = MAX_HEALTH;
    }

    public void IncreaseStress(float stress = 50f)
    {
        this.stress += stress;
        if (this.stress >= MAX_STAMINA) this.stress = MAX_STRESS;
    }

    public void IncreaseMaxStamina(float maxstamina = 1000f)
    {
        this.maxStamina += maxstamina;
        if (this.maxStamina >= MAX_STAMINA) this.maxStamina = MAX_STAMINA;
    }

    public void IncreaseStamina(float stamina = 50f)
    {
        this.stamina += stamina;
        if (this.stamina >= this.maxStamina) this.stamina = this.maxStamina;
    }

    public void IncreaseIntelligence(float Int = 25f)
    {
        this.intelligence += Int;
        if (this.intelligence >= MAX_INTELLIGENCE) this.intelligence = MAX_INTELLIGENCE;
    }

    public void IncreaseSpeed(float speed = 25f)
    {
        this.speed += speed;
        if (this.Speed >= MAX_SPEED) this.speed = MAX_SPEED;
    }

    public void IncreaseMoney(int money = 1)
    {
        this.money += money;
        if(this.money >= MAX_MONEY) this.money = MAX_MONEY;
    }

    public void IncreaseSafety(float safety = 1f)
    {
        this.safety += safety;
        if (this.safety >= MAX_SAFETY)  this.safety = MAX_SAFETY;
    }

    public void DecreaseHealth(float health = 200f)
    {
        this.health -= health;
        if(this.health <= 0) this.health = 0;
    }

    public void DecreaseIntelligence(float intelligence = 50f)
    {
        this.intelligence -= intelligence;
        if (this.intelligence <= 0) this.intelligence = 0;
    }

    public void DecreaseStress(float stress = 50f)
    {
        this.stress -= stress;
        if (this.stress <= 0) this.stress = 0;
    }

    public void DecreaseStamina(float stamina = 1f)
    {
        this.stamina -= stamina;
        if( this.stamina <= 0) this.stamina = 0;
    }

    public void DecreaseMoney(int money = 1)
    {
        this.money -= money;
        if (this.money <= 0) this.money = 0;
    }
}
