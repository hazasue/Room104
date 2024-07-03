using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Product : MonoBehaviour
{
    private string name;
    private string option;
    private int price;
    private int count;

    public RawImage image;
    public TMP_Text nameText;
    public TMP_Text optionText;
    public TMP_Text priceText;

    public void Init(string name, string option, int price, int count)
    {
        this.name = name;
        this.option = option;
        this.price = price;
        this.count = count;

        image.texture = Resources.Load<Texture>("/sprites/" + name);
        nameText.text = name;
        optionText.text = option;
        priceText.text = $"${price}";
    }
}
