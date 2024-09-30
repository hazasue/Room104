using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    protected string name;
    protected Dictionary<string, Transform> positions;

    protected abstract void init();
    
    public abstract Vector3? GetDestination(string from);
    
    public string GetName() { return name; }
}
