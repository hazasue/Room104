using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class JsonManager
{
    public static string DEFAULT_SETTING_DATA_NAME = "settings";
    public static string DEFAULT_PRODUCT_DATA_NAME = "products";
    public static string DEFAULT_NPCINIT_DATA_NAME = "npcinitdata";
    public static string DEFAULT_NPC_DATA_NAME = "npcdata";
    public static string DEFAULT_GAME_DATA_NAME = "gamedata";
    public static string DEFAULT_CURRENT_DATA_NAME = "CurrentGameData";
    public static string DEFAULT_ACCOUNT_DATA_NAME = "AccountData";
    public static string DEFAULT_SAYTALK_DATA_NAME = "SayTalk";
    public static string DEFAULT_ACHIEVEMENT_DATA_NAME = "Achievement";
    
    public static void CreateJsonFile(string fileName, object obj)
    {
        // 데이터 폴더가 없다면 생성하기
        if (!File.Exists(Application.dataPath + "/Data/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/");
        }

        FileStream fileStream = new FileStream(Application.dataPath + "/Data/" + fileName + ".json", FileMode.OpenOrCreate);
        byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        fileStream.SetLength(0);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public static T LoadJsonFile<T>(string fileName)
    {
        if (!File.Exists(Application.dataPath + "/Data/" + fileName + ".json"))
        {
            Debug.Log(Application.dataPath + "/Data/" + fileName + ".json" + ":  Does not exist.");
            return default(T);
        }
        
        FileStream fileStream = new FileStream(Application.dataPath + "/Data/" + fileName + ".json", FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
