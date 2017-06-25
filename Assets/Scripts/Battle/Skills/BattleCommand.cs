using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public interface IDoable
    {
        IEnumerator Do(Ship User, Ship[] Target);
        Ship[] ValidTargets(Ship User);
        Ship[] GetTarget(Ship User);

    }

    public abstract class BattleCommand : IDoable
    {
        public string Name;
        public string Description;
        public int Cost;
        public Ship User;
        public bool SingleTarget=true;
        public enum TargetType { Self, Allies, Enemies, All };
        public TargetType target;
        public abstract Ship[] GetTarget(Ship User);
        public abstract IEnumerator Do(Ship User, Ship[] Target);

        public IEnumerator Do(Ship User, Ship Target)
        {
           yield return Do(User, new Ship[] { Target });
        }

        public bool CanUse(Ship User)
        {
            return User.stats["Fuel",false] >= Cost;
        }

        public abstract Ship[] ValidTargets(Ship User);
    }
}