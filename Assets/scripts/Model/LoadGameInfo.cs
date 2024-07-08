using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadGameInfo : MonoBehaviour
{
    public TMP_Text rootTypeText;
    public TMP_Text episodeText;
    public TMP_Text dateText;
    public TMP_Text playTimeText;

    public void Init(string rootType, int episode, int date, int gameTime, float playTime)
    {
        rootTypeText.text = rootType;
        episodeText.text = $"{episode}";
        dateText.text = $"{date} 일차 {gameTime}";
        playTimeText.text = $"플레이 타임: {playTime.ToString("F2")}";
    }
}
