using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopMenu:MonoBehaviour
{
    public abstract void Open();
    public abstract void Close();
    public abstract void initialize();
    public bool Initialized = false;
}

public class SurvivalShopUI : MonoBehaviour {

    //TODO: Changing menus occasionaly gets stuck

    public ShopMenu SkillMenu,ItemsMenu,EquipmentMenu,RepairMenu,CurrentMenu;

    public Dropdown[] Dropdowns;
    public Dropdown CurrentDropdown;
    public static SurvivalShopUI UI;
    public Text money;

    void OnGUI()
    {
        money.text = "Money:" + PlayerShip.Money+"$";
    }
    void OnLevelWasLoaded(int level)
    {
        UI = this;
        Debug.Log("Set");
        gameObject.SetActive(false);
    }
    public void ChangeType(int n) {
        CurrentMenu.Close();
        if (n == 0) //Items
        {
            CurrentMenu = ItemsMenu;
        }
        else if (n == 1) //Equipment
        {
            CurrentMenu = EquipmentMenu;
        }
        else if (n == 2) //Skills
        {
            
            CurrentMenu = SkillMenu;
            
        }
        else if (n == 3)//Repair
        {
            CurrentMenu = RepairMenu;
        }
        CurrentMenu.initialize();
        //CurrentDropdown.transform.GetChild(0).GetComponent<Text>().text = CurrentDropdown.options[n].text;

    }


}
