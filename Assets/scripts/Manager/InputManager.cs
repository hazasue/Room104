using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 추가적인 키세팅이 필요하다면 eKeyAction의 FullScreen뒤에 값을 추가하고 KeySettting 클래스의 keyCodes 배열 끝에 값을 추가해주면 됨.
// 이후 InputManager 클래스의 Update에서 입력을 확인하고 변수를 변경하는 코드 작성

public enum eKeyAction
{
    None,
    Move,
    Run,
    Interact,
    Setting,
    TextSkip,
    FullScreen,
    KEYCOUNT
}

public class KeySetting
{
    private Dictionary<eKeyAction, List<KeyCode>> keys = new Dictionary<eKeyAction, List<KeyCode>>();
    public Dictionary<eKeyAction, List<KeyCode>> Keys { get { return keys; } }

    public KeySetting()
    {
        KeyCode[] keyCodes = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.LeftShift, KeyCode.E, KeyCode.Escape, KeyCode.X, KeyCode.F4 };
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
    private bool bUIInputToggle = false;
    public bool BInputToglle { get { return bUIInputToggle; } }

    private KeySetting keySet;

    private eKeyAction action = eKeyAction.None;


    public override void Awake()
    {
        base.Awake();
        keySet = new KeySetting();
    }
    
    void Start()
    {
        //For Checking key setting
        //Check if the correct key is matched
        for (int i = 1; i < (int)eKeyAction.KEYCOUNT; i++)
        {
            Debug.Log((eKeyAction)i);
            for (int j = 0; j < keySet.Keys[(eKeyAction)i].Count; j++)
            {
                Debug.Log(keySet.Keys[(eKeyAction)i][j]);
            }
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.anyKey)
        {
            action = eKeyAction.None;
            return;
        }


        if (Input.GetKeyDown(keySet.Keys[eKeyAction.Interact][0]))
        {
            action = eKeyAction.Interact;
            return;
        }

        foreach (KeyCode key in keySet.Keys[eKeyAction.Move])
        {
            if(Input.GetKey(key))
            {
                if (Input.GetKey(keySet.Keys[eKeyAction.Run][0]))
                    action = eKeyAction.Run;
                else
                    action = eKeyAction.Move;
                return;
            }
        }

        if (Input.GetKey(keySet.Keys[eKeyAction.Setting][0]))
        {
            action = eKeyAction.Setting;
            return;
        }

        if (Input.GetKey(keySet.Keys[eKeyAction.TextSkip][0]))
        {
            action = eKeyAction.TextSkip;
            return;
        }

        if (Input.GetKey(keySet.Keys[eKeyAction.FullScreen][0]))
        {
            action = eKeyAction.FullScreen;
            return;
        }

        //Last Check
        action = eKeyAction.None;
    }

    public eKeyAction GetKeyAction()
    {
        return action;
    }

    public void ChangeUIToggle(bool val)
    {
        bUIInputToggle = val;
    }
}
