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
        throw new NotImplementedException();
    }

    public override void initialize()
    {
        throw new NotImplementedException();
    }

    public override void Open()
    {
        throw new NotImplementedException();
    }

    public void Heal(PlayerShip ship)
    {

    }
    public void Refuel(PlayerShip ship)
    {

    }

    public void UpdateButtons()
    {
        /*
        Costs:
        HP=Missing*Level*1.1
        Fuel=Missing*Level*1.1
        Remove Status=Level*10.1
        Revive=Level*100.1
        */




    }

}
