using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Overworld
{

    public class OverWorldUI : MonoBehaviour
    {
        public static OverWorldUI UI;
        public Image HP,Fuel;
        public Text HPText, FuelText;
        public Text Money;
        public PlayerShipMovement Player;

        void Start()
        {
            UI = this;
        }

        public void OpenShop()
        {
            Debug.Log("Shop");
            Player.ShipCanMove = false;
            Time.timeScale = 0;
            SurvivalShopUI.UI.ChangeType(0);

        }
        public void ExitShop()
        {
            SurvivalShopUI.UI.CurrentMenu.SetActive(false);
            Player.ShipCanMove = true;
            Time.timeScale = 1;
        }



        


    }
}
