using Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : Item {

   
	[Flags] public enum Stat {atk=1,def=2,speed=4,luck=8}
    public Stat ToBuff;
    public int amnt;
    public int duration;

    public IEnumerator Do(Ship User, Ship Target)
    {
        //double Multiplier = useType == UseType.;
        yield return null;//Do a sparkle or something

        if ( (ToBuff & Stat.atk)== Stat.atk)
        {

        }
        
    }

    public void EndBuff(Ship Target)
    {

    }


}
