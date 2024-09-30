using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Map
{
    public Transform street;
    
    private void Awake()
    {
        init();
    }
    
    protected override void init()
    {
        this.name = MapManager.MAP_NAME_CONVENIENT_STORE;
        
        this.positions = new Dictionary<string, Transform>();
        this.positions.Add(MapManager.MAP_NAME_STREET, street);
    }

    public override Vector3? GetDestination(string from)
    {
        Vector3 pos = Vector3.zero;

        if (this.positions.ContainsKey(from)) pos = this.positions[from].position;
        else
        {
            pos = Vector3.zero;
            Debug.Log($"Cannot move to {name} from {from}");
            return null;
        }

        return pos;
    }
}