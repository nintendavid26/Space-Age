using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AutoBattleSkill : BattleCommand
    {
        public override IEnumerator Do(Ship User, Ship[] Target)
        {
            throw new NotImplementedException();
        }

        public override Ship[] GetTarget(Ship User)
        {
            throw new NotImplementedException();
        }

        public override Ship[] ValidTargets(Ship User)
        {
            throw new NotImplementedException();
        }
    }
}
