using Helper_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Overworld
{

    public class OverWorldUI : MonoBehaviour
    {
        public static OverWorldUI UI;
        public SurvivalShopUI Shop;
        public Image HP,Fuel;
        public Text HPText, FuelText;
        public Text Money;
        public PlayerShipMovement Player;

        void Start()
        {
            UI = this;
            SurvivalShopUI.UI = Shop;
        }

        public void OpenShop()
        {
            Debug.Log("Shop");
            Player.ShipCanMove = false;
            Time.timeScale = 0;
            SurvivalShopUI.UI.gameObject.SetActive(true);
            SurvivalShopUI.UI.ChangeType(0);
            Music.ChangeSong("Shop");

        }
        public void ExitShop()
        {
            Player.ShipCanMove = true;
            Time.timeScale = 1;
            Music.ChangeSong("OverWorld");
            SurvivalShopUI.UI.gameObject.SetActive(false);
        }



        


    }
}
