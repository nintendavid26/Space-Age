using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum op { mult, div};

[System.Serializable]
public class Stat {

    public class Buff//and DeBuffs
    {
        public float amnt;
        public op Op;
        public int Duration;
        public string StatName;
        public Transform ship;
        public ParticleSystem Particles;

        public Buff(string _StatName,float _amnt,op _Op,int _Duration,Transform _ship)
        {
            StatName = _StatName;
            amnt = _amnt;
            Op = _Op;
            Duration = _Duration+1;//This is because for it to last the next turn it has to be set to 2 because it gets reduced each turn
            ship = _ship;
            Particles =MakeParticles();
        }

        ParticleSystem MakeParticles()
        {
            string debuff = Debuff() ? "DeBuff" : "Buff";
            GameObject P = BattlePrefabs.p.Make(debuff, ship);
            ParticleSystem.MainModule main = P.GetComponent<ParticleSystem>().main;
            Color C = Color.white;
            if (StatName == "Atk") { C = Color.red; }
            else if (StatName == "Def") { C = Color.blue; }
            main.startColor =C;
            return P.GetComponent<ParticleSystem>();
        }

        public bool Debuff()
        {
            if ((amnt > 1 && Op == op.div) || (amnt < 1 && Op == op.mult))
            {
                return true;
            }
            else { return false; }
        }
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
                GameObject.Destroy(Buffs[i].Particles);
                Buffs.RemoveAt(i);
            }
        }
    }

    public void AddBuff(Buff buff) {
        if (Buffs == null)
        {
            Buffs = new List<Buff>();
        }
        buff.StatName = Name;
        Buffs.Add(buff);
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
