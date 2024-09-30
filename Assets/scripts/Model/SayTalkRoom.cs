using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SayTalkRoom : MonoBehaviour
{
    public Image icon;
    public TMP_Text target;
    public TMP_Text content;
    
    public void Init(int target, string content)
    {
        this.icon.sprite = Resources.Load<Sprite>($"Sprites/SayTalk/icon_{target}");
        this.target.text = $"{target}";
        this.content.text = $"{content}";
    }
}
