using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattleUI : MonoBehaviour
    {

        public static BattleUI UI;
        BattleController BC = BattleController.Controller;
        public Button Skill, Item, Run, Attack;
        public Image CommandsContainer;
        public TargetUI target;
        public List<TargetUI> curTargets = new List<TargetUI>();
        public Button Back;
        public SkillContainer skillContainer;

        PlayerShip ship { get { return (PlayerShip)BattleController.Controller.CurrentShip; } }

        void Awake()
        {
            UI = this;
            gameObject.SetActive(false);
            
        }

        void Update() { }

        public void BringUpCommands(PlayerShip S)
        {
            CommandsContainer.gameObject.SetActive(true);
            ResetFunctions(Attack,OnAttackPressed);
            ResetFunctions(Skill, OnSkillPressed);
            ResetFunctions(Run,S,OnFleePressed);
        }

        private void ResetFunctions(Button b, Ship S, params Action<Ship>[] funcs)
        {
            b.onClick.RemoveAllListeners();
            foreach (Action<Ship> func in funcs)
            {
                b.onClick.AddListener(delegate { func(S); });
            }
        }

        public void ResetFunctions(Button b, params UnityEngine.Events.UnityAction[] funcs) {
            b.onClick.RemoveAllListeners();
            foreach (UnityEngine.Events.UnityAction func in funcs)
            {
                b.onClick.AddListener(func);
            }
        }

        public void OnAttackPressed()
        {
            
            BattleController.Controller.SetCommand(new Attack(), ship);
            CommandsContainer.gameObject.SetActive(false);
            
        }

        public void OnSkillPressed()
        {
            skillContainer.gameObject.SetActive(true);
            CommandsContainer.gameObject.SetActive(false);
            skillContainer.Initialize(ship);


        }

        public void OnFleePressed(Ship S)
        {
            BattleController.Controller.SetCommand(new Flee(), S);
            CommandsContainer.enabled = false;
        }

        public void MakeTargets(Ship[] ValidTargets,BattleCommand bc)
        {
            foreach (Ship ship in ValidTargets)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(ship.transform.position);
                TargetUI t = Instantiate(target,pos,Quaternion.identity,transform);
                t.command = bc;
                t.target = ship;
                t.User = this.ship;
                Button tb = t.GetComponent<Button>();
                curTargets.Add(t);

            }
            foreach(TargetUI T in curTargets)
            {
                T.CurTargets = curTargets;
            }


        }

        public void DestroyTargets()
        {
            while (curTargets.Count > 0)
            {
                Destroy(curTargets[0].gameObject);
                curTargets.RemoveAt(0);
            }
        }

    }
}