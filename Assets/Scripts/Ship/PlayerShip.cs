using UnityEngine;
using System.Collections;
using System;
using Adnc.SkillTree;
using System.Collections.Generic;

namespace Battle
{
    public class PlayerShip : Ship
    {
        //All allies share items so it makes sense for this to be static
        public static Dictionary<string, int> Items = new Dictionary<string, int>();
        public static int Money=100;
        public List<string> ItemStrings;
        public static PlayerShip Player;
        public SkillCategory SkillTree;
        public int MaxSkills;
        [HideInInspector]public List<BattleSkill> AvailableSkills;

        public override void Start()
        {
            
            base.Start();
            foreach (string item in ItemStrings)
            {
                GetItem(item);
            }
            if (Player == null) { Player = this; }
            AvailableSkills = new List<BattleSkill>( KnownSkills);
        }
        public override void GetFuel(int amnt)
        {
            base.GetFuel(amnt);
        }

        public override IEnumerator GetCommand()
        {
            if (BattleController.Controller.Auto)
            {
                BattleController.Controller.SetCommand(new Attack(), this);
            }
            else
            {
                BattleUI.UI.BringUpCommands(this);
                yield return new WaitUntil(()=>BattleController.Controller.SelectedCommand!=null);
            }
            
           
        }

        public override IEnumerator GetTarget(BattleCommand bc)
        {
            if (BattleController.Controller.Auto)
            {
                Ship[] target = new Ship[] { BattleController.Controller.EnemyShips[0] };
                BattleController.Controller.SelectedTarget = target;
                yield return null;   
            }
            else
            {
                Debug.Log(bc.ValidTargets(this));
                BattleUI.UI.MakeTargets(bc.ValidTargets(this),BattleController.Controller.SelectedCommand);
                yield return new WaitUntil(() => BattleController.Controller.SelectedTarget != null);
            }
            
        }

        private Ship[] Select(Ship[] ship)
        {

            //Bring Up UI
            throw new NotImplementedException();
        }

        public override void Die()
        {
            base.Die();//TODO Add stuff
        }

        public static void GetItem(string I, int amnt = 1)
        {
            if (Items.ContainsKey(I))
            {
                Items[I] += amnt;
            }
            else
            {
                Items.Add(I, amnt);
            }
        }
        public static int GetItemAmt(string item)
        {
            if (Items.ContainsKey(item))
            {
                return Items[item];
            }
            return 0;
        }
    }
}