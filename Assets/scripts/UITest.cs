using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITest : Singleton<UITest>
{
    [SerializeField]
    private Transform trans;
    private List<GameObject> objects;
    private GameObject prefab;
    private GameObject selectedObject;
    private int offset;

    [SerializeField]
    private GameObject noticeObject;
    public GameObject NoticeObject { get { return noticeObject; } }
    private TMP_Text noticeText;

    [SerializeField]
    private GameObject playerInfoUI;
    public GameObject PlayerUI { get { return playerInfoUI; } }

    [SerializeField]
    private GameObject image1;
    public GameObject Image1 { get { return image1; } }
    [SerializeField]
    private GameObject image2;
    public GameObject Image2 { get { return image2; } }
    [SerializeField]
    private GameObject image3;
    public GameObject Image3 { get { return image3; } }
    [SerializeField]
    private GameObject image4;
    public GameObject Image4 { get { return image4; } }
    
    [SerializeField]
    private GameObject narrativePanel;
    [SerializeField]
    private TMP_Text narrativeText;

    [SerializeField]
    private GameObject portraitPanel;
    [SerializeField]
    private GameObject portraitObj;
    private Image portrait;
    [SerializeField]
    private TMP_Text portraitName;
    [SerializeField]
    private TMP_Text portraitText;
    private TMP_Text selectionText;

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

        noticeText = noticeObject.GetComponentInChildren<TMP_Text>();
        portrait = portraitObj.GetComponent<Image>();
        InitNarrativeUI();
        InitPortraitUI();
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
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                InputManager.Instance.ChangeUIToggle(false);
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

    public void ModifyNoticeText(string str)
    {
        noticeText.text = str;
    }

    private void InitPortraitUI()
    {

    }

    public void ActivatePortrait()
    {
        portraitPanel.SetActive(true);
    }

    public void DeactivatePortrait()
    {
        portraitPanel.SetActive(false);
    }

    public void ModifyPortrait(string rescName, int rescNum, string name, string text)
    {
        portrait.sprite = Resources.LoadAll<Sprite>("Sprites/초상화/" + rescName)[rescNum];
        portraitName.text = name;
        portraitText.text = text;
    }

    private void InitNarrativeUI()
    {

    }

    public void ActivateNarrative()
    {
        narrativePanel.SetActive(true);
    }

    public void DeactivateNarrative()
    {
        narrativePanel.SetActive(false);
    }

    public void ModifyNarrative(string text)
    {
        narrativeText.text = text;
    }

}
