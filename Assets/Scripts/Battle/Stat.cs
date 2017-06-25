using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

    public string Name;
    public int Base;
    public int Modified;

    public List<int> Curve;//I think

    public Stat(string name,int _Base)
    {
        Name = name;
        Base = _Base;
        Modified = _Base;
    }

    public Stat(string name, int _Base ,int _Modified) 
    {
        Name = name;
        Base = _Base;
        Modified = _Modified;
    }
}
