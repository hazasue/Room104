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

    public abstract void Execute();

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
    float offset = 0.5f;
    public TextOutput(string taskID, string effect, string text, string otherEventID) : base(taskID, otherEventID)
    {
        int.TryParse(effect, out this.effect);
        this.text = text.Replace("\\n","\n");
    }

    public override void Execute()
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
                if(elapseTime >= completionTIme + offset)
                    state = eState.End;
                break;
            //페이드 아웃
            case 3:
                fadeOut();
                if (elapseTime >= completionTIme + offset)
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

    public override void Execute()
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
    private Image imageComp;
    private string resourceName;
    private int resourceNum;
    private int effect;
    float elapseTime = 0.0f;
    float completionTIme = 1.0f;
    float offset = 0.5f;
    private Color color;

    public ImageOutput(string taskID, string imageLayer, string resourceName, string resourceNum, string color, string effect, string otherEventID) : base(taskID, otherEventID)
    {
        switch (imageLayer)
        {
            case "image1":
                imageComp = UITest.Instance.Image1.GetComponent<Image>();
                break;
            case "image2":
                imageComp = UITest.Instance.Image2.GetComponent<Image>();
                break;
/*            case "image3":
                imageComp = UITest.Instance.Image3.GetComponent<Image>();
                break;
            case "image4":
                imageComp = UITest.Instance.Image4.GetComponent<Image>();
                break;*/
            default:
                Debug.Log("ImageOutput Layer Error");
                break;
        }
        this.resourceName = resourceName;
        int.TryParse(resourceNum, out this.resourceNum);
        int.TryParse(effect, out this.effect);
        this.color = StringToColor(color);
    }

    public override void Execute()
    {
        if(state == eState.Waiting)
        {
            imageComp.sprite = Resources.Load<Sprite>("Sprites/원화/"+resourceName);
            imageComp.color = color;           
            state = eState.Running;
        }
        if(state == eState.Running)
        {
            switch(effect)
            {
                case 0:
                    imageComp.gameObject.SetActive(false);
                    state = eState.End;
                    break;
                case 1:
                    imageComp.gameObject.SetActive(true);
                    state = eState.End;
                    break;
                case 2:
                    fadeIn();
                    if (elapseTime >= completionTIme + offset)
                        state = eState.End;
                    break;
                case 3:
                    fadeOut();
                    if (elapseTime >= completionTIme + offset)
                    {
                        imageComp.gameObject.SetActive(false);
                        state = eState.End;
                    }
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    Debug.Log("ImageOutput Effect Error");
                    break;
            }
        }
    }

    private void fadeIn()
    {
        imageComp.gameObject.SetActive(true);
        imageComp.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, elapseTime / completionTIme));
        elapseTime += Time.deltaTime;
    }
    private void fadeOut()
    {
        imageComp.color = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, elapseTime / completionTIme));
        elapseTime += Time.deltaTime;
    }
}

public class ObjectSpawner : Task
{
    private GameObject subject;
    private Vector2 location;
    private string locationName;
    private int condition;
    public ObjectSpawner(string taskID, string objectName, string locationName, string condition, string ohterEventID) : base(taskID, ohterEventID)
    {
        this.locationName = locationName;
        int.TryParse(condition, out this.condition);
        switch (this.condition)
        {
            case 0:
                subject = GameObject.Find(objectName);
                break;
            case 1:
                subject = Resources.Load<GameObject>("Prefabs/Spawnable/" + objectName);
                break;
            default:
                break;
        }
    }

    public override void Execute()
    {
        switch (this.condition)
        {
            case 0:
                GameObject.Destroy(subject);
                state = eState.End;
                break;
            case 1:
                location = GameObject.Find(locationName).transform.position;
                GameObject.Instantiate(subject, location, Quaternion.identity);
                state = eState.End;
                break;
            default:
                break;
        }
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

    public override void Execute()
    {
        state = eState.End;
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

    public override void Execute()
    {
        state = eState.End;
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

    public override void Execute()
    {
        state = eState.End;
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

    public override void Execute()
    {
        state = eState.End;
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

    public override void Execute()
    {
        if (state == eState.Waiting)
        {
            state = eState.Running;
            switch (effect)
            {
                case 0:
                    UITest.Instance.DeactivateNarrative();
                    break;
                case 1:
                    UITest.Instance.ActivateNarrative();
                    UITest.Instance.ModifyNarrative(text);
                    break;
            }
        }
        if(InputManager.Instance.GetKeyAction() == eKeyAction.TextSkip)
            state = eState.End;
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

    public override void Execute()
    {
        if (state == eState.Waiting)
        {
            state = eState.Running;
            switch (effect)
            {
                case 0:
                    UITest.Instance.DeactivatePortrait();
                    break;
                case 1:
                    UITest.Instance.ActivatePortrait();
                    UITest.Instance.ModifyPortrait(resourceName, resourceNum, name, text);
                    break;
            }
        }
        if (InputManager.Instance.GetKeyAction() == eKeyAction.TextSkip)
            state = eState.End;
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

    public override void Execute()
    {
        state = eState.End;
    }
}







