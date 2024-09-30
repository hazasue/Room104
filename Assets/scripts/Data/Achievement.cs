using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public int id;
    public string imagePath;
    public string titleKor;
    public string titleEn;
    public string descriptionKor;
    public string descriptionEn;
    public string hintKor;
    public string hintEn;
    public bool cleared;

    public Achievement(int id, string imagePath, string titleKor, string titleEn, string descriptionKor,
        string descriptionEn, string hintKor, string hintEn, bool cleared)
    {
        this.id = id;
        this.imagePath = imagePath;
        this.titleKor = titleKor;
        this.titleEn = titleEn;
        this.descriptionKor = descriptionKor;
        this.descriptionEn = descriptionEn;
        this.hintKor = hintKor;
        this.hintEn = hintEn;
        this.cleared = cleared;
    }
}

public interface IAchievement
{
    public void Achieve();
}