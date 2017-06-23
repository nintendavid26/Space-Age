using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO Fix namespaces in general
public class HealItem : Item {

    public int amnt;

    public IEnumerator Do(Ship User, Ship Target)
    {
        yield return null;//Do a sparkle or something

        Target.Heal(amnt);
    }


}
