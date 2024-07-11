using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    [SerializeField]
    private Transform trans;
    private List<GameObject> objects;
    private GameObject prefab;
    private GameObject selectedObject;
    private int offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = 0;
        objects = new List<GameObject>();
        prefab = Resources.Load<GameObject>("Prefabs/UIs/Option");
        if (prefab == null)
        {
            Debug.Log("생성가능한 프리펩이 없음");
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                objects.Add(Instantiate(prefab, trans));
            }
            selectedObject = objects[0];
            selectedObject.GetComponent<Image>().color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.BInputToglle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                offset -= 1;
                ChangeOption();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                offset += 1;
                ChangeOption();
            }
        }
    }

    private void ChangeOption()
    {
        if (offset < 0)
        {
            offset = 0;
        }
        else if (offset >= objects.Count)
        {
            offset = objects.Count - 1;
        }
        else
        {
            selectedObject.GetComponent<Image>().color = Color.white;
            selectedObject = objects[offset];
            selectedObject.GetComponent<Image>().color = Color.red;
        }
    }

}
