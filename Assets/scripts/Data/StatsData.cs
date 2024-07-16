using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    public float health;
    public float stress;
    public float maxStamina;
    public float stamina;
    public float intelligence;
    public float speed;
    public int money;
    public float safety;

    public StatsData(float health, float stress, float maxStamina, float stamina, float intelligence, float speed, int money, float safety)
    {
        this.health = health;
        this.stress = stress;
        this.maxStamina = maxStamina;
        this.stamina = stamina;
        this.intelligence = intelligence;
        this.speed = speed;
        this.money = money;
        this.safety = safety;
    }
}
