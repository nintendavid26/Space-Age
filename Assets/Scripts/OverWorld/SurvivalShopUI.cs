using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalShopUI : MonoBehaviour {

    //TODO: Changing menus occasionaly gets stuck

    public GameObject SkillMenu,ItemsMenu,EquipmentMenu,RepairMenu,CurrentMenu;
    public Dropdown[] Dropdowns;
    public Dropdown CurrentDropdown;
    public static SurvivalShopUI UI;

    void Start()
    {
        UI = this;
    }
    public void ChangeType(int n) {
        CurrentMenu.SetActive(false);
        if (n == 0) //Items
        {
            CurrentMenu = ItemsMenu;
        }
        else if (n == 1) //Equipment
        {

        }
        else if (n == 2) //Skills
        {
            
            CurrentMenu = SkillMenu;
            
        }
        else if (n == 3)//Repair
        {

        }
        CurrentMenu.SetActive(true);
        CurrentDropdown = Dropdowns[n];
        CurrentDropdown.transform.GetChild(0).GetComponent<Text>().text = CurrentDropdown.options[n].text;
        Debug.Log("Changed to shop " + n);

    }


}
