using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
using System;
using UnityEngine.UI;

public class RepairShop : ShopMenu{

    PlayerShip ship;
    public Text[] ShipNames; //I know these var names are dumb but I don't care
    public Button[] Heals;
    public Button[] Refuels;
    public Button[] Repairs;
    public Button[] Revives;


    public Button b;

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override void initialize()
    {
        gameObject.SetActive(true);
        ShipNames[0].text = PlayerShip.Player.Name;
        ShipNames[1].text = PlayerShip.Player.Allies[1].Name;
        ShipNames[2].text = PlayerShip.Player.Allies[2].Name;

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
        UpdateButton(0);
        UpdateButton(1);
        UpdateButton(2);
    }
    //TODO try to loop this
    public void UpdateButton(int i)
    {
        /*
        Costs:
        HP=Missing*Level*1.1
        Fuel=Missing*Level*1.1
        Remove Status=Level*10.1
        Revive=400+Level*10.1
        */
        PlayerShip ship = (PlayerShip)PlayerShip.Player.Allies[i];
        if (!ship.Alive())
        {
            Heals[i].gameObject.SetActive(false);
            Refuels[i].gameObject.SetActive(false);
            Repairs[i].gameObject.SetActive(false);
            Revives[i].gameObject.SetActive(true);

            int cost = 400 + ship.stats["Level"].Base;  

            Revives[i].GetComponentInChildren<Text>().text = "Revive "+ cost + "$";
            Revives[i].onClick.RemoveAllListeners();
            Revives[i].onClick.AddListener(delegate { Heal(ship, cost); });

            return;
        }
        else
        {
            Revives[i].gameObject.SetActive(false);
        }
        if (ship.stats["Health"].Base <ship.stats["MaxHealth"].Base)
        {
            Heals[i].gameObject.SetActive(true);
            int hp = ship.stats["Health"].Base;
            int max = ship.stats["MaxHealth"].Base;
            int missing = max - hp;
            int lvl = ship.stats["Level"].Base;
            int cost = (int)(missing * lvl * 1.1);
            Heals[i].GetComponentInChildren<Text>().text = "HP " + hp + "/" + max +
                "\nRestore " + cost + "$";
            Heals[i].onClick.RemoveAllListeners();
            Heals[i].onClick.AddListener(delegate { Heal(ship,cost); });
        }
        else
        {
            Heals[i].gameObject.SetActive(false);
        }
        if (ship.stats["Fuel"].Base < ship.stats["MaxFuel"].Base)
        {
            Refuels[i].gameObject.SetActive(true);
            int hp = ship.stats["Fuel"].Base;
            int max = ship.stats["MaxFuel"].Base;
            int missing = max - hp;
            int lvl = ship.stats["Level"].Base;
            int cost = (int)(missing * lvl * 1.1);
            Refuels[i].GetComponentInChildren<Text>().text = "Fuel " + hp + "/" + max +
                "\nRefuel " + cost + "$";
            Refuels[i].onClick.RemoveAllListeners();
            Refuels[i].onClick.AddListener(delegate { Refuel(ship,cost); });
        }
        else
        {
            Refuels[i].gameObject.SetActive(false);
        }
        if (ship.Statuses.Count>0) {
            //TODO Add this when statuses are more implemented
        }
        else
        {
            Repairs[i].gameObject.SetActive(false);
        }



    }

  
}
