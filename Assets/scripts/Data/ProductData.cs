using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductData
{
    public string nameKR;
    public string nameEN;
    public string description;
    public string requiredStat;
    public int requiredStatValue;
    public int price;
    public int count;

    public ProductData(string nameKR, string nameEN, string description, string requiredStat, int requiredStatValue, int price, int count)
    {
        this.nameKR = nameKR;
        this.nameEN = nameEN;
        this.description = description;
        this.requiredStat = requiredStat;
        this.requiredStatValue = requiredStatValue;
        this.price = price;
        this.count = count;
    }
}
