using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SayTalkManager : MonoBehaviour
{
    private static SayTalkManager instance;
    
    private const string CSV_FILENAME_SAYTALK = "say_talk";
    private const string SPLIT_STANDARD = ".";
    private const int MAX_SAVE_SLOT_COUNT = 3;
    private static Vector3 SAYTALK_POSITION_DEFAULT = new Vector3(960f, 540f, 0f);
    private static Vector3 SAYTALK_POSITION_UNDER = new Vector3(960f, -402f, 0f);

    private Dictionary<int, Dictionary<int, SayTalkHistory>> datas;

    private SayTalkHistory dummyData;
    private Dictionary<int, SayTalkHistory> sayTalkDatas;
    public Dictionary<int, SayTalkHistory> SayTalkDatas
    {
        get { return sayTalkDatas; }
    }
    
    private List<SayTalkData> sayTalkData;
    private Dictionary<int, List<SayTalk>> sayTalkLists;   
    private List<SayTalk> currentSayTalkList;
    private int currentSayTalkIdx;
    private int targetId;

    private GameObject currentObject;
    
    public TMP_Text narrativeText;
    public GameObject nullObject;
    public GameObject narrativeObject;
    public GameObject updateObject;
    public GameObject animationObject;
    public RawImage animationImage; 
    public Transform smartPhoneScreen;

    public Dictionary<string, string> objects; 
    
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    public static SayTalkManager Instance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<SayTalkManager>();
        if (instance == null) Debug.Log("There's no active SayTalkManager object");
        return instance;
    }

    private void init()
    {
        initDatas();
        
        instance = this;

        sayTalkLists = new Dictionary<int, List<SayTalk>>();
        objects = new Dictionary<string, string>();
        objects.Add("img_phone_sayTalk_List", "saytalk");
        objects.Add("img_phone_sayTalk_Chat", "saytalkhistory");
        objects.Add("inactivate_chat", "inactivate_chat");
        
        int eventId;
        List<SayTalk> tempTalks;
        SayTalk tempTalk;
        Color32 color;
        string[] splitColor;
        currentObject = nullObject;
        sayTalkData = null;
        targetId = -1;
        //event_id, dialog_num, type, isPlayer, autoSkip, text, duration, loop, color, direction
        List<Dictionary<string, object>> mainEventDB = CSVReader.Read(CSV_FILENAME_SAYTALK);
        foreach (Dictionary<string, object> data in mainEventDB)
        {
            eventId = (int)data["event_id"];

            if (!sayTalkLists.TryGetValue(eventId, out tempTalks))
            {
                sayTalkLists.Add(eventId, new List<SayTalk>());
            }

            splitColor = data["color"].ToString().Split(SPLIT_STANDARD);
            color = new Color32(Convert.ToByte(splitColor[0]), Convert.ToByte(splitColor[1]), Convert.ToByte(splitColor[2]), Convert.ToByte(splitColor[3]));
            tempTalk = new SayTalk((int)data["dialog_num"], data["type"].ToString(),
                Convert.ToBoolean(data["isPlayer"].ToString()), Convert.ToBoolean(data["autoSkip"].ToString()),
                data["text_kor"].ToString(), data["text_en"].ToString(), float.Parse(data["duration"].ToString()),
                Convert.ToBoolean(data["loop"].ToString()), color, data["direction"].ToString(), (int)data["target"]);
            sayTalkLists[eventId].Add(tempTalk);
        }

        initSayTalk(9002);
    }

    private void initDatas()
    {
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_SAYTALK_DATA_NAME + ".json"))
        {
            datas = new Dictionary<int, Dictionary<int, SayTalkHistory>>();
            for (int i = 0; i < MAX_SAVE_SLOT_COUNT; i++)
            {
                datas.Add(i, null);
            }
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_SAYTALK_DATA_NAME, datas);
        }
        else
        {
            datas = JsonManager.LoadJsonFile<Dictionary<int, Dictionary<int, SayTalkHistory>>>(JsonManager.DEFAULT_SAYTALK_DATA_NAME);
        }

        if (JsonManager.LoadJsonFile<CurrentGameInfo>(JsonManager.DEFAULT_CURRENT_DATA_NAME).newGame)
        {
            sayTalkDatas = new Dictionary<int, SayTalkHistory>();
        }
        else
        {
            sayTalkDatas = datas[JsonManager.LoadJsonFile<CurrentGameInfo>(JsonManager.DEFAULT_CURRENT_DATA_NAME).dataId];
        }
    }
    
    private void initSayTalk(int eventId)
    {
        currentSayTalkList = sayTalkLists[eventId];
        currentSayTalkIdx = 0;

        handleSayTalk(currentSayTalkList[currentSayTalkIdx++]);
    }
    

    public List<SayTalk> GetSayTalkList(int eventId)
    {
        if (!sayTalkLists.ContainsKey(eventId)) return new List<SayTalk>();
        
        return sayTalkLists[eventId];
    }

    private void handleSayTalk(SayTalk sayTalk)
    {
        if (narrativeObject.activeSelf == false) narrativeObject.SetActive(false);
        
        switch (sayTalk.Type)
        {
            case eSayTalkType.NARRATIVE:
                // activate narrative screen
                toggleCurrentObject(narrativeObject);
                if (Settings.Instance().isKorean) narrativeText.text = sayTalk.TextKor;
                else
                {
                    narrativeText.text = sayTalk.TextEn;
                }
                
                // update text
                break;
            
            case eSayTalkType.SFX:
                // loop, duration, clip
                toggleCurrentObject(nullObject);
                SoundManager.Instance().ChangeSFX(sayTalk.TextKor, sayTalk.Loop);
                UpdateSayTalk(sayTalk.Duration);
                break;
            
            case eSayTalkType.OBJECT:
                toggleCurrentObject(nullObject);
                if (objects[sayTalk.TextKor] == "inactivate_chat") UIManager.GetInstance().InactivateScreen();
                else
                {
                    UIManager.GetInstance().ActivateScreen(objects[sayTalk.TextKor]);
                }
                UpdateSayTalk(sayTalk.Duration);
                break;
            
            case eSayTalkType.TEXT:
                // update say talk text list
                toggleCurrentObject(updateObject);
                if (sayTalkData == null)
                {
                    if (!sayTalkDatas.TryGetValue(sayTalk.Target, out dummyData))
                    {
                        sayTalkDatas.Add(sayTalk.Target, new SayTalkHistory(new List<SayTalkData>()));
                    }
                    sayTalkData = sayTalkDatas[sayTalk.Target].datas;
                    targetId = sayTalk.Target;
                }
                sayTalkData.Add(new SayTalkData(sayTalk.IsPlayer, sayTalk.TextKor, sayTalk.TextEn));
                UIManager.GetInstance().InitSayTalkHistory(sayTalk.Target);
                break;
            
            case eSayTalkType.ANIMATION:
                Color tempColor;
                switch (sayTalk.TextKor)
                {
                    case "anim_phone_talk_on":
                        StartCoroutine(animateImage(true, sayTalk.Duration));
                        break;
                    
                    case "anim_phone_talk_off":
                        StartCoroutine(animateImage(false, sayTalk.Duration));
                        break;
                    
                    default:
                        break;
                }
                break;
            
            default:
                Debug.Log($"Invalid say talk type: {sayTalk.Type}");
                break;
        }
    }

    public void UpdateSayTalk(float delay = 0f)
    {
        StartCoroutine(updateSayTalk(delay));
    }

    private IEnumerator updateSayTalk(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (currentSayTalkList == null 
            || currentSayTalkList.Count <= currentSayTalkIdx)
        {
            // exit dialog
            currentObject.SetActive(false);
            currentSayTalkList = null;
            currentSayTalkIdx = 0;
            currentObject = nullObject;
            sayTalkData = null;
            targetId = -1;
            yield break;
        }

        handleSayTalk(currentSayTalkList[currentSayTalkIdx++]);
    }
    
    private void toggleCurrentObject(GameObject selectedObject)
    {
        if (currentObject != selectedObject)
        {
            if(currentObject != null) currentObject.SetActive(false);
            currentObject = selectedObject;
            currentObject.SetActive(true);
        }
    }

    private IEnumerator animateImage(bool isOn, float duration)
    {
        Color tempColor;
        float timeGap;
        
        if (isOn)
        {
            smartPhoneScreen.position = SAYTALK_POSITION_UNDER;
            UIManager.GetInstance().ActivateScreen("smartphone");
            while (smartPhoneScreen.position.y <= SAYTALK_POSITION_DEFAULT.y)
            {
                timeGap = Time.deltaTime;
                yield return new WaitForSeconds(timeGap);
                smartPhoneScreen.transform.position += new Vector3(0f, 1f, 0f) * timeGap * 1606f;
            }

            smartPhoneScreen.transform.position = SAYTALK_POSITION_DEFAULT;
            
            UIManager.GetInstance().ActivateScreen(objects["img_phone_sayTalk_List"]);
            
            toggleCurrentObject(animationObject);
            tempColor = animationImage.color;
            tempColor.a = 1f;
            animationImage.color = tempColor;
            while (tempColor.a > 0f)
            {
                timeGap = Time.deltaTime;
                yield return new WaitForSeconds(timeGap);
                tempColor.a -= timeGap / duration * 2f;
                animationImage.color = tempColor;
            }
        }
        else
        {
            toggleCurrentObject(animationObject);
            tempColor = animationImage.color;
            tempColor.a = 0f;
            animationImage.color = tempColor;
            while (tempColor.a < 1f)
            {
                timeGap = Time.deltaTime;
                yield return new WaitForSeconds(timeGap);
                tempColor.a += timeGap / duration * 2f;
                animationImage.color = tempColor;
            }

            toggleCurrentObject(nullObject);
            UIManager.GetInstance().InactivateScreen();
            UIManager.GetInstance().InactivateScreen();
            
            smartPhoneScreen.position = SAYTALK_POSITION_DEFAULT;
            while (smartPhoneScreen.position.y >= SAYTALK_POSITION_UNDER.y)
            {
                timeGap = Time.deltaTime;
                yield return new WaitForSeconds(timeGap);
                smartPhoneScreen.transform.position -= new Vector3(0f, 1f, 0f) * timeGap * 1606f;
            }

            smartPhoneScreen.gameObject.SetActive(false);
            smartPhoneScreen.transform.position = SAYTALK_POSITION_DEFAULT;
        }
        UpdateSayTalk(0f);
    }
}
