using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public Transform viewport;

    public Product productObject;

    private Dictionary<string, ProductData> productDatas;
    private Dictionary<string, Product> products; 

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
        productDatas = JsonManager.LoadJsonFile<Dictionary<string, ProductData>>(JsonManager.DEFAULT_PRODUCT_DATA_NAME);
        products = new Dictionary<string, Product>();

        for (int i = viewport.childCount - 1; i >= 0; i--)
        {
            Destroy(viewport.GetChild(i).gameObject);
        }

        Product tempProduct;
        foreach (ProductData data in productDatas.Values)
        {
            tempProduct = Instantiate(productObject, viewport, true);
            tempProduct.Init(data.name, data.option, data.price, data.count);
        }
    }
}
