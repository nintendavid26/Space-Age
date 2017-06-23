using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Flee : BattleCommand
    {

        public override IEnumerator Do(Ship User, Ship[] Target)
        {
            Debug.Log("Attempt to flee battle");
            return null;
        }

        public override Ship[] GetTarget(Ship User)
        {
            return null;
        }

        public override Ship[] ValidTargets(Ship User)
        {
            return new Ship[] { User };
        }
    }
}