using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
using System;
using UnityEngine.UI;

public class RepairShop : ShopMenu{

    PlayerShip ship;
    public Text Ship1Name, Ship2Name, Ship3Name; //I know these var names are dumb but I don't care
    public Button Ship1Heal, Ship2Heal, Ship3Heal;
    public Button Ship1Refuel, Ship2Refuel, Ship3Refuel;
    public Button Ship1Repair, Ship2Repair, Ship3Repair;
    public Button Ship1Revive, Ship2Revive, Ship3Revive;


    public Button b;

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override void initialize()
    {
        gameObject.SetActive(true);
        Ship1Name.text = PlayerShip.Player.Name;
        Ship2Name.text = PlayerShip.Player.Allies[1].Name;
        Ship3Name.text = PlayerShip.Player.Allies[2].Name;

        UpdateButtons();
    }

    public override void Open()
    {
        throw new NotImplementedException();
    }

    public void Heal(PlayerShip ship,int cost)
    {
        if (PlayerShip.Money < cost) { return; }
        Debug.Log("Heal " + ship.Name);
        ship.stats["Health"].Base = ship.stats["MaxHealth"].Base;
        PlayerShip.Money -= cost;
        UpdateButtons();
    }
    public void Refuel(PlayerShip ship,int cost)
    {
        if (PlayerShip.Money < cost) { return; }
        Debug.Log("Refuel " + ship.Name);
        ship.stats["Fuel"].Base = ship.stats["MaxFuel"].Base;
        PlayerShip.Money -= cost;
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        /*
        Costs:
        HP=Missing*Level*1.1
        Fuel=Missing*Level*1.1
        Remove Status=Level*10.1
        Revive=500+Level*10.1
        */
        #region Ship1
        PlayerShip ship = PlayerShip.Player;
        if (ship.stats["Health"].Base <ship.stats["MaxHealth"].Base)
        {
            Ship1Heal.gameObject.SetActive(true);
            int hp = ship.stats["Health"].Base;
            int max = ship.stats["MaxHealth"].Base;
            int missing = max - hp;
            int lvl = ship.stats["Level"].Base;
            int cost = (int)(missing * lvl * 1.1);
            Ship1Heal.GetComponentInChildren<Text>().text = "HP " + hp + "/" + max +
                "\nRestore " + cost + "$";
            Ship1Heal.onClick.RemoveAllListeners();
            Ship1Heal.onClick.AddListener(delegate { Heal(ship,cost); });
        }
        else
        {
            Ship1Heal.gameObject.SetActive(false);
        }
        if (ship.stats["Fuel"].Base < ship.stats["MaxFuel"].Base)
        {
            Ship1Refuel.gameObject.SetActive(true);
            int hp = ship.stats["Fuel"].Base;
            int max = ship.stats["MaxFuel"].Base;
            int missing = max - hp;
            int lvl = ship.stats["Level"].Base;
            int cost = (int)(missing * lvl * 1.1);
            Ship1Refuel.GetComponentInChildren<Text>().text = "Fuel " + hp + "/" + max +
                "\nRefuel " + cost + "$";
            Ship1Refuel.onClick.RemoveAllListeners();
            Ship1Refuel.onClick.AddListener(delegate { Refuel(ship,cost); });
        }
        else
        {
            Ship1Refuel.gameObject.SetActive(false);
        }
        if (ship.Statuses.Count>0) {

        }
        else
        {
            Ship1Repair.gameObject.SetActive(false);
        }

        #endregion


    }

}
