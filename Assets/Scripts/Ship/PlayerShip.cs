using UnityEngine;
using System.Collections;
using System;
using Adnc.SkillTree;
using System.Collections.Generic;

namespace Battle
{
    public class PlayerShip : Ship
    {

        public static PlayerShip Player;
        public SkillCategory SkillTree;
        public int MaxSkills;
        public List<BattleSkill> CurrentSkills;

        public override void Start()
        {
            base.Start();
            Player = this;
            CurrentSkills = new List<BattleSkill>( KnownSkills);
            UpdateSkillTree();
        }
        void UpdateSkillTree()
        {
            List<SkillCollectionBase> SkillTypes = SkillTree.GetRootSkillCollections();
            foreach(SkillCollectionBase scb in SkillTypes)
            {

            }
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
    }
}