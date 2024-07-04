using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public enum eDialogType
    {
        NARRATIVE,
        PORTRAIT,
        IMAGE,
        BGM,
        SFX,
        OBJECT
    }

    private int num;
    public int Num
    {
        get { return num; }
    }
    
    private eDialogType type;
    public eDialogType Type 
    { 
        get { return type; }
    }
    
    private string text;
    public string Text
    {
        get { return text; }
    }

    public Dialog(int dialogNum, string dialogType, string dialogText)
    {
        this.num = dialogNum;
        this.text = dialogText;

        switch (dialogType)
        {
            case "narrative":
                this.type = eDialogType.NARRATIVE;
                break;

            case "portrait":
                this.type = eDialogType.PORTRAIT;
                break;
            
            case "image":
                this.type = eDialogType.IMAGE;
                break;
            
            case "bgm":
                this.type = eDialogType.BGM;
                break;
            
            case "sfx":
                this.type = eDialogType.SFX;
                break;
            
            case "object":
                this.type = eDialogType.OBJECT;
                break;
            
            default:
                Debug.Log($"Invalid dialog type: {dialogType}");
                break;
        }
    }

    public void Init(int dialogNum, string dialogType, string dialogText)
    {
        this.num = dialogNum;
        this.text = dialogText;

        switch (dialogType)
        {
            case "narrative":
                this.type = eDialogType.NARRATIVE;
                break;

            case "portrait":
                this.type = eDialogType.PORTRAIT;
                break;
            
            case "image":
                this.type = eDialogType.IMAGE;
                break;
            
            case "bgm":
                this.type = eDialogType.BGM;
                break;
            
            case "sfx":
                this.type = eDialogType.SFX;
                break;
            
            case "object":
                this.type = eDialogType.OBJECT;
                break;
            
            default:
                Debug.Log($"Invalid dialog type: {dialogType}");
                break;
        }
    }
}
