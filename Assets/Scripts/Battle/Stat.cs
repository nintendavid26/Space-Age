using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Battle;

public enum op { mult, div};

[System.Serializable]
public class Stat {

    //TODO Make buffs more like pokemon
    public class Buff//and DeBuffs
    {
        public int amnt;
        public int Duration;
        public Stat stat;
        public Ship ship;

        public Buff(Stat _stat,int _amnt,int _Duration,Ship _ship)
        {
            stat = _stat;
            amnt = _amnt;
            Duration = _Duration+1;//This is because for it to last the next turn it has to be set to 2 because it gets reduced each turn
            ship = _ship;
            Ship Ship = ship.GetComponent<Ship>();
        }

        public bool Debuff()
        {
            return amnt < 0;
        }
    }

    public ParticleSystem Particles;

    public string Name;
    public int Base;
    public int Modified {
        get { return CalculateModified(); } }
    public List<Buff> Buffs=new List<Buff>();//Max amnt of buffs?

    public List<int> Curve;//I think
    public Ship ship;

    public Stat(string name,int _Base,Ship _ship)
    {
        Name = name;
        Base = _Base;
        ship = _ship;
        Buffs = new List<Buff>();
    }

    public int PositiveBuff()
    {
        int mult = 0;
        for (int i = 0; i < Buffs.Count; i++)
        {
            mult += Buffs[i].amnt;
        }
        if (mult > 0) { return 1; }
        else if (mult < 0) { return -1; }
        return 0;
    }

    public void TurnPasses()
    {
        if (Buffs == null) { return; }
        if (Buffs.Count == 0) { return; }
        int prev = PositiveBuff();
        for (int i = 0; i < Buffs.Count; i++)
        {
            Buffs[i].Duration= Buffs[i].Duration-1;
            if (Buffs[i].Duration == 0)
            {
                RemoveBuff(i);
            }
        }

        if (prev != PositiveBuff()){
            if ((prev == 1||prev==-1)&&Particles!=null)
            {

                GameObject.Destroy(Particles.gameObject);
                Debug.Log("Destroyed Particles");
            }
            Particles = MakeParticles();
        }
    }

    public void AddBuff(int amnt,int Duration) {
        if (Buffs == null)
        {
            Buffs = new List<Buff>();
        }
        int prevNetPos = PositiveBuff();
        Buff buff = new Buff(this, amnt, Duration, ship);
        Buffs.Add(buff);
        Particles=MakeParticles();
    }

    public void AdjustParticles()
    {

    }

    public void RemoveBuff(int i)
    {
        Buffs.RemoveAt(i);
    }

    ParticleSystem MakeParticles()
    {
        if (PositiveBuff() == 0) { return null; }
        string debuff = PositiveBuff()==-1 ? "DeBuff" : "Buff";
        GameObject P = BattlePrefabs.p.Make(debuff, ship.transform);
        ParticleSystem.MainModule main = P.GetComponent<ParticleSystem>().main;
        Color C = Color.white;
        if (Name == "Atk") { C = Color.red; }
        else if (Name == "Def") { C = Color.blue; }
        main.startColor = C;
        return P.GetComponent<ParticleSystem>();
    }

    public int CalculateModified()
    {
        int x = Base;
        if (Buffs == null) { return x; }
        if (Buffs.Count == 0) { return x; }
        int mult = 0;
        for (int i = 0; i < Buffs.Count; i++)
        {
            mult += Buffs[i].amnt;
        }
        return (int)(x * ((mult+2)*0.5));
    }
}
