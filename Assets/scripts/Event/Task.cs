using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Task
{
    public enum eState
    {
        Waiting,
        Running,
        End
    }

    protected int taskID;
    public int TaskID { get { return taskID; } }

    protected int otherEventID;
    public int OtherEventID { get { return otherEventID; } }

    protected eState state;
    public eState State { get { return state; } }

    public Task(string taskID, string otherEventID)
    {
        int.TryParse(taskID, out this.taskID);
        if (otherEventID != "null")
            int.TryParse(otherEventID, out this.otherEventID);
        else
            this.otherEventID = -1;
        state = eState.Waiting;
    }

    public abstract void Excute();

    protected Color StringToColor(string color)
    {
        string[] rgba = color.Substring(0, color.Length).Split("_");
        return new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3]));
    }
}

public class TextOutput : Task
{
    private int effect;
    private string text;
    public string Text { get { return text; } }
    float elapseTime = 0.0f;
    float completionTIme = 1.0f;
    public TextOutput(string taskID, string effect, string text, string otherEventID) : base(taskID, otherEventID)
    {
        int.TryParse(effect, out this.effect);
        this.text = text.Replace("\\n","\n");
    }

    public override void Excute()
    {
        switch(effect)
        {
            //페이드 인
            case 2:
                if (state == eState.Waiting)
                {
                    state = eState.Running;
                    UITest.Instance.ModifyNoticeText(text);
                    UITest.Instance.NoticeObject.SetActive(true);
                }
                fadeIn();
                if(elapseTime >= completionTIme)
                    state = eState.End;
                break;
            //페이드 아웃
            case 3:
                fadeOut();

                if (elapseTime >= completionTIme)
                {
                    UITest.Instance.NoticeObject.SetActive(false);
                    state = eState.End;
                }
                break;
            default:
                Debug.Log("텍스트 출력 효과에 없는 번호가 들어왔습니다.");
                break;
        }

    }
    private void fadeIn()
    {
        UITest.Instance.NoticeObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0f, 1f, elapseTime / completionTIme);
        elapseTime += Time.deltaTime;
    }
    private void fadeOut()
    {
        UITest.Instance.NoticeObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0f, elapseTime / completionTIme);
        elapseTime += Time.deltaTime;
    }
}

public class Timer : Task
{
    float elapseTime = 0.0f;
    float completionTIme;
    public Timer(string taskID, string time, string otherEventID) : base(taskID, otherEventID)
    {
        float.TryParse(time, out this.completionTIme);
        this.completionTIme = this.completionTIme * 0.001f;
    }

    public override void Excute()
    {
        switch (state)
        {
            case eState.Waiting:
                state = eState.Running;
                break;
            case eState.Running:
                elapseTime += Time.deltaTime;
                break;
            default:
                Debug.Log("Timer 에러");
                break;
        }
        if (elapseTime >= completionTIme)
            state = eState.End;
    }
}

public class ImageOutput : Task
{
    private string imageLayer;
    private string resourceName;
    private int resourceNum;
    private int effect;
    private Color color;

    public ImageOutput(string taskID, string imageLayer, string resourceName, string resourceNum, string color, string effect, string otherEventID) : base(taskID, otherEventID)
    {
        this.imageLayer = imageLayer;
        this.resourceName = resourceName;
        int.TryParse(resourceNum, out this.resourceNum);
        int.TryParse(effect, out this.effect);
        this.color = StringToColor(color);
    }

    public override void Excute()
    {

    }
}

public class SpawnOrDespawn : Task
{
    private GameObject subject;
    private Vector2 location;
    private int condition;
    public SpawnOrDespawn(string taskID, string objectName, string locationName, string condition, string ohterEventID) : base(taskID, ohterEventID)
    {
        //subject = GameObject.Find(objectName);
        //location = GameObject.Find(locationName).transform.position;
        int.TryParse(condition, out this.condition);
    }

    public override void Excute()
    {
        
    }
}

public class BGM : Task
{
    private string resourceName;
    private int effect;

    public BGM(string taskID, string resourceName, string effect, string ohterEventID) : base(taskID, ohterEventID)
    {
        this.resourceName = resourceName;
        int.TryParse(effect, out this.effect);
    }

    public override void Excute()
    {

    }
}

public class SFX : Task
{
    private string resourceName;
    private int volume;
    private int effect;

    public SFX(string taskID, string resourceName, string volume, string effect, string ohterEventID) : base(taskID, ohterEventID)
    {
        this.resourceName = resourceName;
        int.TryParse(volume, out this.volume);
        int.TryParse(effect, out this.effect);
    }

    public override void Excute()
    {

    }
}

public class SpriteModifier : Task
{
    private GameObject subject;
    private string resourceName;
    private int resourceNum;
    private int effect;

    public SpriteModifier(string taskID, string objectName, string resourceName, string resourceNum, string effect, string ohterEventID) : base(taskID, ohterEventID)
    {
        //subject = GameObject.Find(objectName);
        this.resourceName = resourceName;
        int.TryParse(resourceNum, out this.resourceNum);
        int.TryParse(effect, out this.effect);
    }

    public override void Excute()
    {

    }
}

public class ObjectMove : Task
{
    private GameObject subject;
    private Vector2 location;

    public ObjectMove(string taskID, string objectName, string locationName, string ohterEventID) : base(taskID, ohterEventID)
    {
        //subject = GameObject.Find(objectName);
        //location = GameObject.Find(locationName).transform.position;
    }

    public override void Excute()
    {

    }
}

public class Narrative : Task
{
    private int effect;
    private string text;

    public Narrative(string taskID, string effect, string text, string ohterEventID) : base(taskID, ohterEventID)
    {
        int.TryParse(effect, out this.effect);
        this.text = text;
    }

    public override void Excute()
    {

    }
}

public class Portrait : Task
{
    private string resourceName;
    private int resourceNum;
    private int effect;
    private string name;
    private string text;

    public Portrait(string taskID, string resourceName, string resourceNum, string effect, string name, string text, string ohterEventID) : base(taskID, ohterEventID)
    {
        this.resourceName = resourceName;
        int.TryParse(resourceNum, out this.resourceNum);
        int.TryParse(effect, out this.effect);
        this.name = name;
        this.text = text;
    }

    public override void Excute()
    {

    }
}

public class Animation : Task
{
    private GameObject subject;
    private string animationName;

    public Animation(string taskID, string objectName, string animationName, string ohterEventID) : base(taskID, ohterEventID)
    {
        //subject = GameObject.Find(objectName);
        this.animationName = animationName;
    }

    public override void Excute()
    {

    }
}







