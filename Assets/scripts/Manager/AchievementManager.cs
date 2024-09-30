using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private static AchievementManager instance;
    
    private const string CSV_FILENAME_ACHIEVEMENT = "achievement";
    private const float POP_UP_POSITION_START = 1200f;
    private const float POP_UP_POSITION_END = 1040f;
    private const float POP_UP_SPEED = 180f;

    
    private Dictionary<int, Achievement> achievements;
    public AchievementInstance achievement;
    public Transform viewport;

    public AchievementInstance popUpAchievement;

    private void Start()
    {
        init();
    }

    public static AchievementManager Instance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<AchievementManager>();
        if (instance == null) Debug.Log("There's no active Achievement Manager object");
        return instance;
    }

    private void init()
    {
        //parsing from achievement table
        // init achievement dynamically
        instance = this;

        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.DEFAULT_ACHIEVEMENT_DATA_NAME + ".json"))
        {
            achievements = new Dictionary<int, Achievement>();

            int achievementId;
            
            List<Dictionary<string, object>> achievementDB = CSVReader.Read(CSV_FILENAME_ACHIEVEMENT);
            foreach (Dictionary<string, object> data in achievementDB)
            {
                achievementId = (int)data["id"];
                achievements.Add(achievementId, new Achievement(achievementId, data["imagePath"].ToString(), data["title_KOR"].ToString(), data["title_EN"].ToString(),
                    data["description_KOR"].ToString(), data["description_EN"].ToString(), data["hint_KOR"].ToString(), data["hint_EN"].ToString(),
                    System.Convert.ToBoolean(data["cleared"].ToString())));
            }
            
            JsonManager.CreateJsonFile(JsonManager.DEFAULT_ACHIEVEMENT_DATA_NAME, achievements);
        }
        else
        {
            achievements = JsonManager.LoadJsonFile<Dictionary<int, Achievement>>(JsonManager.DEFAULT_ACHIEVEMENT_DATA_NAME);
        }

        UpdateAchievementInstances();
        Achieve(307);
    }

    public void UpdateAchievementInstances()
    {
        for (int i = viewport.childCount - 1; i >= 0; i--)
        {
            Destroy(viewport.GetChild(i).gameObject);
        }
        
        AchievementInstance tempAchievement;
        
        foreach (Achievement data in achievements.Values)
        {
            tempAchievement = Instantiate(achievement, viewport, true);
            tempAchievement.Init(data.id, data.imagePath, data.titleKor, data.titleEn,
                data.descriptionKor, data.descriptionEn, data.hintKor, data.hintEn, data.cleared);
        }
    }

    public void Achieve(int id)
    {
        if (!achievements.ContainsKey(id)) return;
        if (achievements[id].cleared) return;

        achievements[id].cleared = true;
        Achievement achievement = achievements[id];
        // effect on upper-right corner

        popUpAchievement.Init(achievement.id, achievement.imagePath, achievement.titleKor, achievement.titleEn,
            achievement.descriptionKor, achievement.descriptionEn, achievement.hintKor, achievement.hintEn, achievement.cleared);

        StartCoroutine(popUpAnimate());
        
        JsonManager.CreateJsonFile(JsonManager.DEFAULT_ACHIEVEMENT_DATA_NAME, achievements);
    }

    private IEnumerator popUpAnimate()
    {
        float timeGap = 0f;
        Transform transform = popUpAchievement.transform;

        while (transform.position.y >= POP_UP_POSITION_END)
        {
            timeGap = Time.deltaTime;
            yield return new WaitForSeconds(timeGap);
            transform.position += new Vector3(0f, -1f, 0f) * timeGap * POP_UP_SPEED;
        }

        yield return new WaitForSeconds(1f);
        
        while (transform.position.y <= POP_UP_POSITION_START)
        {
            timeGap = Time.deltaTime;
            yield return new WaitForSeconds(timeGap);
            transform.position += new Vector3(0f, 1f, 0f) * timeGap * POP_UP_SPEED;
        }

    }
}                                                                                                           