using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class UseItem : BattleCommand
    {
        Item i;

        public override IEnumerator Do(Ship User, Ship[] Target)
        {
            i.Do(User,Target[0]);
            return null;
        }

        public override Ship[] GetTarget(Ship User)
        {

            //Get Item to 
            throw new NotImplementedException();
        }

        public override Ship[] ValidTargets(Ship User)
        {
            return i.ValidTargets(User);
        }
    }
}