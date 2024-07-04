using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private const string CSV_FILENAME_MAINEVENT = "main_event";
    
    // event_id - group_id - dialog_info
    private Dictionary<int, Dictionary<int, List<Dialog>>> allDialogs;
    private List<Dialog> currentDialogs;
    private int currentDialogIdx;

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
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

        //StartDialog(10001, 1);
    }

    public void StartDialog(int eventId, int dialogGroupId)
    {
        initDialog(eventId, dialogGroupId);
    }

    private void initDialog(int eventId, int dialogGroupId)
    {
        currentDialogs = allDialogs[eventId][dialogGroupId];
        currentDialogIdx = 0;

        handleDialog(currentDialogs[currentDialogIdx++]);
    }

    private void handleDialog(Dialog dialog)
    {
        switch (dialog.Type)
        {
            case Dialog.eDialogType.NARRATIVE:
                // update text
                break;
            
            case Dialog.eDialogType.PORTRAIT:
                // update text
                Debug.Log($"PORTRAIT: {dialog.Num}, {dialog.Type}, {dialog.Text}");
                break;
            
            case Dialog.eDialogType.IMAGE:
                // activate image
                break;
            
            case Dialog.eDialogType.BGM:
                // play bgm
                break;
            
            case Dialog.eDialogType.SFX:
                // play sfx
                Debug.Log($"SFX: {dialog.Num}, {dialog.Type}, {dialog.Text}");
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
            currentDialogs = null;
            currentDialogIdx = 0;
            return;
        }

        handleDialog(currentDialogs[currentDialogIdx++]);
    }
}
