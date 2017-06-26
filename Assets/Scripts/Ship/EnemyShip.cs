using Extensions.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class EnemyShip : Ship
    {
        [Serializable]
        public struct Rewards
        {
            public int ExpReward;
            public int MoneyReward;
            public Item Reward;
        }
        public Rewards reward;
        public override void Start()
        {
            base.Start();
            KnownCommands.Add(new Attack());
        }
        public override IEnumerator GetCommand()
        {

            BattleCommand bc = KnownCommands.RandomItem();
            BattleController.Controller.SelectedCommand = bc;
            bc.User = this;

            yield return new WaitForEndOfFrame();
        }

        public override IEnumerator GetTarget(BattleCommand bc)
        {
            if (bc.SingleTarget)
            {
                Ship[] target = new Ship[] { bc.ValidTargets(this).ToList().RandomItem() };
                BattleController.Controller.SelectedTarget = target;
            }
            else
            {
                Ship[] target = bc.ValidTargets(this);
                BattleController.Controller.SelectedTarget = target;
            }
            yield return null;
        }

        public override void Die()
        {
            base.Die();
            BattleController.Controller.Rewards.Add(reward);

        }

        public void AdjustStatsToLevel()
        {

        }

        
    }
}