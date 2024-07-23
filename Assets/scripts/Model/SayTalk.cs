using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SayTalk : MonoBehaviour
{
    public TMP_Text target;
    public TMP_Text content;

    public void Init(int target, string content)
    {
        this.target.text = $"{target}";
        this.content.text = $"{content}";
    }
}
