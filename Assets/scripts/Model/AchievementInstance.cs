using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementInstance : MonoBehaviour
{
    public RawImage image;
    public TMP_Text titleText;
    public TMP_Text descText;

    public void Init(int id, string imagePath, string titleKor, string titleEn, string descriptionKor,
        string descriptionEn, string hintKor, string hintEn, bool cleared)
    {
        if (!cleared)
        {
            image.texture = Resources.Load<Texture>("Sprites/Achievements/null");
            titleText.text = "???";
            if(Settings.Instance().isKorean) descText.text = hintKor;
            else
            {
                descText.text = hintEn;
            }
        }
        else
        {
            image.texture = Resources.Load<Texture>($"Sprites/Achievements/{imagePath}");
            if (Settings.Instance().isKorean)
            {
                titleText.text = titleKor;
                descText.text = descriptionKor;
            }
            else
            {
                titleText.text = titleEn;
                descText.text = descriptionEn;
            }
        }
    }
}
