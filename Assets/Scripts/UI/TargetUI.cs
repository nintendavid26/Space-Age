using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{

    public class TargetUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {

        public Ship target;
        public PlayerShip User;
        public float rotSpeed;
        public BattleCommand command;
        Button b;
        bool Multi;
        public List<TargetUI> CurTargets=new List<TargetUI>();


        void Start()
        {
            transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
            rotSpeed = UnityEngine.Random.Range(25, 75);
            b = GetComponent<Button>();
            b.onClick.AddListener(OnClick);
           
        }
        void Update()
        {
            transform.Rotate(Vector3.back * Time.deltaTime * rotSpeed);
        }

        void OnClick()
        {
            if (command.SingleTarget)
            {
                BattleController.Controller.SelectedTarget = new Ship[] { target };
            }
            else
            {
                BattleController.Controller.SelectedTarget = command.ValidTargets(User);
            }
            BattleUI.UI.DestroyTargets();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!command.SingleTarget)
            {
                foreach (TargetUI T in CurTargets)
                {
                   ColorBlock c= T.b.colors;
                    c.normalColor = Color.red;
                    T.b.colors = c;
                } 

            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!command.SingleTarget)
            {
                foreach (TargetUI T in CurTargets)
                {
                    ColorBlock c = T.b.colors;
                    c.normalColor = Color.white;
                    T.b.colors = c;
                }

            }
        }
    }
}