using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class UseItem : BattleCommand
    {
        Item i;
        Ship Target;

        public UseItem(Item I)
        {
            i = I;
        }

        public override IEnumerator Do(Ship User, Ship[] Target)
        {
            yield return i.Do((PlayerShip)User, Target[0]);
        }

        public override Ship[] GetTarget(Ship User)
        {

            //Get Item to 
            throw new NotImplementedException();
        }

        public override Ship[] ValidTargets(Ship User)
        {
            Debug.Log(i.Name);
            return i.ValidTargets(User);
        }
    }
}