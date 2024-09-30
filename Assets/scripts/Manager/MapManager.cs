using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    public static string MAP_NAME_ROOM_104 = "room104";
    public static string MAP_NAME_CONVENIENT_STORE = "store";
    public static string MAP_NAME_FLOOR_1 = "floor1";
    public static string MAP_NAME_FLOOR_2 = "floor2";
    public static string MAP_NAME_FLOOR_3 = "floor3";
    public static string MAP_NAME_STREET = "street";
    
    private Dictionary<string, Map> maps;

    private Map currentMap;
    private Transform player;

    public Map room104;
    public Map store;
    public Map floor1;
    public Map floor2;
    public Map floor3;
    public Map street;
    
    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void init()
    {
        maps = new Dictionary<string, Map>();
        
        maps.Add(MAP_NAME_ROOM_104, room104);
        maps.Add(MAP_NAME_CONVENIENT_STORE, store);
        maps.Add(MAP_NAME_FLOOR_1, floor1);
        maps.Add(MAP_NAME_FLOOR_2, floor2);
        maps.Add(MAP_NAME_FLOOR_3, floor3);
        maps.Add(MAP_NAME_STREET, street);

        player = GameManager.Instance.Player.transform;
        currentMap = maps[MAP_NAME_ROOM_104];
    }

    public void MoveMap(string to)
    {
        Vector3? tempPos = maps[to].GetDestination(currentMap.GetName());
        if (tempPos == null) return;
        player.position = (Vector3)tempPos;
        currentMap = maps[to];
    }
}
