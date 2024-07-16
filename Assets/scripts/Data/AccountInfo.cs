using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountInfo
{
    public string currentEnding;

    public AccountInfo(string currentEnding)
    {
        this.currentEnding = currentEnding;
    }
}
