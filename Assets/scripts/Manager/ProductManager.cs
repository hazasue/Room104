using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public Transform viewport;

    public Product productObject;

    private Dictionary<int, Product> products; 

    // Start is called before the first frame update
    void Awake()
    {
        //Init();
    }

    public void Init()
    {
        products = new Dictionary<int, Product>();

        for (int i = viewport.childCount - 1; i >= 0; i--)
        {
            Destroy(viewport.GetChild(i).gameObject);
        }

        Product tempProduct;
        string name;
        string description;
        foreach (KeyValuePair<int, ProductData> data in GameManager.Instance.Products)
        {
            if (Settings.Instance().isKorean)
            {
                name = data.Value.nameKR;
                description = data.Value.descriptionKR;
            }
            else
            {
                name = data.Value.nameEN;
                description = data.Value.descriptionEN;
            }
            
            tempProduct = Instantiate(productObject, viewport, true);
            tempProduct.Init(name, description, data.Value.price, data.Value.count);
            products.Add(data.Key, tempProduct);
        }
    }
}
