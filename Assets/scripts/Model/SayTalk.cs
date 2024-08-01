using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum eSayTalkType
{
    NARRATIVE,
    SFX,
    OBJECT,
    TEXT,
    ANIMATION
}

public class SayTalk : MonoBehaviour
{
    private int num;
    public int Num
    {
        get { return num; }
    }
    
    private eSayTalkType type;
    public eSayTalkType Type 
    { 
        get { return type; }
    }

    private bool isPlayer;
    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    private bool autoSkip;
    public bool AutoSkip
    {
        get { return autoSkip; }
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

    private string direction;
    public string Direction
    {
        get { return direction; }
    }

    private int target;

    public int Target
    {
        get { return target; }
    }

    public SayTalk(int num, string type, bool isPlayer, bool autoSkip, string text, float duration, bool loop,
        Color32 color, string direction, int target)
    {
        this.num = num;
        this.isPlayer = isPlayer;
        this.autoSkip = autoSkip;
        this.text = text;
        this.duration = duration;
        this.loop = loop;
        this.color = color;
        this.direction = direction;
        this.target = target;

        switch (type)
        {
            case "narrative":
                this.type = eSayTalkType.NARRATIVE;
                break;
            
            case "sfx":
                this.type = eSayTalkType.SFX;
                break;
            
            case "object":
                this.type = eSayTalkType.OBJECT;
                break;
            
            case "text":
                this.type = eSayTalkType.TEXT;
                break;
            
            case "animation":
                this.type = eSayTalkType.ANIMATION;
                break;
            
            default:
                Debug.Log($"Invalid say talk type: {type}");
                break;
        }
    }
}
