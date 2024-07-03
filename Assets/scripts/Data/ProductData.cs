using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductData
{
    public string name;
    public string option;
    public int price;
    public int count;

    public ProductData(string name, string option, int price, int count)
    {
        this.name = name;
        this.option = option;
        this.price = price;
        this.count = count;
    }
}
