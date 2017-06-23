using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{

    public class TargetUI : MonoBehaviour
    {

        public Ship target;
        public PlayerShip User;
        public float rotSpeed;
        public BattleCommand command;
        Button b;
        bool Multi;


        void Start()
        {
            transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            rotSpeed = Random.Range(25, 75);
            b = GetComponent<Button>();
            b.onClick.AddListener(OnClick);
            if (!command.SingleTarget)
            {
                b.image.color = b.colors.highlightedColor;
            }
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



    }
}