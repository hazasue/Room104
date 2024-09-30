using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public enum eDialogType
    {
        NARRATIVE,
        PORTRAIT,
        IMAGE1,
        IMAGE2,
        BGM,
        SFX,
        MOVE,
        ANIMATION,
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

    private float duration;
    public float Duration
    {
        get { return duration; }
    }

    private bool loop;
    public bool Loop
    {
        get { return loop; }
    }

    private Color32 color;
    public Color32 Color
    {
        get { return color; }
    }

    public Dialog(int dialogNum, string dialogType, string dialogText, float duration, bool loop, Color32 color)
    {
        this.num = dialogNum;
        this.text = dialogText;
        this.duration = duration;
        this.loop = loop;
        this.color = color;

        switch (dialogType)
        {
            case "narrative":
                this.type = eDialogType.NARRATIVE;
                break;

            case "portrait":
                this.type = eDialogType.PORTRAIT;
                break;
            
            case "image1":
                this.type = eDialogType.IMAGE1;
                break;
            
            case "image2":
                this.type = eDialogType.IMAGE2;
                break;
            
            case "bgm":
                this.type = eDialogType.BGM;
                break;
            
            case "sfx":
                this.type = eDialogType.SFX;
                break;
            
            case "move":
                this.type = eDialogType.MOVE;
                break;
            
            case "animation":
                this.type = eDialogType.ANIMATION;
                break;
            
            case "object":
                this.type = eDialogType.OBJECT;
                break;
            
            default:
                Debug.Log($"Invalid dialog type: {dialogType}");
                break;
        }
    }

    public void Init(int dialogNum, string dialogType, string dialogText, float duration, bool loop)
    {
        this.num = dialogNum;
        this.text = dialogText;
        this.duration = duration;
        this.loop = loop;

        switch (dialogType)
        {
            case "narrative":
                this.type = eDialogType.NARRATIVE;
                break;

            case "portrait":
                this.type = eDialogType.PORTRAIT;
                break;
            
            case "image1":
                this.type = eDialogType.IMAGE1;
                break;
            
            case "image2":
                this.type = eDialogType.IMAGE2;
                break;
            
            case "bgm":
                this.type = eDialogType.BGM;
                break;
            
            case "sfx":
                this.type = eDialogType.SFX;
                break;
            
            case "move":
                this.type = eDialogType.MOVE;
                break;
            
            case "animation":
                this.type = eDialogType.ANIMATION;
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
