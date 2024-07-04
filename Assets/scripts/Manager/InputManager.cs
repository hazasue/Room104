using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eKeyAction
{
    None,
    Move,
    Run,
    Interact,
    Setting,
    FullScreen,
    KEYCOUNT
}

public class KeySetting
{
    private Dictionary<eKeyAction, List<KeyCode>> keys = new Dictionary<eKeyAction, List<KeyCode>>();
    public Dictionary<eKeyAction, List<KeyCode>> Keys { get { return keys; } }

    public KeySetting()
    {
        KeyCode[] keyCodes = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.LeftShift, KeyCode.E, KeyCode.Escape, KeyCode.F4 };
        for (int i = 1; i < (int)eKeyAction.KEYCOUNT; i++)
        {
            List<KeyCode> temp = new List<KeyCode>();
            if (i == 1)
            {
                for (int j = 0; j < 4; j++)
                    temp.Add(keyCodes[j]);
            }
            else
            {
                temp.Add(keyCodes[i + 2]);
            }
            keys.Add((eKeyAction)i, temp);
        }
    }
}

public class InputManager : Singleton<InputManager>
{
    private bool bUIInputToggle = true;
    public bool BInputToglle { get { return bUIInputToggle; } }
    private KeySetting keySet;
    private eKeyAction action = eKeyAction.None;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        keySet = new KeySetting();
    }
    
    public eKeyAction GetKeyAction()
    {
        return action;
    }

    void Start()
    {
        for(int i = 1; i < (int)eKeyAction.KEYCOUNT; i++)
        {
            Debug.Log((eKeyAction)i);
            //Debug.Log("½ÇÇàµÊ?" + keySet.Keys[(eKeyAction)i].Count);
            for (int j = 0; j < keySet.Keys[(eKeyAction)i].Count; j++)
            {
                Debug.Log(keySet.Keys[(eKeyAction)i][j]);
            }
        }      
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode key in keySet.Keys[eKeyAction.Move])
        {
            if(Input.GetKey(key))
            {
                action = eKeyAction.Move;
                return;
            }
        }

        if (Input.GetKeyDown(keySet.Keys[eKeyAction.Interact][0]))
        {
            action = eKeyAction.Interact;
            return;
        }

        if (Input.GetKey(keySet.Keys[eKeyAction.Run][0]))
        {
            action = eKeyAction.Run;
            return;
        }

        if (Input.GetKey(keySet.Keys[eKeyAction.Setting][0]))
        {
            action = eKeyAction.Setting;
            return;
        }

        if (Input.GetKey(keySet.Keys[eKeyAction.FullScreen][0]))
        {
            action = eKeyAction.FullScreen;
            return;
        }

        action = eKeyAction.None;
        return;
    }
}
