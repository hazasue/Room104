using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductData
{
    public string nameKR;
    public string nameEN;
    public string descriptionKR;
    public string descriptionEN;
    public string requiredStat;
    public int requiredStatValue;
    public int price;
    public int count;

    public ProductData(string nameKR, string nameEN, string descriptionKR, string descriptionEN, string requiredStat, int requiredStatValue, int price, int count)
    {
        this.nameKR = nameKR;
        this.nameEN = nameEN;
        this.descriptionKR = descriptionKR;
        this.descriptionEN = descriptionEN;
        this.requiredStat = requiredStat;
        this.requiredStatValue = requiredStatValue;
        this.price = price;
        this.count = count;
    }
}
