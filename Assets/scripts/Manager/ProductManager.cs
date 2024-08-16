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
        init();
    }

    private void init()
    {
        products = new Dictionary<int, Product>();

        for (int i = viewport.childCount - 1; i >= 0; i--)
        {
            Destroy(viewport.GetChild(i).gameObject);
        }

        Product tempProduct;
        foreach (KeyValuePair<int, ProductData> data in GameManager.Instance.Products)
        {
            tempProduct = Instantiate(productObject, viewport, true);
            tempProduct.Init(data.Value.nameKR, data.Value.description, data.Value.price, data.Value.count);
            products.Add(data.Key, tempProduct);
        }
    }
}
