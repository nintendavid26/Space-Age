using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum op { mult, div, add, sub };

[System.Serializable]
public class Stat {

    public class Buff//and DeBuffs
    {
        public float amnt;
        public op Op;
        public int Duration;
    }

    public string Name;
    public int Base;
    public int Modified {
        get { return CalculateModified(); } }
    public List<Buff> Buffs=new List<Buff>();//Max amnt of buffs?

    public List<int> Curve;//I think

    public Stat(string name,int _Base)
    {
        Name = name;
        Base = _Base;

        Buffs = new List<Buff>();
    }

    public void TurnPasses()
    {
        if (Buffs == null) { return; }
        if (Buffs.Count == 0) { return; }
        for (int i = 0; i < Buffs.Count; i++)
        {
            Buffs[i].Duration= Buffs[i].Duration-1;
            if (Buffs[i].Duration == 0)
            {
                Buffs.RemoveAt(i);
            }
        }
    }

    public void AddBuff() {

    }

    public void RemoveBuff()
    {

    }

    public int CalculateModified()
    {
        int x = Base;
        if (Buffs == null) { return x; }
        if (Buffs.Count == 0) { return x; }
        Buffs=Buffs.OrderBy(X=>X.Op).ToList();//Might need to reverse, or should it be aplied based on order added

        foreach (Buff b in Buffs)
        {
            switch (b.Op)
            {
                case op.add:
                    x = x + (int)b.amnt;
                    break;
                case op.sub:
                    x = x - (int)b.amnt;
                    break;
                case op.mult:
                    x = (int)(x * b.amnt);
                    break;
                case op.div:
                    x = (int)(x / b.amnt);
                    break;
                default:
                    break;
            }
        }

        return x;
    }
}
