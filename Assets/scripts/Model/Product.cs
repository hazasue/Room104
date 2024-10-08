using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Product : MonoBehaviour
{
    private string name;
    private string description;
    private int price;
    private int count;

    public RawImage image;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;

    public GameObject warningScreen;
    public TMP_Text warningText;

    public void Init(string name, string description, int price, int count, bool canBuy = true)
    {
        this.name = name;
        this.description = description;
        this.price = price;
        this.count = count;

        image.texture = Resources.Load<Texture>("/sprites/" + name);
        nameText.text = name;
        descriptionText.text = description;
        priceText.text = $"${price:n0}";

        if (count <= 0)
        {
            warningScreen.SetActive(true);
            if (Settings.Instance().isKorean) warningText.text = "매진";
            else
            {
                warningText.text = "Sold Out";
            }
        }

        if (!canBuy)
        {
            warningScreen.SetActive(true);
            if (Settings.Instance().isKorean) warningText.text = "구매 불가능";
            else
            {
                warningText.text = "Can't Buy";
            }
        }
    }
}
