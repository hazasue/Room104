using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcData
{
    public int id;
    public string name;
    public bool trait1;
    public string traitName1;
    public List<int> traitEvents1;
    
    public bool trait2;
    public string traitName2;
    public List<int> traitEvents2;

    public NpcData(int id, string name, bool trait1, string traitName1, List<int> traitEvents1, bool trait2,
        string traitName2, List<int> traitEvents2)
    {
        this.id = id;
        this.name = name;
        this.trait1 = trait1;
        this.traitName1 = traitName1;
        this.traitEvents1 = traitEvents1;
        this.trait2 = trait2;
        this.traitName2 = traitName2;
        this.traitEvents2 = traitEvents2;
    }
}
