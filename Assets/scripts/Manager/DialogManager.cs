using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : Singleton<GameManager>
{
    private const string CSV_FILENAME_MAINEVENT = "main_event";

    private const string SPLIT_STANDARD = ".";

    public TMP_Text narrativeText;
    public TMP_Text portraitText;
    public GameObject narrativeObject;
    public GameObject portraitObject;

    public RawImage image1;
    public RawImage image2;

    private GameObject currentObject;
    
    // event_id - group_id - dialog_info
    //private Dictionary<int, Dictionary<int, List<Dialog>>> allDialogs;
    private Dictionary<int, List<Dialog>> dialogLists;
    private List<Dialog> currentDialogs;
    private int currentDialogIdx;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        init();
    }

    private void init()
    {
        /*
        allDialogs = new Dictionary<int, Dictionary<int, List<Dialog>>>();
        currentDialogs = null;

        Dictionary<int, List<Dialog>> tempDialogLists;
        List<Dialog> tempDialogs;

        Dialog tempDialog;
        int eventId;
        int dialogGroupId;

        List<Dictionary<string, object>> mainEventDB = CSVReader.Read(CSV_FILENAME_MAINEVENT);
        foreach (Dictionary<string, object> data in mainEventDB)
        {
            eventId = (int)data["event_id"];
            if (!allDialogs.TryGetValue(eventId, out tempDialogLists))
            {
                allDialogs.Add(eventId, new Dictionary<int, List<Dialog>>());
            }

            dialogGroupId = (int)data["dialog_group_id"];

            if (!allDialogs[eventId].TryGetValue(dialogGroupId, out tempDialogs))
            {
                allDialogs[eventId].Add(dialogGroupId, new List<Dialog>());
            }

            tempDialog = new Dialog((int)data["dialog_num"], data["type"].ToString(), data["text"].ToString());
            allDialogs[eventId][dialogGroupId].Add(tempDialog);
        }

        int groupCount = 0;
        int dialogCount = 0;
        foreach (Dictionary<int, List<Dialog>> data in allDialogs.Values)
        {
            groupCount += data.Count;
            foreach (List<Dialog> dialog in data.Values)
            {
                dialogCount += dialog.Count;
            }
        }

        Debug.Log($"event: {allDialogs.Count}\n group: {groupCount}\n dialog: {dialogCount}");
        */
        
        
        dialogLists = new Dictionary<int, List<Dialog>>();
        
        int dialogGroupId;
        List<Dialog> tempDialogs;
        Dialog tempDialog;
        Color32 color;
        string[] splitColor;
        currentObject = null;
        
        List<Dictionary<string, object>> mainEventDB = CSVReader.Read(CSV_FILENAME_MAINEVENT);
        foreach (Dictionary<string, object> data in mainEventDB)
        {
            dialogGroupId = (int)data["dialog_group_id"];

            if (!dialogLists.TryGetValue(dialogGroupId, out tempDialogs))
            {
                dialogLists.Add(dialogGroupId, new List<Dialog>());
            }

            splitColor = data["color"].ToString().Split(SPLIT_STANDARD);
            color = new Color32(Convert.ToByte(splitColor[0]), Convert.ToByte(splitColor[1]), Convert.ToByte(splitColor[2]), Convert.ToByte(splitColor[3]));
            tempDialog = new Dialog((int)data["dialog_num"], data["type"].ToString(), data["text"].ToString(), float.Parse(data["duration"].ToString()), Convert.ToBoolean(data["loop"].ToString()), color);
            dialogLists[dialogGroupId].Add(tempDialog);
        }

        //Debug.Log($"Dialog list count: {dialogLists.Count}");
        StartDialog(1);
    }

    
    public void StartDialog(int dialogGroupId)
    {
        initDialog(dialogGroupId);
    }

    private void initDialog(int dialogGroupId)
    {
        currentDialogs = dialogLists[dialogGroupId];
        currentDialogIdx = 0;

        handleDialog(currentDialogs[currentDialogIdx++]);
    }
    

    public List<Dialog> GetDialogList(int groupId)
    {
        if (!dialogLists.ContainsKey(groupId)) return new List<Dialog>();
        
        return dialogLists[groupId];
    }

    private void handleDialog(Dialog dialog)
    {
        switch (dialog.Type)
        {
            case Dialog.eDialogType.NARRATIVE:
                // update text
                toggleCurrentObject(narrativeObject);
                narrativeText.text = dialog.Text;
                break;
            
            case Dialog.eDialogType.PORTRAIT:
                // update text
                toggleCurrentObject(portraitObject);
                portraitText.text = dialog.Text;
                break;
            
            case Dialog.eDialogType.IMAGE1:
                // activate image
                toggleCurrentObject(image1.gameObject);
                image1.texture = Resources.Load<Texture>($"Sprites/InGame/{dialog.Text}");
                image1.color = dialog.Color;
                break;
            
            case Dialog.eDialogType.IMAGE2:
                // activate image
                toggleCurrentObject(image2.gameObject);
                image2.texture = Resources.Load<Texture>($"Sprites/InGame/{dialog.Text}");
                image2.color = dialog.Color;
                break;

            case Dialog.eDialogType.BGM:
                // play bgm
                SoundManager.Instance().ChangeBGM(dialog.Text, dialog.Loop);
                UpdateDialog();
                break;
            
            case Dialog.eDialogType.SFX:
                // play sfx
                SoundManager.Instance().ChangeSFX(dialog.Text, dialog.Loop);
                UpdateDialog();
                break;
            
            case Dialog.eDialogType.OBJECT:
                break;
            
            default:
                Debug.Log($"Invalid dialog type: {dialog.Type}");
                break;
        }
    }

    public void UpdateDialog()
    {
        if (currentDialogs == null 
            || currentDialogs.Count <= currentDialogIdx)
        {
            // exit dialog
            currentObject.SetActive(false);
            currentDialogs = null;
            currentDialogIdx = 0;
            currentObject = null;
            return;
        }

        handleDialog(currentDialogs[currentDialogIdx++]);
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
}
